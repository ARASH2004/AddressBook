using Application.CRUD;
using Application.Dtos.Addresses;
using Application.Dtos.Contacts;
using Domain.Entities;
using Infrustructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace _118.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SearchController : ControllerBase
    {
        private readonly ContactCrud _contactCrud;
       public SearchController(ContactCrud contactCrud)
        {
            this._contactCrud = contactCrud;
        }


        [HttpGet]
        public ActionResult<List<ContactReadDto>> Search(string? firstname, string? lastname, string? address, string? number)
        {
        // if its null it pass but if itsn't it filter contacts
        var contacts = _contactCrud.GetAll().Where(P =>
                    (string.IsNullOrEmpty(firstname) || (string.IsNullOrEmpty(firstname)&&P.FirstName.Contains(firstname)))&&
                    (string.IsNullOrEmpty(lastname) || (string.IsNullOrEmpty(lastname) && P.LastName.Contains(lastname))) &&
                    (string.IsNullOrEmpty(address) || (string.IsNullOrEmpty(address) && P.Addresses.Any(p=>p.Detail.Contains(address)))) &&
                    (string.IsNullOrEmpty(number) || (string.IsNullOrEmpty(number) && P.Numbers.Any(q=>q.value.Contains(firstname))))
                ).ToList();
            
            var dto = contacts.Select(p => new ContactReadDto
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                Addresses = p.Addresses?.Select(p => p.Detail).ToList(),
                Numbers = p.Numbers?.Select(p => p.value).ToList()
            });
            return Ok(dto);
           
        }
    }
}
