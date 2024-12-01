using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPE1D0_HSZF_2024251.Model;
using IPE1D0_HSZF_2024251.Persistence.MsSql.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IPE1D0_HSZF_2024251.Persistence.MsSql
{
    public class Queries : IQueries
    {
        private readonly CarsharingDbContext _context;
        private readonly ICarRepository carRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly ITripsRepository tripRepository;

        public Queries(ICarRepository carRepository, ICustomerRepository customerRepository, ITripsRepository tripRepository)
        {
            this.carRepository = carRepository;
            this.customerRepository = customerRepository;
            this.tripRepository = tripRepository;
        }
        
            public List<Car> GetAverageDistanceByModel()
            {
            var averageDistances = _context.Cars

                .GroupBy(car => car.Model) // Csoportosítás modell szerint
                .Select(group => new Car
                {
                    Model = group.Key,
                    TotalDistance = group.Average(car => car.TotalDistance)
                })
                .ToList();

            return averageDistances;
            }

        public Car CarWithTheMostDistance()
        {
            return _context.Cars.OrderByDescending(t=>t.TotalDistance).FirstOrDefault();
        }

        public List<Customer> Top10Customer()
        {
            var topCustomers = _context.Trip
                .GroupBy(t => t.CustomerId)
                .Select(group => new 
                {
                    Id = group.Key,
                    TotalCost = group.Sum(t=>t.Cost)
                })
                .OrderByDescending(c=>c.TotalCost)
                .Take(10)
                .ToList()
                .Join(_context.Customer, 
                  tripGroup => tripGroup.Id,
                  customer => customer.Id,
                  (tripGroup, customer) => new Customer
                  {
                      Id = customer.Id,
                      Name = customer.Name,
                      Balance = tripGroup.TotalCost,
                  }
                )
            .ToList();
            
            return topCustomers;
        }


    }
}
