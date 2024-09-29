
using Microsoft.AspNetCore.Mvc;
using Utils.Common;
using Utils.Contracts;
using Utils.Models;

namespace Accomodation_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<RequestEntity, ResponseEntity, KeyType, EditEntity, Entity> : ControllerBase where EditEntity : class, new() where RequestEntity : class, new() where ResponseEntity : class, new() where Entity : class, IEntity
    {
        private readonly IService<RequestEntity, ResponseEntity, KeyType, EditEntity, Entity> _Services;

        public GenericController(IService<RequestEntity, ResponseEntity, KeyType, EditEntity, Entity> Services)
        {
            _Services = Services;
        }

        [HttpPost]

        public async Task<ApiResponse<bool>> Add(RequestEntity Create, CancellationToken cancellationToken)
        {
            return await _Services.CreateAsync(Create, cancellationToken);
        }
        [HttpGet]

        public async Task<ApiResponse<List<ResponseEntity>>> Get(CancellationToken cancellationToken)
        {
            return await _Services.Get(cancellationToken);
        }

        [HttpGet("[action]")]

        public async Task<ApiResponse<ResponseEntity>> GetById(KeyType key, CancellationToken cancellationToken)
        {
            return await _Services.GetByKeyAsync(key, cancellationToken);
        }
        [HttpDelete]
        public async Task<ApiResponse<bool>> Delete(KeyType key, CancellationToken cancellationToken)
        {
            return await _Services.DeleteAsync(key, cancellationToken);
        }
        [HttpPut]

        public async Task<ApiResponse<bool>> Update(EditEntity edit,KeyType id, CancellationToken cancellationToken)
        {
            return await _Services.UpdateAsync(edit,id, cancellationToken);
        }
    }
}
