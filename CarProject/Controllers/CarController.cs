using CarProject.Core.Mangers.Interfaces;
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
        private readonly ICarServices _carServices;

        public CarController(CarDbContext context , ICarServices carServices)
        {
            _context = context;
            _carServices = carServices;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Car>>> GetAllCars()
        {
            var res = _carServices.GetAllCars();
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Car>>> GetNotReadedCars()
        {
            var res = _carServices.GetNotReadedCars();
            return Ok(res);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Car>>> GetReadedCars()
        {
            var res = _carServices.GetReadedCars();
            return Ok(res);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var res = _carServices.GetCar(id);
            return Ok(res);
        }

        [HttpPost]
        public ActionResult CreateCar([FromBody] CarViewModel vm)
        {

            var res = _carServices.CreateCar(LoggedInUser, vm);
            return Ok(res);
        }


        [HttpPut]
        public IActionResult EditCar(int id,[FromBody] CarViewModel vm)
        {
            var res = _carServices.EditCar(LoggedInUser,id, vm);
            return Ok(res);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {

            var res = _carServices.DeleteCustomer(id);
            return Ok(res);

        }

    }
}
