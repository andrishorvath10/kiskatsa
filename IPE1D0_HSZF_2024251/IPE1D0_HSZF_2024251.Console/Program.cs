using System;
using IPE1D0_HSZF_2024251.Application;
using IPE1D0_HSZF_2024251.Model;
using IPE1D0_HSZF_2024251.Persistence.MsSql;
using IPE1D0_HSZF_2024251.Persistence.MsSql.Interfaces;
using Microsoft.Extensions.DependencyInjection;

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

var carRepository = serviceProvider.GetService<ICarRepository>();
var customerRepository = serviceProvider.GetService<ICustomerRepository>();
var tripRepository = serviceProvider.GetService<ITripsRepository>();


//Also could add validation for trips if the carId and customerId exists    
int choice;
do
{
    Console.WriteLine("1. Modify Cars");
    Console.WriteLine("2. Modify Customers");
    Console.WriteLine("3. Modify Trips");
    Console.WriteLine("0. Exit");


    choice = int.Parse(Console.ReadLine());

    switch (choice)
    {
        case 1:
            Console.WriteLine("Car Management System");
            Console.WriteLine("1. View All Cars");
            Console.WriteLine("2. Add a New Car");
            Console.WriteLine("3. Update a Car");
            Console.WriteLine("4. Delete a Car");

            int carChoice = int.Parse(Console.ReadLine());
            switch (carChoice)
            {
                case 1:
                    var cars = await carRepository.GetAllCarsAsync();
                    foreach (var car in cars)
                    {
                        Console.WriteLine($"ID: {car.Id}, Model: {car.Model}, Total Distance: {car.TotalDistance}, Distance Since Last Maintenance: {car.DistanceSinceLastMaintenance}");
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter Car Model:");
                    string model = Console.ReadLine();

                    Console.WriteLine("Enter Total Distance:");
                    float totalDistance = float.Parse(Console.ReadLine());

                    Console.WriteLine("Enter Distance Since Last Maintenance:");
                    float distanceSinceLastMaintenance = float.Parse(Console.ReadLine());

                    var newCar = new Car
                    {
                        Model = model,
                        TotalDistance = totalDistance,
                        DistanceSinceLastMaintenance = distanceSinceLastMaintenance
                    };

                    await carRepository.AddCarWithCustomIdAsync(newCar);
                    Console.WriteLine("Car added successfully with ID: " + newCar.Id);
                    break;
                case 3:
                    Console.WriteLine("Enter Car ID to Update:");
                    int updateId = int.Parse(Console.ReadLine());

                    var carToUpdate = await carRepository.GetCarByIdAsync(updateId);
                    if (carToUpdate != null)
                    {
                        Console.WriteLine($"Current Model: {carToUpdate.Model}, enter new Model (or press Enter to keep):");
                        string newModel = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newModel)) carToUpdate.Model = newModel;

                        Console.WriteLine($"Current Total Distance: {carToUpdate.TotalDistance}, enter new Total Distance (or press Enter to keep):");
                        string newTotalDistance = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newTotalDistance))
                            carToUpdate.TotalDistance = float.Parse(newTotalDistance);

                        Console.WriteLine($"Current Distance Since Last Maintenance: {carToUpdate.DistanceSinceLastMaintenance}, enter new Distance (or press Enter to keep):");
                        string newDistance = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newDistance))
                            carToUpdate.DistanceSinceLastMaintenance = float.Parse(newDistance);

                        await carRepository.UpdateCarAsync(carToUpdate);
                        Console.WriteLine("Car updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Car not found.");
                    }
                    break;
                case 4:
                    Console.WriteLine("Enter Car ID to Delete:");
                    int deleteId = int.Parse(Console.ReadLine());
                    await carRepository.DeleteCarAsync(deleteId);
                    Console.WriteLine("Car deleted successfully.");
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
            break;
        case 2:
            Console.WriteLine("Customer Management System");
            Console.WriteLine("1. View All Customers");
            Console.WriteLine("2. Add a New Customer");
            Console.WriteLine("3. Update a Customer");
            Console.WriteLine("4. Delete a Customer");

            int customerChoice = int.Parse(Console.ReadLine());
            switch (customerChoice)
            {
                case 1:
                    var customers = await customerRepository.GetAllCustomersAsync();
                    foreach (var customer in customers)
                    {
                        Console.WriteLine($"ID: {customer.Id}, Name: {customer.Name}, Balance: {customer.Balance}");
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter Customer Name:");
                    string name = Console.ReadLine();

                    Console.WriteLine("Enter Balance:");
                    float balance = float.Parse(Console.ReadLine());

                    var newCustomer = new Customer
                    {
                        Name = name,
                        Balance = balance
                    };

                    await customerRepository.AddCustomerWithCustomIdAsync(newCustomer);
                    Console.WriteLine("Customer added successfully with ID: " + newCustomer.Id);
                    break;
                case 3:
                    Console.WriteLine("Enter Customer ID to Update:");
                    int customerUpdateId = int.Parse(Console.ReadLine());

                    var customerToUpdate = await customerRepository.GetCustomerByIdAsync(customerUpdateId);
                    if (customerToUpdate != null)
                    {
                        Console.WriteLine($"Current Name: {customerToUpdate.Name}, enter new Name (or press Enter to keep):");
                        string newName = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newName)) customerToUpdate.Name = newName;

                        Console.WriteLine($"Current Balance: {customerToUpdate.Balance}, enter new Balance (or press Enter to keep):");
                        string newBalance = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newBalance))
                            customerToUpdate.Balance = float.Parse(newBalance);

                        await customerRepository.UpdateCustomerAsync(customerToUpdate);
                        Console.WriteLine("Customer updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Customer not found.");
                    }
                    break;
                case 4:
                    Console.WriteLine("Enter Customer ID to Delete:");
                    int customerDeleteId = int.Parse(Console.ReadLine());
                    await customerRepository.DeleteCustomerAsync(customerDeleteId);
                    Console.WriteLine("Customer deleted successfully.");
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
            break;
        case 3:
            Console.WriteLine("Trip Management System");
            Console.WriteLine("1. View All Trips");
            Console.WriteLine("2. Add a New Trip");
            Console.WriteLine("3. Update a Trip");
            Console.WriteLine("4. Delete a Trip");

            int tripChoice = int.Parse(Console.ReadLine());
            switch (tripChoice)
            {
                case 1:
                    var trips = await tripRepository.GetAllTripsAsync();
                    foreach (var trip in trips)
                    {
                        Console.WriteLine($"ID: {trip.Id}, Car ID: {trip.CarId}, Customer ID: {trip.CustomerId}, Distance: {trip.Distance}, Cost: {trip.Cost}");
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter Car ID:");
                    int CarId = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter Customer ID:");
                    int CustomerId = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter Distance:");
                    float Distance = float.Parse(Console.ReadLine());

                    Console.WriteLine("Enter Cost:");
                    float cost = float.Parse(Console.ReadLine());

                    var newTrip = new Trip
                    {
                        CarId = CarId,
                        CustomerId = CustomerId,
                        Distance = Distance,
                        Cost = cost
                    };

                    await tripRepository.AddTripWithCustomIdAsync(newTrip);
                    Console.WriteLine("Trip added successfully with ID: " + newTrip.Id);
                    break;
                case 3:
                    Console.WriteLine("Enter Trip ID to Update:");
                    int tripUpdateId = int.Parse(Console.ReadLine());

                    var tripToUpdate = await tripRepository.GetTripByIdAsync(tripUpdateId);
                    if (tripToUpdate != null)
                    {
                        Console.WriteLine($"Current Car ID: {tripToUpdate.CarId}, enter new Car ID (or press Enter to keep):");
                        string newCarId = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newCarId)) tripToUpdate.CarId = int.Parse(newCarId);

                        Console.WriteLine($"Current Customer ID: {tripToUpdate.CustomerId}, enter new Customer ID (or press Enter to keep):");
                        string newCustomerId = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newCustomerId)) tripToUpdate.CustomerId = int.Parse(newCustomerId);

                        Console.WriteLine($"Current Distance: {tripToUpdate.Distance}, enter new Distance (or press Enter to keep):");
                        string newDistance = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newDistance))
                            tripToUpdate.Distance = float.Parse(newDistance);

                        Console.WriteLine($"Current Cost: {tripToUpdate.Cost}, enter new Cost (or press Enter to keep):");
                        string newCost = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newCost))
                            tripToUpdate.Cost = float.Parse(newCost);

                        await tripRepository.UpdateTripAsync(tripToUpdate);
                        Console.WriteLine("Trip updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Trip not found.");
                    }
                    break;
                case 4:
                    Console.WriteLine("Enter Trip ID to Delete:");
                    int tripDeleteId = int.Parse(Console.ReadLine());
                    await tripRepository.DeleteTripAsync(tripDeleteId);
                    Console.WriteLine("Trip deleted successfully.");
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
            break;
        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
} while (choice != 0);



var tripManager = new TripManager(customerRepository, tripRepository);
var customerNotifier = new CustomerNotifier();
var fleetManager = new FleetManager();

// Subscribe to events
tripManager.InsufficientCoverageEvent += customerNotifier.OnInsufficientCoverage;
tripManager.TripCompletedEvent += customerNotifier.OnTripCompleted;
tripManager.TripCompletedEvent += fleetManager.OnTripCompleted;

// Interaction
Console.WriteLine("Welcome to the Trip Management System.");
Console.WriteLine("Enter Customer ID:");
int customerId = int.Parse(Console.ReadLine());

Console.WriteLine("Enter Car ID:");
int carId = int.Parse(Console.ReadLine());

Console.WriteLine("Enter Distance (in km):");
float distance = float.Parse(Console.ReadLine());

// Start the trip
await tripManager.StartTrip(customerId, carId, distance);

