using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NationalParkAPI_01.Models;
using NationalParkAPI_01.Models.DTOs;
using NationalParkAPI_01.Repository.IRepository;
using NP_Web_App.Models;
using Booking = NationalParkAPI_01.Models.Booking;

namespace NationalParkAPI_01.Controllers
{
    [Route("api/Booking")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        public BookingController(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetBookings() 
        {
            var bookingDto = _bookingRepository.GetAllBooking().Select(_mapper.Map<Booking, BookingsDto>);
            return Ok(bookingDto);
        }
        [HttpGet("{BookingId:int}",Name ="GetBooking")]
        public IActionResult GetBooking(int BookingId)
        {
            var booking = _bookingRepository.GetBooking(BookingId);
            if(booking == null) return NotFound();
            var bookingDto = _mapper.Map<BookingsDto>(booking);
            return Ok(bookingDto);
        }
        [HttpPost]
        public IActionResult CreateBooking ([FromBody] BookingsDto bookingsDto)
        {
            if (bookingsDto == null) return BadRequest();
            if (_bookingRepository.BookingExists(bookingsDto.Id))
            {
                ModelState.AddModelError("", $"Booking Id in Db!!:{bookingsDto.Id}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (!ModelState.IsValid) return BadRequest();
            var booking = _mapper.Map<BookingsDto, Booking>(bookingsDto);
            if (!_bookingRepository.CheckAvailability(booking.BookingDate, booking.NationalParkId))
            {
                ModelState.AddModelError("", $"Try Another Date");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (!_bookingRepository.CreateBooking(booking))
            {
                ModelState.AddModelError("", $"Something went wrong while save data :{booking.Id}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return CreatedAtRoute("GetBooking", new { BookingId = booking.Id },booking);
        }
        [HttpPut]
        public IActionResult UpdateBooking([FromBody] BookingsDto bookingsDto)
        {
            if (bookingsDto == null) return BadRequest();
            if (!ModelState.IsValid) return NotFound();
            var bookings = _mapper.Map<Booking>(bookingsDto);
            if (!_bookingRepository.UpdateBooking(bookings))
            {
                ModelState.AddModelError("", $"Something Went Wrong While Updating data !!{bookingsDto.Id}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
        [HttpDelete("{BookingId:int}")]
        public IActionResult DeleteBooking(int BookingId)
        {
            if (!_bookingRepository.BookingExists(BookingId)) return NotFound();
            var booking = _bookingRepository.GetBooking(BookingId);
            if (booking == null) return BadRequest();
            if (!_bookingRepository.DeleteBooking(booking))
            {
                ModelState.AddModelError("", $"Something WEnt Wrong While Deleting data !!{booking.Id}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }

    }
}
