using CarProject.DbModel.Models;
using CarProject.ModelViews.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarProject.Core.Mangers.Interfaces
{
    public interface ICarServices
    {
       public List<Car> GetAllCars();
       public List<Car> GetNotReadedCars();
       public List<Car> GetReadedCars();
       public CarViewModel GetCar(int id);
       public CarViewModel CreateCar(UserModelViewModel currentUser, CarViewModel vm);
       public CarViewModel EditCar(UserModelViewModel currentUser, int id, CarViewModel vm);
       public CarViewModel DeleteCustomer(int id);

    }
}
