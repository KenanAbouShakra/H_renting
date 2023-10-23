namespace HouseRenting.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string BookingDate { get; set; } = String.Empty;
        
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = default!;

        public int ItemId { get; set; }
        public virtual Item? Items { get; set; }
    }
}