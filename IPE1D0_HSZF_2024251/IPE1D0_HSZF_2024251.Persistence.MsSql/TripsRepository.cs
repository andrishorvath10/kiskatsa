using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPE1D0_HSZF_2024251.Model;
using Microsoft.EntityFrameworkCore;

namespace IPE1D0_HSZF_2024251.Persistence.MsSql
{
    public class TripsRepository : ITripsRepository
    {
        private readonly CarsharingDbContext _context;

        public TripsRepository(CarsharingDbContext context)
        {
            _context = context;
        }

        public async Task<List<Trip>> GetAllTripAsync()
        {
            return await _context.Trip.ToListAsync();
        }

        public async Task AddTripsAsync(IEnumerable<Trip> trips)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Trip ON");

                    foreach (var trip in trips)
                    {
                        var existingTrip = await _context.Trip.FindAsync(trip.Id);

                        if (existingTrip != null)
                        {
                            existingTrip.CarId = trip.CarId;
                            existingTrip.CustomerId = trip.CustomerId;
                            existingTrip.Distance = trip.Distance;
                            existingTrip.Cost = trip.Cost;
                            _context.Trip.Update(existingTrip);
                        }
                        else
                        {
                            await _context.Trip.AddAsync(trip);
                        }
                    }

                    await _context.SaveChangesAsync();

                    await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Trip OFF");

                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

    }
}

