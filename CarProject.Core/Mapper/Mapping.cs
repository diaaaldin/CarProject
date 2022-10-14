using AutoMapper;
using CarProject.Models;
using CarProject.ViewModel;

namespace CarProject.Mapper
{
    public class Mapping : Profile
    {
        
        public Mapping()
        {
            CreateMap<User, UserLoginResponseViewModel>().ReverseMap();
            CreateMap<UserModelViewModel, User>().ReverseMap();
            CreateMap<CarViewModel, Car>().ReverseMap();


        }
    }
}
