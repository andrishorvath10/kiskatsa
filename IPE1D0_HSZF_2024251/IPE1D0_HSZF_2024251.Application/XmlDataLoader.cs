using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPE1D0_HSZF_2024251.Persistence.MsSql;

namespace IPE1D0_HSZF_2024251.Application
{
    public class XmlDataLoader
    {
        private readonly ICarRepository _carRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITripsRepository _tripsRepository;

        public XmlDataLoader(ICarRepository carRepository, ICustomerRepository customerRepository, ITripsRepository tripsRepository)
        {
            _carRepository = carRepository;
            _customerRepository = customerRepository;
            _tripsRepository = tripsRepository;
        }

        public async Task LoadDatasFromXml(string carsXmlPath, string customersXmlPath, string tripsXmlPath)
        {
            try
            {
                var cars = await XmlImporter.LoadCarsFromXmlAsync(carsXmlPath);
                var customers = await XmlImporter.LoadCustomersFromXmlAsync(customersXmlPath);
                var trips = await XmlImporter.LoadTripsFromXmlAsync(tripsXmlPath);

                await _carRepository.AddCarsAsync(cars);
                await _customerRepository.AddCustomersAsync(customers);
                await _tripsRepository.AddTripsAsync(trips);

                /*
                Console.WriteLine("Cars uploaded to the database:");
                foreach (var car in await _carRepository.GetAllCarsAsync())
                {
                    Console.WriteLine($"ID: {car.Id}, Model: {car.Model}, Total Distance: {car.TotalDistance}");
                }

                Console.WriteLine("\nTrips uploaded to the database:");
                foreach (var trip in await _tripsRepository.GetAllTripAsync())
                {
                    Console.WriteLine($"ID: {trip.Id}, Car ID: {trip.CarId}, Customer ID: {trip.CustomerId}, Distance: {trip.Distance}, Cost: {trip.Cost}");
                }
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data from XML: {ex.Message}");
                throw;
            }
        }
    }
}
