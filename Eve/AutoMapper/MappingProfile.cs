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
            // CreateMap<Src,Dest>();

            CreateMap<DataRowView, AccountViewModel>().ForMember(dest => dest.IdAccount, conf => conf.MapFrom(src => src.Row.ItemArray[0])).
             ForMember(dest => dest.Username, conf => conf.MapFrom(src => src.Row.ItemArray[1])).
             ForMember(dest => dest.Password, conf => conf.MapFrom(src => src.Row.ItemArray[2]));

            CreateMap<Account, AccountViewModel>().ReverseMap();

            CreateMap<DataRowView, Account>().ForMember(dest => dest.IdAccount, conf => conf.MapFrom(src => src.Row.ItemArray[0])).
             ForMember(dest => dest.Username, conf => conf.MapFrom(src => src.Row.ItemArray[1])).
             ForMember(dest => dest.Password, conf => conf.MapFrom(src => src.Row.ItemArray[2]));

            CreateMap<DataRowView, CategoryViewModel>().ForMember(dest => dest.IdCategory, conf => conf.MapFrom(src => src.Row.ItemArray[0])).
             ForMember(dest => dest.Name, conf => conf.MapFrom(src => src.Row.ItemArray[1]));


            CreateMap<DataRowView, EventViewModel>().ForMember(dest => dest.IdEvent, conf => conf.MapFrom(src => src.Row.ItemArray[0])).
            ForMember(dest => dest.Name, conf => conf.MapFrom(src => src.Row.ItemArray[1])).
            ForMember(dest => dest.Organizers, conf => conf.MapFrom(src => src.Row.ItemArray[2])).
            ForMember(dest => dest.Description, conf => conf.MapFrom(src => src.Row.ItemArray[3])).
            ForMember(dest => dest.IdCity, conf => conf.MapFrom(src => src.Row.ItemArray[4])).
            ForMember(dest => dest.IdAddress, conf => conf.MapFrom(src => src.Row.ItemArray[5])).
            ForMember(dest => dest.IdCategory, conf => conf.MapFrom(src => src.Row.ItemArray[6])).
            ForMember(dest => dest.ScheduledOn, conf => conf.MapFrom(src => src.Row.ItemArray[7])).
            ForMember(dest => dest.Duration, conf => conf.MapFrom(src => src.Row.ItemArray[8])).
            ForMember(dest => dest.Address, conf => conf.MapFrom(src => src.Row.ItemArray[9]));

            CreateMap<DataRowView, Category>().ForMember(dest => dest.IdCategory, conf => conf.MapFrom(src => src.Row.ItemArray[0])).
             ForMember(dest => dest.Name, conf => conf.MapFrom(src => src.Row.ItemArray[1]));

            CreateMap<CategoryViewModel, Category>().ReverseMap();

            CreateMap<Question, QuestionViewModel>().ReverseMap();
            CreateMap<Answer, AnswerViewModel>().ReverseMap();

            CreateMap<Address, AddressViewModel>().ReverseMap();
            CreateMap<Event, EventViewModel>().AfterMap((src, dest) => dest.SetAddress(src.IdAddress));
            CreateMap<EventViewModel, Event>();
        }
    }
}
