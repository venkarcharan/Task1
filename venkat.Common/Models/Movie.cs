using System.ComponentModel.DataAnnotations;

namespace venkat.Common.Models
{
    public class Movie
    {
        public int MovieId { get; set; }

        public string? MovieGuid { get; set; }

        [Required(ErrorMessage = "Movie Name is required")]
        [StringLength(100, ErrorMessage = "Movie Name cannot exceed 100 characters")]
        public string MovieName { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        [StringLength(50, ErrorMessage = "Genre cannot exceed 50 characters")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Language Name is required")]
        [StringLength(50, ErrorMessage = "Language Name cannot exceed 50 characters")]
        public string LanguageName { get; set; }

        [Required(ErrorMessage = "Ticket Price is required")]
        [Range(1, 5000, ErrorMessage = "Ticket Price must be between 1 and 5000")]
        public decimal TicketPrice { get; set; }
    }
}