using Wolfpack.Business.Models;
using Wolfpack.Business.Models.Pack;

namespace Wolfpack.Business.Interface;

public interface IPackService
{
    Task<IServiceResponse<IReadOnlyList<PackModel>>> GetAll();

    Task<IServiceResponse<PackWithWolvesModel>> GetById(Guid packId);

    Task<IServiceResponse<PackModel>> Create(PackForCreationModel forCreationModel);

    Task<IServiceResponse<PackModel>> Update(Guid packId, PackForUpdateModel forUpdate);

    Task<ISimpleServiceResponse> Delete(Guid packId);

    Task<IServiceResponse<PackWithWolvesModel>> AddWolfToPack(Guid packId, Guid wolfId);

    Task<IServiceResponse<PackWithWolvesModel>> RemoveWolfFromPack(Guid packId, Guid wolfId);
}