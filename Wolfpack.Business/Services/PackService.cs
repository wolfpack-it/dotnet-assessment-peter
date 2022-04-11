using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Wolfpack.Business.Interface;
using Wolfpack.Business.Models;
using Wolfpack.Business.Models.Pack;
using Wolfpack.Business.Validation.Pack;
using Wolfpack.Data.Database;
using Wolfpack.Data.Database.Entities;

namespace Wolfpack.Business.Services;

internal class PackService : IPackService
{
    private readonly WolfpackContext _context;
    private readonly IMapper _mapper;

    public PackService(WolfpackContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IServiceResponse<IReadOnlyList<PackModel>>> GetAll()
    {
        var packs = await _context.Packs
            .AsNoTracking()
            .ProjectTo<PackModel>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return ServiceResponse.Ok<IReadOnlyList<PackModel>>(packs);
    }

    public async Task<IServiceResponse<PackWithWolvesModel>> GetById(Guid packId)
    {
        var pack = await _context.Packs
            .AsNoTracking()
            .Include(x => x.Wolves)
            .ProjectTo<PackWithWolvesModel>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == packId);

        if (pack is null)
        {
            return ServiceResponse.Fail<PackWithWolvesModel>(ServiceResultCode.NotFound);
        }

        return ServiceResponse.Ok(pack);
    }

    public async Task<IServiceResponse<PackModel>> Create(PackForCreationModel forCreationModel)
    {
        var validator = new PackForCreationValidator(_context);

        var validationResult = await validator.ValidateAsync(forCreationModel);

        if (!validationResult.IsValid)
        {
            return ServiceResponse.Fail<PackModel>(validationResult);
        }

        var entity = _mapper.Map<PackForCreationModel, Pack>(forCreationModel);

        await _context.Packs.AddAsync(entity);

        await _context.SaveChangesAsync();

        var model = _mapper.Map<Pack, PackModel>(entity);

        return ServiceResponse.Ok(ServiceResultCode.Created, model);
    }

    public async Task<IServiceResponse<PackModel>> Update(Guid packId, PackForUpdateModel forUpdateModel)
    {
        var validator = new PackForUpdateModelValidator(_context, packId);

        var validationResult = await validator.ValidateAsync(forUpdateModel);

        if (!validationResult.IsValid)
        {
            return ServiceResponse.Fail<PackModel>(validationResult);
        }

        var entity = await _context.Packs.FirstOrDefaultAsync(x => x.Id == packId);

        if (entity is null)
        {
            return ServiceResponse.Fail<PackModel>(ServiceResultCode.NotFound);
        }

        _mapper.Map(forUpdateModel, entity);

        await _context.SaveChangesAsync();

        var model = _mapper.Map<Pack, PackModel>(entity);

        return ServiceResponse.Ok(model);
    }

    public async Task<ISimpleServiceResponse> Delete(Guid packId)
    {
        var entity = await _context.Packs.FirstOrDefaultAsync(x => x.Id == packId);

        if (entity is null)
        {
            return SimpleServiceResponse.Fail(ServiceResultCode.NotFound);
        }

        _context.Packs.Remove(entity);

        await _context.SaveChangesAsync();

        return SimpleServiceResponse.Ok;
    }

    public async Task<IServiceResponse<PackWithWolvesModel>> AddWolfToPack(Guid packId, Guid wolfId)
    {
        var addWolfToPackModel = new AddWolfToPackModel { Id = wolfId };
        var validator = new PackForAddWolfToPackModelValidator(_context, packId);

        var validationResult = await validator.ValidateAsync(addWolfToPackModel);

        if (!validationResult.IsValid)
        {
            return ServiceResponse.Fail<PackWithWolvesModel>(validationResult);
        }

        var entity = await _context.Packs.FirstOrDefaultAsync(x => x.Id == packId);
        var wolf = await _context.Wolves.FirstOrDefaultAsync(x => x.Id == wolfId);

        if (entity is null || wolf is null)
        {
            return ServiceResponse.Fail<PackWithWolvesModel>(ServiceResultCode.NotFound);
        }

        entity.Wolves.Add(wolf);

        await _context.SaveChangesAsync();

        var model = _mapper.Map<Pack, PackWithWolvesModel>(entity);

        return ServiceResponse.Ok(model);
    }

    public async Task<IServiceResponse<PackWithWolvesModel>> RemoveWolfFromPack(Guid packId, Guid wolfId)
    {
        var entity = await _context.Packs.Include(x => x.Wolves)
            .Where(x => x.Wolves.Any(w => w.Id == wolfId))
            .FirstOrDefaultAsync(x => x.Id == packId);

        if (entity is null)
        {
            return ServiceResponse.Fail<PackWithWolvesModel>(ServiceResultCode.NotFound);
        }

        var wolf = entity.Wolves.Single(x => x.Id == wolfId);

        wolf.Packs.Remove(entity);

        await _context.SaveChangesAsync();

        var model = _mapper.Map<Pack, PackWithWolvesModel>(entity);

        return ServiceResponse.Ok(model);
    }
}