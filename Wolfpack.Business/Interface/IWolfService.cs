using Wolfpack.Business.Models;
using Wolfpack.Business.Models.Wolf;

namespace Wolfpack.Business.Interface;

public interface IWolfService
{
    Task<IServiceResponse<IReadOnlyList<WolfModel>>> GetAll();

    Task<IServiceResponse<WolfWithPacksModel>> GetById(Guid wolfId);

    Task<IServiceResponse<WolfModel>> Create(WolfForCreationModel forCreationModel);

    Task<IServiceResponse<WolfModel>> Update(Guid wolfId, WolfForUpdateModel forUpdateModel);

    Task<ISimpleServiceResponse> Delete(Guid wolfId);
}