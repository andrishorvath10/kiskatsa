using System;
using IPE1D0_HSZF_2024251.Application;
using IPE1D0_HSZF_2024251.Persistence.MsSql;
using Microsoft.Extensions.DependencyInjection;

namespace IPE1D0_HSZF_2024251.Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<CarsharingDbContext>()
                .AddScoped<ICarRepository, CarRepository>()
                .AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped<ITripsRepository, TripsRepository>()
                .AddScoped<XmlDataLoader>()
                .BuildServiceProvider();

            string carsXmlPath = "cars.xml";
            string customersXmlPath = "customers.xml";
            string tripsXmlPath = "trips.xml";

            using (var scope = serviceProvider.CreateScope())
            {
                var dataLoader = scope.ServiceProvider.GetRequiredService<XmlDataLoader>();
                await dataLoader.LoadDatasFromXml(carsXmlPath, customersXmlPath, tripsXmlPath);
            }
        }
    }
}
