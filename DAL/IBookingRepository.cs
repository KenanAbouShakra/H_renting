using Microsoft.AspNetCore.Mvc;

using HouseRenting.Models;
namespace HouseRenting.DAL
{
    public interface IBookingRepository
    {
  
        Task<bool> CreateBookingItem(Booking booking, int itemId);
        Task<Booking?> GetBookingById (int id);
        Task<bool> Delete(int id);
        

    }
}
