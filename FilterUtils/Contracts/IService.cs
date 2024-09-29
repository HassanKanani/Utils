
using Utils.Common;
using Utils.Models;
namespace Utils.Contracts;
public interface IService<RequestEntity,ResponseEntity,KeyType,EditEntity,Entity> where EditEntity : class,new() where RequestEntity : class, new() where ResponseEntity : class ,new() where Entity : class, IEntity
{
    Task<ApiResponse<bool>> CreateAsync(RequestEntity tentityDto, CancellationToken cancellationToken);
    Task<ApiResponse<bool>> DeleteAsync(KeyType Id, CancellationToken cancellationToken);
    Task<ApiResponse<bool>> SoftDelete(KeyType Id, CancellationToken cancellationToken);
    Task<ApiResponse<bool>> UpdateAsync(EditEntity tentityDto, KeyType Id, CancellationToken cancellationToken);
    Task<ApiResponse<ResponseEntity>> GetByKeyAsync(KeyType Id, CancellationToken cancellationToken);
    Task<ApiResponse<List<ResponseEntity>>> Get(CancellationToken cancellationToken);
}
