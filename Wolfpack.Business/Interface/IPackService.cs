using Wolfpack.Business.Models;
using Wolfpack.Business.Models.Pack;

namespace Wolfpack.Business.Interface
{
    public interface IPackService
    {
        Task<IServiceResponse<PackModel>> GetById(Guid packId);

        Task<IServiceResponse<PackModel>> Create(PackForCreationModel forCreationModel);

        Task<IServiceResponse<PackModel>> Update(PackForModificationModel forCreationModel);

        Task<ISimpleServiceResponse> Delete(Guid packId);
    }
}
