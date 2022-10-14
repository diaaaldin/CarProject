using AutoMapper;
using CarProject.Core.Mangers.Interfaces;
using CarProject.Data;
using CarProject.Models;
using CarProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazeez.Common.Extensions;

namespace CarProject.Core.Mangers
{
    public class CarServices : ICarServices
    {
        private readonly CarDbContext _context;
        private readonly IMapper _mapper;

        public CarServices(CarDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<Car> GetAllCars()
        {
            var res = _context.Cars.ToList();
            return res;
        }
        public List<Car> GetNotReadedCars()
        {
            var res = _context.Cars.Where(x => x.IsReaded == 0).ToList();
            return res;
        }

        public List<Car> GetReadedCars()
        {
            var res = _context.Cars.Where(x => x.IsReaded == 1).ToList();
            return res;
        }

        public CarViewModel GetCar(int id)
        {
            var car = _context.Cars.Find(id);

            if (car == null)
            {
               throw new ServiceValidationException("Car Not Exsist");
            }
            var res = _mapper.Map<CarViewModel>(car);
            return res;
        }
        public CarViewModel CreateCar(UserModelViewModel currentUser ,CarViewModel vm)
        {
            var url = "";

            var admin = _context.Users.Where(x => x.IsAdmin == 1).ToList();
            bool isAdmen = admin.Any(x => x.Id == currentUser.Id);
            Car res;
           
            if (!string.IsNullOrWhiteSpace(vm.ImageString))
            {
                url = Helper.Helper.SaveImage(vm.ImageString, "CarsImages");
            }
            string image = "";
            if (!string.IsNullOrWhiteSpace(url))
            {
                var baseURL = "https://localhost:44366/";
                image = $@"{baseURL}/api/v1/user/fileretrive/profilepic?filename={url}";
            }
            if (isAdmen)
            {
                res = new Car
                {
                    UserId = vm.UserId,
                    Name = vm.Name,
                    Price = vm.Price,
                    Image = image,
                    Description = vm.Description,
                    CreatedBy = currentUser.Id,
                    CreatedDate = DateTime.Now,
                    IsReaded = 0,
                    Archived = 1
                };
            }
            else
            {
                res = new Car
                {
                    UserId = currentUser.Id,
                    Name = vm.Name,
                    Price = vm.Price,
                    Image = image,
                    Description = vm.Description,
                    CreatedBy = currentUser.Id,
                    CreatedDate = DateTime.Now,
                    IsReaded = 0,
                    Archived = 1
                };
            }
           

            _context.Cars.Add(res);
            _context.SaveChanges();

            var result = _mapper.Map<CarViewModel>(res);
            return result;
        }
        public CarViewModel EditCar(UserModelViewModel currentUser, int id, CarViewModel vm)
        {
            var chick = _context.Cars.Find(id)
                 ?? throw new ServiceValidationException("Car not found");

            var admin = _context.Users.Where(x => x.IsAdmin == 1).ToList();
            bool isAdmen = admin.Any(x => x.Id == currentUser.Id);

            if (chick.User.Id == currentUser.Id || isAdmen == true)
            {
                var url = "";

                if (!string.IsNullOrWhiteSpace(vm.ImageString))
                {
                    url = Helper.Helper.SaveImage(vm.ImageString, "CarsImages");
                }
                chick.Name = vm.Name;
                chick.Price = vm.Price;
                if (!string.IsNullOrWhiteSpace(url))
                {
                    var baseURL = "https://localhost:44366/";
                    chick.Image = $@"{baseURL}/api/v1/user/fileretrive/profilepic?filename={url}";
                }

                chick.Description = vm.Description;
                chick.UpdatedDate = DateTime.Now;
                chick.IsReaded = vm.IsReaded;
                chick.Archived = 1;

                _context.SaveChanges();
            }
            else
            {
                throw new ServiceValidationException("User not valid");
            }



            var result = _mapper.Map<CarViewModel>(chick);
            return result;
        }

        public CarViewModel DeleteCustomer(int id)
        {

            var car = _context.Cars.Find(id);
            if (car == null)
            {
                throw new ServiceValidationException("Car Not found");
            }

            _context.Cars.Remove(car);
            _context.SaveChanges();

            var result = _mapper.Map<CarViewModel>(car);
            return result;
        }




    }
}
