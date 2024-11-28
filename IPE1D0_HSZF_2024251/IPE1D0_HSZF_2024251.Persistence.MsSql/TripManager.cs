using IPE1D0_HSZF_2024251.Model;
using IPE1D0_HSZF_2024251.Persistence.MsSql.Events;
using IPE1D0_HSZF_2024251.Persistence.MsSql.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IPE1D0_HSZF_2024251.Persistence.MsSql.Events.EventArgs;

namespace IPE1D0_HSZF_2024251.Persistence.MsSql
{
    public class TripManager
    {
        public event EventHandler<InsufficientCoverageEventArgs> InsufficientCoverageEvent;
        public event EventHandler<TripCompletedEventArgs> TripCompletedEvent;

        private readonly ICustomerRepository _customerRepository;
        private readonly ITripsRepository _tripRepository;

        public TripManager(ICustomerRepository customerRepository, ITripsRepository tripRepository)
        {
            _customerRepository = customerRepository;
            _tripRepository = tripRepository;
        }

        public async Task StartTrip(int customerId, int carId, float distance)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            float minimumRequiredBalance = 40.0f;
            float baseFare = 0.5f;
            float perKmRate = 0.35f;

            if (customer.Balance < minimumRequiredBalance)
            {
                InsufficientCoverageEvent?.Invoke(this, new InsufficientCoverageEventArgs
                {
                    CustomerName = customer.Name,
                    CurrentBalance = customer.Balance
                });
                return;
            }

            float totalCost = baseFare + (distance * perKmRate);

            customer.Balance -= totalCost;
            await _customerRepository.UpdateCustomerBalanceAsync(customer);

            var newTrip = new Trip
            {
                CarId = carId,
                CustomerId = customerId,
                Distance = distance,
                Cost = totalCost
            };
            await _tripRepository.AddTripAsync(newTrip);

            TripCompletedEvent?.Invoke(this, new TripCompletedEventArgs
            {
                TripId = newTrip.Id,
                CustomerName = customer.Name,
                TotalCost = totalCost,
                Distance = distance
            });
        }
    }


}
