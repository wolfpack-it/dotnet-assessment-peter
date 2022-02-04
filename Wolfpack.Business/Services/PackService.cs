using Wolfpack.Business.Interface;
using Wolfpack.Business.Models;
using Wolfpack.Business.Models.Pack;

namespace Wolfpack.Business.Services
{
    internal class PackService : IPackService
    {
        public Task<IServiceResponse<PackModel>> GetById(Guid packId)
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResponse<PackModel>> Create(PackForCreationModel forCreationModel)
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResponse<PackModel>> Update(PackForModificationModel forCreationModel)
        {
            throw new NotImplementedException();
        }

        public Task<ISimpleServiceResponse> Delete(Guid packId)
        {
            throw new NotImplementedException();
        }
    }
}
