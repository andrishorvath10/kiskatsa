using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPE1D0_HSZF_2024251.Model;

namespace IPE1D0_HSZF_2024251.Persistence.MsSql
{
    public interface ITripsRepository
    {
        void AddTrips(IEnumerable<Trip> trips);
    }
}
