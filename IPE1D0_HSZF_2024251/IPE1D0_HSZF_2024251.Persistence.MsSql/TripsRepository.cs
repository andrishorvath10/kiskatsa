using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Castle.Core.Resource;
using IPE1D0_HSZF_2024251.Model;
using IPE1D0_HSZF_2024251.Persistence.MsSql.Interfaces;
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

        public async Task<List<Trip>> GetAllTripsAsync()
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

        public async Task AddTripAsync(Trip trip)
        {
            await _context.Trip.AddAsync(trip);
            await _context.SaveChangesAsync();
        }

        public async Task<Trip?> GetTripByIdAsync(int id)
        {
            return await _context.Trip.FindAsync(id);
        }

        public async Task UpdateTripAsync(Trip trip)
        {
            var existingTrip = await _context.Trip.FindAsync(trip.Id);
            if (existingTrip != null)
            {
                existingTrip.CarId = trip.CarId;
                existingTrip.CustomerId = trip.CustomerId;
                existingTrip.Distance = trip.Distance;
                existingTrip.Cost = trip.Cost;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteTripAsync(int id)
        {
            var trip = await _context.Trip.FindAsync(id);
            if (trip != null)
            {
                _context.Trip.Remove(trip);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddTripWithCustomIdAsync(Trip trip)
        {
            trip.Id = await GetMaxTripIdAsync() + 1;

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT trip ON");

                await _context.Trip.AddAsync(trip);
                await _context.SaveChangesAsync();

                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT trip OFF");

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> GetMaxTripIdAsync()
        {
            return await _context.Trip.MaxAsync(t => (int?)t.Id) ?? 0;
        }



        

    }
}

