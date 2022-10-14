using CarProject.Data;
using CarProject.Models;
using CarProject.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tazeez.Common.Extensions;

namespace CarProject.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class CarController : ApiBaseController
    {
        private readonly CarDbContext _context;

        public CarController(CarDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Car>>> GetAllCars()
        {
            return await _context.Cars.ToListAsync();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Car>>> GetNotReadedCars()
        {
            return await _context.Cars.Where(x => x.IsReaded == 0).ToListAsync();
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Car>>> GetReadedCars()
        {
            return await _context.Cars.Where(x => x.IsReaded == 1).ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var customer = await _context.Cars.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPost]
        public ActionResult CreateCar([FromBody] CarViewModel vm)
        {

            var url = "";

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
            var res = new Car
            {
                UserId      = LoggedInUser.Id,
                Name        = vm.Name,
                Price       = vm.Price,
                Image       = image,
                Description = vm.Description,
                CreatedDate = DateTime.Now,
                IsReaded    = 0,
                Archived    = 1

            };

            _context.Cars.Add(res);
            _context.SaveChanges();

            return Ok(res);
        }


        [HttpPut]
        public IActionResult EditCar(int id,[FromBody] CarViewModel vm)
        {
            var chick = _context.Cars.Find(id)
                ?? throw new ServiceValidationException("Car not found");

            var admin = _context.Users.Where(x => x.IsAdmin == 1).ToList();
            bool isAdmen = admin.Any(x => x.Id == LoggedInUser.Id);

            if ( chick.User.Id == LoggedInUser.Id || isAdmen == true)
            {
                return BadRequest();
            }

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

            return Ok(chick);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Cars.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
