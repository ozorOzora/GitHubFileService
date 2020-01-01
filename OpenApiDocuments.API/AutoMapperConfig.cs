using AutoMapper;
using OpenApiDocuments.API.ViewModels;
using OpenApiDocuments.Core.BO;

namespace OpenApiDocuments.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Document, DocumentViewModel>()
                .ForMember(x => x.Title, x => x.MapFrom(d => d.Metadata.Title))
                .ForMember(x => x.Description, x => x.MapFrom(d => d.Metadata.Description))
                .ForMember(x => x.Servers, x => x.MapFrom(d => d.Metadata.Servers));
        }
    }
}
