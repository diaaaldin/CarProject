using CarProject.Core.Mangers.Interfaces;
using CarProject.ModelViews.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Linq;
using Tazeez.Common.Extensions;

namespace CarProject.Controllers
{
    public class ApiBaseController : Controller
    {
        private UserModelViewModel _loggedInUser;

        public UserModelViewModel LoggedInUser
        {
            get
            {
                if (_loggedInUser != null)
                {
                    return _loggedInUser;
                }

                Request.Headers.TryGetValue("Authorization", out StringValues Token);

                if (string.IsNullOrWhiteSpace(Token))
                {
                    _loggedInUser = null;
                    return _loggedInUser;
                }

                var ClaimId = User.Claims.FirstOrDefault(c => c.Type == "Id");

                int.TryParse(ClaimId.Value, out int idd);

                if (ClaimId == null || !int.TryParse(ClaimId.Value, out int id))
                {
                    throw new ServiceValidationException(401, "Invalid or expired token");
                }

                var commonManager = HttpContext.RequestServices.GetService(typeof(ICommonManager)) as ICommonManager;

                _loggedInUser = commonManager.GetUserRole(new UserModelViewModel { Id = id });

                //return new UserModelViewModel
                //{
                //    Id = id,
                //    FirstName = "admin",
                //    LastName = "admin",
                //    Email = "admin@mail.com",
                //    IsAdmin = 1
                //};
                return _loggedInUser;
            }
        }

        public ApiBaseController()
        {
        }
    }
}
