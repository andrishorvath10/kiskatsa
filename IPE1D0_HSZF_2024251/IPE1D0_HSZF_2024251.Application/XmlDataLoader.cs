using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
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

        public void LoadDatasFromXml()
        {
            var cars = XmlImporter.LoadCarsFromXml("cars.xml");
            var customers = XmlImporter.LoadCustomersFromXml("customers.xml");
            var trips = XmlImporter.LoadTripsFromXml("trips.xml");

            _carRepository.AddCars(cars);
            _customerRepository.AddCustomers(customers);
            _tripsRepository.AddTrips(trips);
        }
    }
}
