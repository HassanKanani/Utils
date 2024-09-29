using Application.StaticVariable;
using AutoMapper;
using MathNet.Numerics.Statistics.Mcmc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repositories;
using Utils.Common;
using Utils.Contracts;
using Utils.Models;
using static NPOI.HSSF.Util.HSSFColor;
namespace Utils.Services;
public class Service<RequestEntity, ResponseEntity, KeyType, EditEntity, Entity> : IService<RequestEntity, ResponseEntity, KeyType, EditEntity, Entity> where EditEntity : class, new() where RequestEntity : class, new() where ResponseEntity : class, new() where Entity : class, IEntity
{

    private readonly IRepository<Entity> _repository;
    private readonly IMapper _mapper;
    public Service(IRepository<Entity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ApiResponse<bool>> CreateAsync(RequestEntity tentityDto, CancellationToken cancellationToken)
    {
        try
        {
            Entity entity = _mapper.Map<Entity>(tentityDto);
            await _repository.AddAsync(entity, cancellationToken);
            return ApiResponse<bool>.CreateSuccessResponse(CrudMessage.CreateSuccess(nameof(Entity)));
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<ApiResponse<bool>> DeleteAsync(KeyType Id, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _repository.GetById(Id);
            await _repository.DeleteAsync(entity, cancellationToken);
            return ApiResponse<bool>.CreateSuccessResponse(CrudMessage.DeleteSuccess(nameof(Entity)));
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ApiResponse<List<ResponseEntity>>> Get(CancellationToken cancellationToken)
    {
        try
        {
            List<ResponseEntity> r =   _mapper.Map<List<ResponseEntity>>(_repository.TableNoTracking.ToList());
            return  ApiResponse<List<ResponseEntity>>.CreateSuccessResponse(r);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ApiResponse<ResponseEntity>> GetByKeyAsync(KeyType Id, CancellationToken cancellationToken)
    {
        try
        {
          ResponseEntity r = _mapper.Map<ResponseEntity>(_repository.GetById(Id));
            return ApiResponse<ResponseEntity>.CreateSuccessResponse(r);

        }
        catch (Exception)
        {

            throw;
        }
    }

    public Task<ApiResponse<bool>> SoftDelete(KeyType Id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<bool>> UpdateAsync(EditEntity tentityDto, KeyType Id, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _repository.GetById(Id);

             _mapper.Map(tentityDto,entity);
            await _repository.UpdateAsync(entity, cancellationToken);
            return ApiResponse<bool>.CreateSuccessResponse(CrudMessage.UpdateSuccess(nameof(Entity)));
        }
        catch (Exception)
        {
            throw;
        }
    }
}
