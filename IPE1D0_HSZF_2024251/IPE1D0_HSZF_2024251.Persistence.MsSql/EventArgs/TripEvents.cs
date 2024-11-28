using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPE1D0_HSZF_2024251.Persistence.MsSql.Events
{
    public class EventArgs
    {
        public class InsufficientCoverageEventArgs : EventArgs
        {
            public string CustomerName { get; set; }
            public float CurrentBalance { get; set; }
        }

        public class TripCompletedEventArgs : EventArgs
        {
            public int TripId { get; set; }
            public string CustomerName { get; set; }
            public float TotalCost { get; set; }
            public float Distance { get; set; }
        }
    }

}
