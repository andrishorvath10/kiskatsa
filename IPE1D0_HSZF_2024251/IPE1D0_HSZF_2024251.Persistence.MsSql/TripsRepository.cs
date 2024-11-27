using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Resource;
using IPE1D0_HSZF_2024251.Model;
using Microsoft.EntityFrameworkCore;

namespace IPE1D0_HSZF_2024251.Persistence.MsSql
{
    public class TripsRepository : ITripsRepository
    {
        private readonly CarsharingDbContext _context;

        public TripsRepository(CarsharingDbContext carsharingDbContext)
        {
            _context = carsharingDbContext;
        }

        public void AddTrips(IEnumerable<Trip> trips)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Engedélyezd az IDENTITY_INSERT-et a Customer táblán
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Customer ON");

                    // Adatok beszúrása
                    _context.trip.AddRange(trips);
                    _context.SaveChanges();

                    // Kapcsold ki az IDENTITY_INSERT-et
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Customer OFF");

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw; // Újradobja a hibát
                }
            }
        }
    }
}
