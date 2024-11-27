using System;
using IPE1D0_HSZF_2024251.Application;
using IPE1D0_HSZF_2024251.Persistence.MsSql;
using Microsoft.Extensions.DependencyInjection;
using static System.Formats.Asn1.AsnWriter;

namespace IPE1D0_HSZF_2024251.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*var serviceprovider = new ServiceCollection()
                .AddDbContext<CarsharingDbContext>()
                .AddScoped<ICarRepository, CarRepository>()
                .AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped<ITripsRepository, TripsRepository>()
                .AddScoped<XmlDataLoader>()
                .BuildServiceProvider();

            using (var scope = serviceprovider.CreateScope()) 
            {
                var dataloader = scope.ServiceProvider.GetRequiredService<XmlDataLoader>();
                dataloader.LoadDatasFromXml();
            }*/
            var dbContext = new CarsharingDbContext();

            // XML fájlok helye
            string carsXmlPath = "cars.xml";
            string customersXmlPath = "customers.xml";
            string tripsXmlPath = "trips.xml";

            // Adatok beolvasása XML-ből
            var cars = XmlImporter.LoadCarsFromXml(carsXmlPath);
            var customers = XmlImporter.LoadCustomersFromXml(customersXmlPath);
            var trips = XmlImporter.LoadTripsFromXml(tripsXmlPath);

            // Adatok feltöltése az adatbázisba
            dbContext.cars.AddRange(cars);
            dbContext.customer.AddRange(customers);
            dbContext.trip.AddRange(trips);
            dbContext.SaveChanges();

            


        }
        
       
    }
}
