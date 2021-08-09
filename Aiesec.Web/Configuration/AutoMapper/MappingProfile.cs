using AutoMapper;
using System.Collections.Generic;

namespace Aiesec.Web.Configuration.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
        // Database.Office 
        CreateMap<Data.Model.BusinessModel.Office, Data.DTO.Response.Office>().ReverseMap()
            .ForMember(dest=> dest.City,opt=>opt.MapFrom(src=>src.City));

        CreateMap<Data.DTO.Request.Office, Data.Model.BusinessModel.Office>().ReverseMap()
            .ForMember(dest => dest.CityID, opt => opt.MapFrom(src => src.City));

        CreateMap<Data.DTO.Request.LocalCommittee, Data.Model.BusinessModel.LocalCommittee>().ReverseMap()
                 .ForMember(dest => dest.CityID, opt => opt.MapFrom(src => src.City));

        CreateMap<Data.Model.BusinessModel.LocalCommittee, Data.DTO.Response.LocalCommittee>().ReverseMap()
                 .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City));

        CreateMap<Data.Model.BusinessModel.FunctionalField, Data.DTO.Request.FunctionalField>().ReverseMap();
        CreateMap<Data.Model.BusinessModel.FunctionalField, Data.DTO.Response.FunctionalField>().ReverseMap();
            
        CreateMap<Data.Model.BusinessModel.FileModel, Data.DTO.Request.FileModel>().ReverseMap();
        CreateMap<Data.Model.BusinessModel.FileModel, Data.DTO.Response.FileModel>().ReverseMap();


        CreateMap<Data.DTO.Request.Report, Data.Model.BusinessModel.Report>();
        CreateMap<Data.Model.BusinessModel.Report, Data.DTO.Response.Report>();

        }
    }
}
