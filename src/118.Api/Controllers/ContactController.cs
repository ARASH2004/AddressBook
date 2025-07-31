using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.CRUD;
using Application.Dtos.Contacts;

namespace _118.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ContactController : ControllerBase
    {
        private readonly ContactCrud _contactCrud;

        public ContactController(ContactCrud contactCrud)
        {
            this._contactCrud = contactCrud;
        }

        [HttpPost("add contact")]
        public ActionResult<Contact> CreateContact([FromBody] ContactWriteDto dto)
        {
            var contact = new Contact
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                // if it has value just select its detail / value
                Addresses = dto.Addresses.Count > 0 ? dto.Addresses.Select(p => new Address { Detail = p.Detail }).ToList() : null,
                Numbers = dto.Numbers.Count > 0 ? dto.Numbers.Select(p => new Number { value = p.Value }).ToList() : null
            };
            _contactCrud.Add(contact);
            return Ok(GetContactDto(contact));
        }

        private ActionResult<ContactReadDto> GetContactDto(Contact contact)
        {
            return new ContactReadDto
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Addresses = contact.Addresses?.Select(p => p.Detail).ToList(),
                Numbers = contact.Numbers?.Select(p => p.value).ToList()
            };
        }
        [HttpGet("Get All contact")]
        public ActionResult<List<ContactReadDto>> GetAllContact()
        {
            // load all contacts then pass values to dto
            var contacts = _contactCrud.GetAll();
            var dto = contacts.Select(p => new ContactReadDto
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                Addresses = p.Addresses?.Select(p => p.Detail).ToList(),
                Numbers = p.Numbers?.Select(p => p.value).ToList()
            });
            return Ok(dto);
        }
        [HttpGet("{id}")]
        public ActionResult<ContactReadDto> GetById(Guid id)
        {
            var contact = _contactCrud.GetById(id);
            var dto = GetContactDto(contact);
            return Ok(dto);
        }
        [HttpPost("{id}")]
        public ActionResult<ContactReadDto> Update(Guid id , [FromBody] ContactUpdateDto dto)
        {
            var contact = _contactCrud.GetById(id);
            contact.FirstName = dto.FirstName;
            contact.LastName = dto.LastName;
            _contactCrud.Update(contact);
            var result = GetContactDto(contact);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public ActionResult Delet(Guid id)
        {
            var contact = _contactCrud.GetById(id);
            _contactCrud.Delete(contact);
            return Ok();
        }

    }
}
