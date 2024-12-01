using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPE1D0_HSZF_2024251.Model;
using IPE1D0_HSZF_2024251.Persistence.MsSql.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IPE1D0_HSZF_2024251.Persistence.MsSql
{
    public class CarRepository : ICarRepository
    {
        private readonly CarsharingDbContext _context;

        public CarRepository(CarsharingDbContext context)
        {
            _context = context;
        }
        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task AddCarsAsync(IEnumerable<Car> cars)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Cars ON");

                    foreach (var car in cars)
                    {
                        var existingCar = await _context.Cars.FindAsync(car.Id);

                        if (existingCar != null)
                        {
                            existingCar.Model = car.Model;
                            existingCar.TotalDistance = car.TotalDistance;
                            existingCar.DistanceSinceLastMaintenance = car.DistanceSinceLastMaintenance;
                            _context.Cars.Update(existingCar);
                        }
                        else
                        {
                            await _context.Cars.AddAsync(car);
                        }
                    }

                    await _context.SaveChangesAsync();

                    await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Cars OFF");

                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task AddCarAsync(Car car)
        {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCarAsync(Car car)
        {
            var existingCar = await _context.Cars.FindAsync(car.Id);
            if (existingCar != null)
            {
                existingCar.Model = car.Model;
                existingCar.TotalDistance = car.TotalDistance;
                existingCar.DistanceSinceLastMaintenance = car.DistanceSinceLastMaintenance;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Car?> GetCarByIdAsync(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task AddCarWithCustomIdAsync(Car car)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT cars ON");

            car.Id = await GetMaxCarIdAsync() + 1;
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT cars OFF");

            await transaction.CommitAsync();
        }

        public async Task<int> GetMaxCarIdAsync()
        {
            return await _context.Cars.MaxAsync(c => (int?)c.Id) ?? 0;
        }

        public Task<Car> GetCarbyMostDistanceAsync()
        {
            return  _context.Cars.OrderByDescending(c => c.TotalDistance).FirstOrDefaultAsync();
        }

        public float AvgDistanceFromCars()
        {
            return _context.Cars.Average(c => c.TotalDistance);
        }
    }
}
