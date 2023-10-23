using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace HouseRenting.Models
{
    public class Item
    {
           public int ItemId { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        [Range(1,24, ErrorMessage = "The numbers of rooms must be greater than 0.and less then 20")]
        public int Rooms { get; set; }

        [Range(5, int.MaxValue, ErrorMessage = "The Area must be greater than 5m.")]
        public int Area {get; set; }
        [Range(500, int.MaxValue, ErrorMessage = "The Renting must be greater than 500 kr.")]
        public int Renting { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public bool IsBooked { get; set; } = false;
        public virtual List<Image>? Images { get; set; }
        public virtual Booking? Bookings { get; set; }


    }
}
