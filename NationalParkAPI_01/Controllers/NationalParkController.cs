using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NationalParkAPI_01.Models;
using NationalParkAPI_01.Models.DTOs;
using NationalParkAPI_01.Repository;
using NationalParkAPI_01.Repository.IRepository;

namespace NationalParkAPI_01.Controllers
{
    [Route("api/NationalPark")]
    [ApiController]
    //[Authorize]
    public class NationalParkController : Controller
    {
        private readonly INationalParkRepository _nationalParkRepository;
        private readonly IMapper _mapper;
        public NationalParkController(IMapper mapper, INationalParkRepository nationalParkRepository)
        {
            _nationalParkRepository = nationalParkRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetNationalParks()
        {
            var NationalParkDto = _nationalParkRepository.GetNationalParks().Select(_mapper.Map<NationalPark, NationalParkDto>);
            return Ok(NationalParkDto);
        }

        [HttpGet("{nationalParkId:int}",Name = "GetNationalPark")]
        public IActionResult GetNationalPark(int nationalParkId)
        {
            var nationalpark = _nationalParkRepository.GetNationalPark(nationalParkId);
            if (nationalpark == null) return NotFound();
            var nationalParkDto = _mapper.Map<NationalParkDto>(nationalpark);
            return Ok(nationalParkDto);
        }
        [HttpPost]
        public IActionResult Createnationalpark([FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null) return BadRequest();
            if (_nationalParkRepository.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "National Park in Db!!");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (!ModelState.IsValid) return BadRequest();
            var nationalpark = _mapper.Map<NationalParkDto, NationalPark>(nationalParkDto);
            if (!_nationalParkRepository.CreateNationalPark(nationalpark)) {
                ModelState.AddModelError("", $"Something wemt wrong while save data :{nationalParkDto.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            // return Ok();
            return CreatedAtRoute("GetNationalPark", new { nationalParkId = nationalpark.Id},nationalpark);

        }
    
        [HttpPut]
        public IActionResult UpdatenationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null) return BadRequest();
            if (!ModelState.IsValid) return NotFound();
            var nationalpark = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_nationalParkRepository.UpdateNationalPark(nationalpark))
            {
                ModelState.AddModelError("", $"Something WEnt Wrong While Updating data !!{nationalParkDto.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
        [HttpDelete("{nationalParkId:int}")]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            if (!_nationalParkRepository.NationalParkExists(nationalParkId)) return NotFound();
            var nationalPark = _nationalParkRepository.GetNationalPark(nationalParkId);
            if (nationalPark == null) return BadRequest();
            if (!_nationalParkRepository.DeleteNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"Something WEnt Wrong While Deleting data !!{nationalPark.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
    }
}

