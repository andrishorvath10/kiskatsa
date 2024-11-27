using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using IPE1D0_HSZF_2024251.Model;

namespace IPE1D0_HSZF_2024251.Persistence.MsSql
{
    public class XmlImporter
    {
        public static async Task<List<Car>> LoadCarsFromXmlAsync(string filePath)
        {
            return await LoadFromXmlAsync<Car>(filePath, "Cars");
        }

        public static async Task<List<Customer>> LoadCustomersFromXmlAsync(string filePath)
        {
            return await LoadFromXmlAsync<Customer>(filePath, "Customers");
        }

        public static async Task<List<Trip>> LoadTripsFromXmlAsync(string filePath)
        {
            return await LoadFromXmlAsync<Trip>(filePath, "Trips");
        }

        private static async Task<List<T>> LoadFromXmlAsync<T>(string filePath, string rootElement)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"The file '{filePath}' was not found.");
                }

                var serializer = new XmlSerializer(typeof(List<T>), new XmlRootAttribute(rootElement));

                using var reader = new StreamReader(filePath);
                string xmlContent = await reader.ReadToEndAsync();
                using var stringReader = new StringReader(xmlContent);

                var result = serializer.Deserialize(stringReader) as List<T>;
                if (result == null)
                {
                    throw new InvalidOperationException($"Failed to deserialize XML file '{filePath}' into a list of {typeof(T).Name}.");
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading XML from file '{filePath}': {ex.Message}");
                throw;
            }
        }
    }
}
