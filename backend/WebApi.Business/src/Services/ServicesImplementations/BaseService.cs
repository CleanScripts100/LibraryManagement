using AutoMapper;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Shared;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Shared;

namespace WebApi.Business.src.Implementations
{
    public class BaseService<T, TReadDto, TCreateDto, TUpdateDto> : IBaseService<T, TReadDto, TCreateDto, TUpdateDto>
    {
        private readonly IBaseRepository<T> _baseRepo;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepository<T> baseRepo, IMapper mapper)
        {
            _baseRepo = baseRepo;
            _mapper = mapper;
        }
        public async Task<bool> DeleteOneById(Guid id)
        {
            var foundItem = await _baseRepo.GetOneById(id);
            if (foundItem != null)
            {
                await _baseRepo.DeleteOneById(foundItem);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<TReadDto>> GetAll()
        {
            var result = await _baseRepo.GetAll();
            return _mapper.Map<IEnumerable<TReadDto>>(result);
        }

        public async Task<IEnumerable<TReadDto>> GetAll(QueryOptions queryOptions)
        {
            var result = await _baseRepo.GetAll(queryOptions);
            return _mapper.Map<IEnumerable<TReadDto>>(result);
        }
      

        public async Task<TReadDto> GetOneById(Guid id)
        {
            return _mapper.Map<TReadDto>(await _baseRepo.GetOneById(id));
        }
        public async Task<T> GetEntityById(Guid id)
        {
            return await _baseRepo.GetOneById(id);
        }
        public virtual async Task<TReadDto> UpdateOneById(Guid id, TUpdateDto updated)
        {
            var foundItem = _mapper.Map<T>(await _baseRepo.GetOneById(id)) ?? throw CustomException.NotFoundException();
            var baseEntity = _mapper.Map<T>(updated);
            var updatedEntity = await _baseRepo.UpdateOneById(baseEntity);
            return _mapper.Map<TReadDto>(updatedEntity);
        }

        public virtual async Task<TReadDto> CreateOne(TCreateDto dto)
        {
            var entity = await _baseRepo.CreateOne(_mapper.Map<T>(dto));
            return _mapper.Map<TReadDto>(entity);
        }
    }
}