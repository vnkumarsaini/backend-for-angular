using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NationalParkAPI_01.Data;
using NationalParkAPI_01.Models;
using NationalParkAPI_01.Repository.IRepository;
using NP_Web_App.Models;

namespace NationalParkAPI_01.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;
        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool BookingExists(int BookingId)
        {
            return _context.Bookings.Any(b => b.Id == BookingId);
        }

        public bool CheckAvailability(DateTime date, int Id)
        {
            var booking = _context.Bookings.Where(s=>s.BookingDate== date && s.NationalParkId==Id).Count();
            return booking <= 10;
        }

       

        public bool CreateBooking(Models.Booking booking)
        {
            _context.Bookings.Add(booking);
            return Save();
        }

        public bool DeleteBooking(Models.Booking booking)
        {
            _context.Bookings.Remove(booking);
            return Save();
        }

       
       

        public bool Save()
        {
            return _context.SaveChanges() == 1 ? true : false;
        }

        public bool UpdateBooking(Models.Booking booking)
        {
            _context.Bookings.Update(booking);
            return Save();
        }

        ICollection<Models.Booking> IBookingRepository.GetAllBooking()
        {
            return _context.Bookings.ToList();
        }

        Models.Booking IBookingRepository.GetBooking(int BookingId)
        {
            return _context.Bookings.Find(BookingId);
        }
    }
}
