using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IPE1D0_HSZF_2024251.Persistence.MsSql.Events.EventArgs;

namespace IPE1D0_HSZF_2024251.Persistence.MsSql
{
    public class FleetManager
    {
        public void OnTripCompleted(object sender, TripCompletedEventArgs e)
        {
            Console.WriteLine($"Fleet Management: Trip ID {e.TripId} completed. Distance: {e.Distance} km, Cost: €{e.TotalCost}.");
        }
    }
}
