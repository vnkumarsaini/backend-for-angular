using NationalParkAPI_01.Models;

namespace NationalParkAPI_01.Repository.IRepository
{
    public interface IBookingRepository
    {
        ICollection<Booking> GetAllBooking();
        Booking GetBooking(int BookingId);
        bool BookingExists(int BookingId);
        bool CreateBooking(Booking booking);
        bool UpdateBooking(Booking booking);
        bool DeleteBooking(Booking booking);
        bool CheckAvailability(DateTime date, int Id); 
        bool Save();
    }
}
