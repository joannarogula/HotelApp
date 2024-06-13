using HotelBookingApp.Domain.Interfaces;
using HotelBookingApp.Domain.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HotelBookingApp.Infrastructure.Data
{
    public class BookingRepository : IBookingRepository
    {
        private readonly string _connectionString;

        public BookingRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command =
                       new MySqlCommand("SELECT * FROM Bookings WHERE BookingId = @BookingId", connection))
                {
                    command.Parameters.AddWithValue("@BookingId", bookingId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Booking
                            {
                                BookingId = reader.GetInt32("BookingId"),
                                // Zakładając, że RoomId i UserId są kolumnami w tabeli Bookings
                                RoomId = reader.GetInt32("RoomId"),
                                UserId = reader.GetInt32("UserId"),
                                // Dodaj inne pola w zależności od struktury tabeli
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            var bookings = new List<Booking>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("SELECT * FROM Bookings", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            bookings.Add(new Booking
                            {
                                BookingId = reader.GetInt32("BookingId"),
                                RoomId = reader.GetInt32("RoomId"),
                                UserId = reader.GetInt32("UserId"),
                                // Dodaj inne pola w zależności od struktury tabeli
                            });
                        }
                    }
                }
            }

            return bookings;
        }

        public async Task AddBookingAsync(Booking booking)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command =
                       new MySqlCommand(
                           "INSERT INTO Bookings (RoomId, UserId, /* Inne kolumny */) VALUES (@RoomId, @UserId, /* Inne wartości */)",
                           connection))
                {
                    command.Parameters.AddWithValue("@RoomId", booking.RoomId);
                    command.Parameters.AddWithValue("@UserId", booking.UserId);
                    // Dodaj inne parametry w zależności od struktury tabeli

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command =
                       new MySqlCommand(
                           "UPDATE Bookings SET RoomId = @RoomId, UserId = @UserId, /* Inne kolumny */ WHERE BookingId = @BookingId",
                           connection))
                {
                    command.Parameters.AddWithValue("@RoomId", booking.RoomId);
                    command.Parameters.AddWithValue("@UserId", booking.UserId);
                    command.Parameters.AddWithValue("@BookingId", booking.BookingId);
                    // Dodaj inne parametry w zależności od struktury tabeli

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteBookingAsync(int bookingId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("DELETE FROM Bookings WHERE BookingId = @BookingId", connection))
                {
                    command.Parameters.AddWithValue("@BookingId", bookingId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}

