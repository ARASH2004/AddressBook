using Application.CRUD;
using Application.Dtos.Addresses;
using Application.Dtos.Numbers;
using Domain.Entities;
using Infrustructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace _118.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class NumberController : ControllerBase
    {
        private readonly NumberCrud _numberCrud;
        public NumberController(NumberCrud numberCrud)
        {
            this._numberCrud = numberCrud;
        }

        [HttpPost("{id}")]
        public ActionResult<NumberReadDto> CreateNumber(Guid id, [FromBody] NumberWriteDto dto)
        {
            var  number = new Number {
                ContactId = id,
                value = dto.Value,
            };
            _numberCrud.Add(number);
            return Ok(GetNumberDto(number));
        }

        [HttpGet("{id}")]
        public ActionResult<List<Number>> GetAllNumbers(Guid id)
        {
            var numbers = _numberCrud.GetAllNumbers(id);
            var dto = numbers.Select(p => new NumberReadDto
            {
                Value = p.value,
                Id = p.Id
            }).ToList();
            return Ok(dto);
        }
        [HttpGet("{id}")]
        public ActionResult<List<Address>> GetNumberById(Guid id)
        {
            var address = _numberCrud.GetNumberById(id);

            return Ok(GetNumberDto(address));
        }
        [HttpPost("{id}")]
        public ActionResult<NumberReadDto> UpdateNumber(Guid id, [FromBody] string value)
        {
            var number = _numberCrud.GetNumberById(id);
            number.value = value;
            _numberCrud.Update(number);
            return Ok(number);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteNumber(Guid id)
        {
            var number = _numberCrud.GetNumberById(id);
            _numberCrud.Delete(number);
            return Ok();
        }
        private NumberReadDto GetNumberDto(Number number)
        {
         return new NumberReadDto
         {
             Value=number.value,
             Id = number.Id
         };   
        }
    }
}
