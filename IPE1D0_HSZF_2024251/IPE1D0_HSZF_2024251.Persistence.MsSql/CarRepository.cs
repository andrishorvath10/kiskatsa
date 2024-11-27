using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPE1D0_HSZF_2024251.Model;
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
    }
}
