using System.ComponentModel.DataAnnotations;

namespace venkat.Common.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string SeatNumber { get; set; }

        public DateTime BookingDate { get; set; }

        public string City { get; set; }

        public int MovieId { get; set; }
    }
}