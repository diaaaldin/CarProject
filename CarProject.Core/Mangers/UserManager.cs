using AutoMapper;
using CarProject.Core.Mangers.Interfaces;
using CarProject.Data;
using CarProject.DbModel.Models;
using CarProject.ModelViews.ViewModel;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Tazeez.Common.Extensions;

namespace CarProject.Core.Mangers
{
    public class UserManager : IUserManager
    {
        private readonly CarDbContext _context;
        private readonly IMapper _mapper;

        public UserManager(CarDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region public
        public UserLoginResponseViewModel Login(UserLoginViewModel vm)
        {

            var user = _context.Users.FirstOrDefault(x => x.Email.ToLower() == vm.Email.ToLower());

            if (user == null || !VerifyHashPassword(vm.Password, user.Password))
            {
                throw new ServiceValidationException(300, "User Is not valid Name or password");
            }

            var res = new UserLoginResponseViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Image = user.Image,
                Token = $"Bearer {GenerateJwtTaken(user)}"
            };

            //var res = _mapper.Map<UserLoginResponseViewModel>(user);
            //res.Token = $"Bearer {GenerateJwtTaken(user)}";

            return res;
        }

        public UserLoginResponseViewModel Rigester(UserRegisterViewModel vm)
        {
    
            if (_context.Users.Any(x => x.Email.ToLower() == vm.Email.ToLower()))
            {
                throw new ServiceValidationException(300, "User Is Exist");
            }

            var hashed = HashPassword(vm.Password);

            var res = new User
            {

                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email.ToLower(),
                Password = hashed,
                ConfirmPassword = hashed,
                CreatedDate = DateTime.Now,
                Image = String.Empty,
                IsAdmin = vm.IsAdmin,
                Archived = 1,

            };
            _context.Users.Add(res);
            _context.SaveChanges();

            //var result = _mapper.Map<UserLoginResponseViewModel>(res);
            //result.Token = $"Bearer {GenerateJwtTaken(res)}";
            var result = new UserLoginResponseViewModel
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email,
                Image = String.Empty,
                Token = $"Bearer {GenerateJwtTaken(res)}"
            };

            return result;
        }

        public UserModelViewModel UpdateProfile(UserModelViewModel currentUser ,UserModelViewModel request)
        {

            var user = _context.Users.FirstOrDefault(x => x.Id == currentUser.Id)
                ?? throw new ServiceValidationException("User not found");

            if (user == null)
            {
                throw new ServiceValidationException("User not found");
            }

            var url = "";

            if (!string.IsNullOrWhiteSpace(request.ImageString))
            {
                url = Helper.Helper.SaveImage(request.ImageString, "profileimages");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UpdatedDate = DateTime.Now;
            user.IsAdmin = request.IsAdmin;

            if (!string.IsNullOrWhiteSpace(url))
            {
                var baseURL = "https://localhost:44366/";
                user.Image = $@"{baseURL}/api/v1/user/fileretrive/profilepic?filename={url}";
            }
            _context.SaveChanges();

            var res = new UserModelViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Image = user.Image,
                Email = user.Email,
            };
            //return _mapper.Map<UserModelViewModel>(user);
            return res;
        }
        public void DeleteUser(UserModelViewModel currentUser , int id)
        {
            if (currentUser.Id == id )
            {
                throw new ServiceValidationException("you have no access to delete your self");
            }
            var user = _context.Users
                .FirstOrDefault(x => x.Id == id)
                ??throw new ServiceValidationException("User not found");
            user.Archived = 0;
            _context.SaveChanges();
        }
        #endregion public

        #region private
        private static string HashPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            return hashedPassword;
        }
        private static bool VerifyHashPassword(string password, string HashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, HashedPassword);
        }

        private string GenerateJwtTaken(User user)
        {
            var jwtKey = "#test.key*j;ljklkjhadfsd";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub , $"{user.FirstName} {user.LastName}"),
                new Claim(JwtRegisteredClaimNames.Email , user.Email ),
                new Claim("Id" , user.Id.ToString() ),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString() ),
            };
            var issuer = "test.com";
            var taken = new JwtSecurityToken(
                issuer,
                issuer,
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(taken);
        }
        #endregion private
    }
}
