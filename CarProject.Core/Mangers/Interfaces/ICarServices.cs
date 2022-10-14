using CarProject.Models;
using CarProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarProject.Core.Mangers.Interfaces
{
    public interface ICarServices
    {
        List<Car> GetAllCars();
        List<Car> GetNotReadedCars();
        List<Car> GetReadedCars();
        CarViewModel GetCar(int id);
        CarViewModel CreateCar(UserModelViewModel currentUser, CarViewModel vm);
        CarViewModel EditCar(UserModelViewModel currentUser, int id, CarViewModel vm);
        CarViewModel DeleteCustomer(int id);

    }
}
