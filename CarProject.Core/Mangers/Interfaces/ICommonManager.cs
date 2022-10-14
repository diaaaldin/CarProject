using CarProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarProject.Core.Mangers.Interfaces
{
    public interface ICommonManager :IManager
    {
        UserModelViewModel GetUserRole(UserModelViewModel user);

    }
}
