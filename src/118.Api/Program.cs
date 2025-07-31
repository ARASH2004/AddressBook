using _118.Api.Services;
using Application.CRUD;
using Infrustructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AddressBookDbContext>();
builder.Services.AddScoped<ContactCrud>();
builder.Services.AddScoped<AddressCrud>();
builder.Services.AddScoped<NumberCrud>();
builder.Services.AddScoped<UserCrud>();
builder.Services.AddScoped<JwtSecurityTokenHandler>();
builder.Services.AddScoped<JWTService>();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
// Registers Swagger generator to produce OpenAPI documentation for your endpoints.
// Register and configure Swagger/OpenAPI generation
builder.Services.AddSwaggerGen(c =>
{
    // 1. Define a Swagger document named "v1" with metadata
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AddressBook API",   // API title shown in the Swagger UI
        Version = "v1"                  // API version identifier
    });

    // 2. Declare the JWT Bearer authentication scheme for the Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",             // HTTP header to read the token from
        Type = SecuritySchemeType.Http,     // We're using HTTP authentication
        Scheme = "Bearer",                    // Scheme name must match the Authorization header
        BearerFormat = "JWT",                       // Optional hint that the format is JWT
        In = ParameterLocation.Header,    // Token will be sent in the request header
        Description = "Enter 'Bearer' [space] and then your JWT token."
    });

    // 3. Require the Bearer scheme globally so every endpoint uses it by default
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"               // Matches the definition name above
                }
            },
            Array.Empty<string>()                // No OAuth2 scopes required
        }
    });
});



// Retrieve the JWT secret key from configuration (appsettings.json or environment variable).
string key = builder.Configuration["jwt:key"];

// Convert the secret key string into a byte array for cryptographic operations.
var KeyBytes = Encoding.ASCII.GetBytes(key);


// Add and configure authentication services in the DI container.
builder.Services.AddAuthentication(options =>
{
    // Set the default authentication scheme to JWT Bearer.
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

    // Set the default challenge scheme (used when authentication is required but missing/invalid).
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Configure JWT Bearer handler with validation parameters.
.AddJwtBearer(options =>
{
    // When false, allows HTTP (non-HTTPS) metadata endpoints—use false only in development.
    options.RequireHttpsMetadata = false;

    // Save the validated token in HttpContext so you can retrieve it later if needed.
    options.SaveToken = true;

    // Define how incoming JWTs should be validated.
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Ensure the token has a valid signature from our issuer key.
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(KeyBytes),

        // If true, the token’s 'iss' claim must match a configured value.
        // Here it’s set to false, so 'iss' is not validated.
        ValidateIssuer = false,

        // If true, the token’s 'aud' (audience) claim must match a configured value.
        // Here it’s set to false, so 'aud' is not validated.
        ValidateAudience = false,

        // Ensure the token has not expired.
        ValidateLifetime = true,

        // Clock skew allowed when checking token expiry (set to zero for strict expiry).
        ClockSkew = TimeSpan.Zero,
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
     app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
