using AutoMapper;
using CarProject.Core.Mangers.Interfaces;
using CarProject.Data;
using CarProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazeez.Common.Extensions;

namespace CarProject.Core.Mangers
{
    public class CommonManager : ICommonManager
    {
        private readonly IMapper _mapper;
        private readonly CarDbContext _context;

        public CommonManager(CarDbContext context , IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public UserModelViewModel GetUserRole(UserModelViewModel user)
        {
            var dbuser = _context.Users.FirstOrDefault(x => x.Id == user.Id)
                ?? throw new ServiceValidationException("User Is not valid");

            var res = new UserModelViewModel
            {
                Id = dbuser.Id,
                FirstName = dbuser.FirstName,
                LastName = dbuser.LastName,
                Image = dbuser.Image,
                Email = dbuser.Email,
                

            };
            //return _mapper.Map<UserModelViewModel>(dbuser);
            return res;
        }
    }
}
