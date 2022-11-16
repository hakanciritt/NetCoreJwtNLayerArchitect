using Microsoft.EntityFrameworkCore;
using NLayerProjectForJwt.Core.Repositories;
using NLayerProjectForJwt.Core.Services;
using NLayerProjectForJwt.Core.UnitOfWork;
using SharedLibrary;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NLayerProjectForJwt.Service.Services
{
    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _repository;

        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public virtual async Task<Response<TDto>> AddAsync(TDto tDto)
        {
            var entity = ObjectMapper.Mapper.Map<TEntity>(tDto);
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            var newDto = ObjectMapper.Mapper.Map<TDto>(entity);

            return Response<TDto>.Success(newDto, 200);
        }

        public virtual async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var getAll = await _repository.GetAllAsync();
            var productsDto = ObjectMapper.Mapper.Map<List<TDto>>(getAll);
            return Response<IEnumerable<TDto>>.Success(productsDto, 200);
        }

        public virtual async Task<Response<TDto>> GetByIdAsync(int id)
        {
            var find = await _repository.GetByIdAsync(id);
            if (find == null)
            {
                return Response<TDto>.Fail("Bulunamadı", 404, true);
            }
            var product = ObjectMapper.Mapper.Map<TDto>(find);
            return Response<TDto>.Success(product, 200);
        }

        public virtual async Task<Response<NoDataDto>> RemoveAsync(int id)
        {
            var isExistsEntity = await _repository.GetByIdAsync(id);
            if (isExistsEntity == null) return Response<NoDataDto>.Fail("Bulunamadı", 404, true);

            _repository.Remove(isExistsEntity);
            await _unitOfWork.CommitAsync();
            return Response<NoDataDto>.Success(204);
        }

        public virtual async Task<Response<NoDataDto>> UpdateAsync(TDto tDto)
        {
            var mapEntity = ObjectMapper.Mapper.Map<TEntity>(tDto);
            _repository.Update(mapEntity);
            await _unitOfWork.CommitAsync();

            return Response<NoDataDto>.Success(204);
        }

        public virtual async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var list = _repository.Where(predicate);

            return Response<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()), 200);

        }
    }
}
