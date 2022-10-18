using AutoMapper;
using CarProject.DbModel.Models;
using CarProject.ModelViews.ViewModel;
using CSVWorker.Common.Extensions;

namespace CarProject.Mapper
{
    public class Mapping : Profile
    {
        
        public Mapping()
        {
            CreateMap<User, UserLoginResponseViewModel>().ReverseMap();
            CreateMap<UserModelViewModel, User>().ReverseMap();
            CreateMap<Car, CarViewModel>().ReverseMap();
            CreateMap<BlogViewModel, Blog>().ReverseMap();
            CreateMap<PagedResult<BlogViewModel>, PagedResult<Blog>>().ReverseMap();

        }
    }
}
