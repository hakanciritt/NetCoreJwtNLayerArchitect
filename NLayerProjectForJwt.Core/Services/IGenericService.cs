using SharedLibrary;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NLayerProjectForJwt.Core.Services
{
    public interface IGenericService<TEntity, TDto> where TEntity : class where TDto : class
    {
        Task<Response<TDto>> GetByIdAsync(int id);
        Task<Response<IEnumerable<TDto>>> GetAllAsync();
        Task<Response<TDto>> AddAsync(TDto tDto);
        Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);
        Task<Response<NoDataDto>> RemoveAsync(int id);
        Task<Response<NoDataDto>> UpdateAsync(TDto tDto);
    }
}
