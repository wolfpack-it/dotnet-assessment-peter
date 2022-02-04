using AutoMapper;
using Wolfpack.Business.Models.Pack;
using Wolfpack.Data.Database.Entities;

namespace Wolfpack.Business.Mapping;

internal class WolfpackProfile : Profile
{
    public WolfpackProfile()
    {
        CreateMap<Pack, PackModel>(MemberList.Destination);

        CreateMap<PackForCreationModel, Pack>(MemberList.Destination)
            .ForMember(destination => destination.Id, options => options.Ignore());

        CreateMap<PackForUpdateModel, Pack>(MemberList.Destination)
            .ForMember(destination => destination.Id, options => options.Ignore());
    }
}