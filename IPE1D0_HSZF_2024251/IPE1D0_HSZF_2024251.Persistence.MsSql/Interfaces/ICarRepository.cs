using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPE1D0_HSZF_2024251.Model;

namespace IPE1D0_HSZF_2024251.Persistence.MsSql.Interfaces
{
    public interface ICarRepository
    {
        Task<List<Car>> GetAllCarsAsync();
        Task AddCarsAsync(IEnumerable<Car> cars);
        Task AddCarAsync(Car car);
        Task<Car?> GetCarByIdAsync(int id);
        Task UpdateCarAsync(Car car);
        
        Task DeleteCarAsync(int id);
        Task AddCarWithCustomIdAsync(Car car);
        Task<Car> GetCarbyMostDistanceAsync();
        

    }
}
