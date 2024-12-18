﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPE1D0_HSZF_2024251.Model;

namespace IPE1D0_HSZF_2024251.Persistence.MsSql.Interfaces
{
    public interface IQueries
    {
        Car CarWithTheMostDistance();
        List<Customer> Top10Customer();
        List<Car> GetAverageDistanceByModel();
    }
}
