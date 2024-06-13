using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingApp.Domain.Models;

namespace HotelBookingApp.Domain.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime startDate, DateTime endDate);
        Task<Room> GetRoomByIdAsync(int roomId);
        Task<Room> CreateRoomAsync(Room room);
        Task UpdateRoomAsync(Room room);
        Task DeleteRoomAsync(int roomId);
        Task<IEnumerable<Room>> GetAllRoomsAsync();
    }
}
