using AutoMapper;
using Wolfpack.Business.Models.Pack;
using Wolfpack.Business.Models.Wolf;
using Wolfpack.Data.Database.Entities;

namespace Wolfpack.Business.Mapping;

internal class WolfpackProfile : Profile
{
    public WolfpackProfile()
    {
        // Pack mapping profile
        CreateMap<Pack, PackModel>(MemberList.Destination);
        CreateMap<Pack, PackWithWolvesModel>(MemberList.Destination);

        CreateMap<PackForCreationModel, Pack>(MemberList.Destination)
            .ForMember(destination => destination.Id, options => options.Ignore());

        CreateMap<PackForUpdateModel, Pack>(MemberList.Destination)
            .ForMember(destination => destination.Id, options => options.Ignore());

        // Wolf mapping profile
        CreateMap<Wolf, WolfModel>(MemberList.Destination);
        CreateMap<Wolf, WolfWithPacksModel>(MemberList.Destination);

        CreateMap<WolfForCreationModel, Wolf>(MemberList.Destination)
            .ForMember(destination => destination.Id, options => options.Ignore());

        CreateMap<WolfForUpdateModel, Wolf>(MemberList.Destination)
            .ForMember(destination => destination.Id, options => options.Ignore());
    }
}