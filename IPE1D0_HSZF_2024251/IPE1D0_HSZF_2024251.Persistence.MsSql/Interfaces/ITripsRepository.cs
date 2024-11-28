using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPE1D0_HSZF_2024251.Model;

namespace IPE1D0_HSZF_2024251.Persistence.MsSql.Interfaces
{
    public interface ITripsRepository
    {
        Task<List<Trip>> GetAllTripsAsync();
        Task AddTripsAsync(IEnumerable<Trip> trips);
        Task AddTripAsync(Trip trip);
        Task<Trip?> GetTripByIdAsync(int id);
        Task UpdateTripAsync(Trip trip);
        Task DeleteTripAsync(int id);
        Task AddTripWithCustomIdAsync(Trip trip);
    }
}
