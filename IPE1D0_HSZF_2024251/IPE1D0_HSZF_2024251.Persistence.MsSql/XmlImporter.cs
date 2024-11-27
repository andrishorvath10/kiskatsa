using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using IPE1D0_HSZF_2024251.Model;

namespace IPE1D0_HSZF_2024251.Persistence.MsSql
{
    public class XmlImporter
    {
        public static List<Car> LoadCarsFromXml(string filePath)
        {
            var serializer = new XmlSerializer(typeof(List<Car>), new XmlRootAttribute("Cars"));
            using var reader = new StreamReader(filePath);
            return (List<Car>)serializer.Deserialize(reader)!;
        }

        public static List<Customer> LoadCustomersFromXml(string filePath)
        {
            var serializer = new XmlSerializer(typeof(List<Customer>), new XmlRootAttribute("Customers"));
            using var reader = new StreamReader(filePath);
            return (List<Customer>)serializer.Deserialize(reader)!;
        }

        public static List<Trip> LoadTripsFromXml(string filePath)
        {
            var serializer = new XmlSerializer(typeof(List<Trip>), new XmlRootAttribute("Trips"));
            using var reader = new StreamReader(filePath);
            return (List<Trip>)serializer.Deserialize(reader)!;
        }
    }
} 
