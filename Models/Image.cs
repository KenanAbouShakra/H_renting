namespace HouseRenting.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public virtual int ItemId { get; set; }
        public string? ImageUrl { get; set; } = null;
    }
}
