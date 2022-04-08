using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Wolfpack.Business.Interface;
using Wolfpack.Business.Models;
using Wolfpack.Business.Models.Wolf;
using Wolfpack.Business.Validation.Wolf;
using Wolfpack.Data.Database;
using Wolfpack.Data.Database.Entities;

namespace Wolfpack.Business.Services;

public class WolfService : IWolfService
{
    private readonly WolfpackContext _context;
    private readonly IMapper _mapper;

    public WolfService(WolfpackContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IServiceResponse<IReadOnlyList<WolfModel>>> GetAll()
    {
        var wolves = await _context.Wolves
            .AsNoTracking()
            .ProjectTo<WolfModel>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return ServiceResponse.Ok<IReadOnlyList<WolfModel>>(wolves);
    }

    public async Task<IServiceResponse<WolfWithPacksModel>> GetById(Guid wolfId)
    {
        var wolves = await _context.Wolves
            .AsNoTracking()
            .Include(x => x.Packs)
            .ProjectTo<WolfWithPacksModel>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == wolfId);

        if (wolves is null)
        {
            return ServiceResponse.Fail<WolfWithPacksModel>(ServiceResultCode.NotFound);
        }

        return ServiceResponse.Ok(wolves);
    }

    public async Task<IServiceResponse<WolfModel>> Create(WolfForCreationModel forCreationModel)
    {
        var validator = new WolfForCreationValidator(_context);

        var validationResult = await validator.ValidateAsync(forCreationModel);

        if (!validationResult.IsValid)
        {
            return ServiceResponse.Fail<WolfModel>(validationResult);
        }

        var entity = _mapper.Map<WolfForCreationModel, Wolf>(forCreationModel);

        await _context.Wolves.AddAsync(entity);

        await _context.SaveChangesAsync();

        var model = _mapper.Map<Wolf, WolfModel>(entity);

        return ServiceResponse.Ok(ServiceResultCode.Created, model);
    }

    public async Task<IServiceResponse<WolfModel>> Update(Guid wolfId, WolfForUpdateModel forUpdateModel)
    {
        var validator = new WolfForUpdateModelValidator(_context, wolfId);

        var validationResult = await validator.ValidateAsync(forUpdateModel);

        if (!validationResult.IsValid)
        {
            return ServiceResponse.Fail<WolfModel>(validationResult);
        }

        var entity = await _context.Wolves.FirstOrDefaultAsync(x => x.Id == wolfId);

        if (entity is null)
        {
            return ServiceResponse.Fail<WolfModel>(ServiceResultCode.NotFound);
        }

        _mapper.Map(forUpdateModel, entity);

        await _context.SaveChangesAsync();

        var model = _mapper.Map<Wolf, WolfModel>(entity);

        return ServiceResponse.Ok(model);
    }

    public async Task<ISimpleServiceResponse> Delete(Guid wolfId)
    {
        var entity = await _context.Wolves.FirstOrDefaultAsync(x => x.Id == wolfId);

        if (entity is null)
        {
            return SimpleServiceResponse.Fail(ServiceResultCode.NotFound);
        }

        _context.Wolves.Remove(entity);

        await _context.SaveChangesAsync();

        return SimpleServiceResponse.Ok;
    }
}