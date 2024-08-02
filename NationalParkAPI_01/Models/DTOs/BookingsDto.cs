using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NationalParkAPI_01.Models.DTOs
{
    public class BookingsDto
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string CustomerName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        [Required]
        [Phone(ErrorMessage = "Please enter a valid Phone Number.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; }
        public string PaymentStatus { get; set; }
        public string TransactionId { get; set; }
        public string BookingStatus { get; set; }
        [Display(Name = "Number of Adults")]
        public int AdultTickets { get; set; }
        [Display(Name = "Number of Children")]
        public int ChildTickets { get; set; }
        public double TotalPrice { get; set; }
        public int NationalParkId { get; set; }
        [ForeignKey("NationalParkId")]
        public NationalPark NationalPark { get; set; }
    }
}
