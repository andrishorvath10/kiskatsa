using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IPE1D0_HSZF_2024251.Persistence.MsSql.Events.EventArgs;

namespace IPE1D0_HSZF_2024251.Persistence.MsSql
{
    public class CustomerNotifier
    {
        public void OnInsufficientCoverage(object sender, InsufficientCoverageEventArgs e)
        {
            Console.WriteLine($"Dear {e.CustomerName}, your current balance (€{e.CurrentBalance}) is insufficient for a trip. Please top up.");
        }

        public void OnTripCompleted(object sender, TripCompletedEventArgs e)
        {
            Console.WriteLine($"Dear {e.CustomerName}, your trip (ID: {e.TripId}) of {e.Distance} km has been completed. Total cost: €{e.TotalCost}.");
        }
    }
}
