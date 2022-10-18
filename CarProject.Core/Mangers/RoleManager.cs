using AutoMapper;
using CarProject.Core.Mangers.Interfaces;
using CarProject.Data;
using CarProject.ModelViews.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarProject.Core.Mangers
{
    public class RoleManager : IRoleManager
    {
        private CarDbContext _context;
        private IMapper _mapper;
        public RoleManager(CarDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool CheckAccess(UserModelViewModel userModel)
        {
            var isAdmin = _context.Users.Any(a => a.Id == userModel.Id && a.IsAdmin == 1);

            return isAdmin;
        }
    }
}
