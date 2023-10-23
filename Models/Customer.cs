using System.ComponentModel.DataAnnotations;

namespace HouseRenting.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-]{2,20}", ErrorMessage = "The Name must be numbers or letters and between 2 to 20 characters.")]
        [Display(Name = "Customer.Name")]
        public string Name { get; set; } = String.Empty;

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}", ErrorMessage = "Ugyldig e - postadresse")]
        [Display(Name = "Customer.Email")]
        public string Email { get; set; } = String.Empty;

        [RegularExpression(@"^\+?(?:[0-9] ?){4,14}[0-9]$", ErrorMessage = "Invalid phone number")]
        [Display(Name = "Customer.Phone")]
        public string Phone { get; set; }= String.Empty;
        public virtual List<Booking>? Bookings { get; set; }
    }
}