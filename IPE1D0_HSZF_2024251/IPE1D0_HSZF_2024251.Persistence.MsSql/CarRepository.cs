using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPE1D0_HSZF_2024251.Model;
using Microsoft.EntityFrameworkCore;

namespace IPE1D0_HSZF_2024251.Persistence.MsSql
{
    public class CarRepository : ICarRepository
    {
        private readonly CarsharingDbContext? _context;
        public CarRepository(CarsharingDbContext context)
        {
            _context = context;
        }
        public void AddCars(IEnumerable<Car> cars)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // allow IDENTITY_INSERT
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Cars ON");

                    foreach (var car in cars)
                    {
                        // Ellenőrizd, hogy létezik-e már a rekord
                        if (_context.cars.Any(c => c.Id == car.Id))
                        {
                            Console.WriteLine($"Car with Id {car.Id} already exists. Skipping...");
                            continue;
                        }

                        _context.cars.Add(car);
                    }
                    
                    _context.SaveChanges();

                    
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Cars OFF");

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw; // Újradobja a hibát, ha a művelet sikertelen
                }
            }
        }
    }
}
