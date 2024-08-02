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
    [Route("api/Trails")]
    [ApiController]
    //[Authorize]
    public class TrailsController : Controller
    {
        private readonly ITrailsRepository _trailsRepository;
        private readonly IMapper _mapper;
        public TrailsController(IMapper mapper,ITrailsRepository trailsRepository)
        {
            _trailsRepository = trailsRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetTrails()
        {
            var trailsDto = _trailsRepository.GetAllTrails().Select(_mapper.Map<Trails, TrailsDto>);
            return Ok(trailsDto);
        }
        [HttpGet("{trailsid:int}", Name = "GetTrails")]
        public IActionResult GetTrails(int trailsid)
        {
            var trail = _trailsRepository.GetTrails(trailsid);
            if (trail == null) return NotFound();
            var trailDto = _mapper.Map<NationalParkDto>(trail);
            return Ok(trailDto);
        }
        [HttpPost]
        public IActionResult CreateTrails([FromBody] TrailsDto trailsDto)
        {
            if (trailsDto == null) return BadRequest();
            if (_trailsRepository.TrailsExists(trailsDto.Name))
            {
                ModelState.AddModelError("", $"Trails in Db!!:{trailsDto.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (!ModelState.IsValid) return BadRequest();
            var trail = _mapper.Map<TrailsDto, Trails>(trailsDto);
            if (!_trailsRepository.CreateTrails(trail))
            {
                ModelState.AddModelError("", $"Something went wrong while save data :{trail.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
             //return Ok();
            return CreatedAtRoute("GetTrails", new { trailsid = trail.Id });
        }
        [HttpPut]
        public IActionResult UpdateTrails([FromBody] TrailsDto trailsDto)
        {
            if (trailsDto == null) return BadRequest();
            if (!ModelState.IsValid) return NotFound();
            var trails = _mapper.Map<Trails>(trailsDto);
            if (!_trailsRepository.UpdateTrails(trails))
            {
                ModelState.AddModelError("", $"Something WEnt Wrong While Updating data !!{trailsDto.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
        [HttpDelete("{trailsId:int}")]
        public IActionResult DeleteTrail(int trailsId)
        {
            if (!_trailsRepository.TrailsExists(trailsId)) return NotFound();
            var trail = _trailsRepository.GetTrails(trailsId);
            if (trail == null) return BadRequest();
            if (!_trailsRepository.DeleteTrails(trail))
            {
                ModelState.AddModelError("", $"Something Went Wrong While Deleting data !!{trail.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
    }
}
