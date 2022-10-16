using CarProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarProject.Core.Mangers.Interfaces
{
    public interface IRoleManager
    {
        bool CheckAccess(UserModelViewModel userModel);
    }
}
