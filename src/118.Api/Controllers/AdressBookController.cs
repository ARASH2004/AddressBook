using Application.CRUD;
using Application.Dtos.Addresses;
using Domain.Entities;
using Infrustructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace _118.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AdressBookController : ControllerBase
    {
        private AddressCrud _AddressCrud;
        public AdressBookController(AddressCrud addressCrud)
        {
            this._AddressCrud = addressCrud;
        }

        [HttpPost("{id}")]
        public ActionResult<AddressReadDto> CreateAddress(Guid id, [FromBody] AddressWriteDto dto)
        {
            var address = new Address {
                ContactId = id,
                Detail = dto.Detail
            };
            _AddressCrud.Add(address);
            return Ok(GetAddressDto(address));
        }

        [HttpGet("{id}")]
        public ActionResult<List<Address>> GetAllAdresses(Guid id)
        {
            var addresses = _AddressCrud.GetAlladdresses(id);
            var dto = addresses.Select(p => new AddressReadDto
            {
                Detail = p.Detail,
                Id = p.Id
            }).ToList();
            return Ok(dto);
        }
        [HttpGet("{id}")]
        public ActionResult<List<Address>> GetAdresseById(Guid id)
        {
            var address = _AddressCrud.GetAddressById(id);

            return Ok(GetAddressDto(address));
        }
        [HttpPost("{id}")]
        public ActionResult<AddressReadDto> UpdateAddress(Guid id, [FromBody] string Detail)
        {
            var address = _AddressCrud.GetAddressById(id);
            address.Detail = Detail;
            _AddressCrud.Update(address);
            return Ok(address);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteAddress(Guid id)
        {

            _AddressCrud.Delete(id);
            return Ok();
        }
        private AddressReadDto GetAddressDto(Address address)
        {
         return new AddressReadDto
         {
             Detail = address.Detail,
             Id = address.Id
         };   
        }
    }
}
