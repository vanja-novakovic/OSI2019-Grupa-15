using AutoMapper;
using Database.Entities;
using Eve.ViewModel;
using System.Data;

namespace Eve.AutoMapper
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DataRowView, AccountViewModel>().ForMember(dest => dest.IdAccount, conf => conf.MapFrom(src => src.Row.ItemArray[0])).
             ForMember(dest => dest.Username, conf => conf.MapFrom(src => src.Row.ItemArray[1])).
             ForMember(dest => dest.Password, conf => conf.MapFrom(src => src.Row.ItemArray[2]));

            CreateMap<Account, AccountViewModel>().ReverseMap();

            CreateMap<DataRowView, Account>().ForMember(dest => dest.IdAccount, conf => conf.MapFrom(src => src.Row.ItemArray[0])).
             ForMember(dest => dest.Username, conf => conf.MapFrom(src => src.Row.ItemArray[1])).
             ForMember(dest => dest.Password, conf => conf.MapFrom(src => src.Row.ItemArray[2]));
        }
    }
}
