using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBookingApp.Domain.Models;
using HotelBookingApp.Domain.Interfaces;

namespace HotelBookingApp.Domain.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }


        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime startDate, DateTime endDate)
        {
            var allRooms = await _roomRepository.GetAllRoomsAsync();
            var reservedRooms = await _roomRepository.GetReservedRoomsAsync(startDate, endDate);

            var availableRooms = allRooms.Where(room => !reservedRooms.Any(reserved => reserved.RoomId == room.RoomId));
            return availableRooms;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _roomRepository.GetAllRoomsAsync();
        }

        public async Task<Room> GetRoomByIdAsync(int roomId)
        {
            return await _roomRepository.GetRoomByIdAsync(roomId);
        }

        public async Task<Room> CreateRoomAsync(Room room)
        {
            await _roomRepository.AddRoomAsync(room);
            return room;
        }

        public async Task UpdateRoomAsync(Room room)
        {
            await _roomRepository.UpdateRoomAsync(room);
        }

        public async Task DeleteRoomAsync(int roomId)
        {
            await _roomRepository.DeleteRoomAsync(roomId);
        }


        private bool NoBookingOverlap(Room room, DateTime start, DateTime end)
        {
            // Tu można dodać logikę sprawdzającą, czy nie ma nakładających się rezerwacji
            return true;
        }
    }
}
