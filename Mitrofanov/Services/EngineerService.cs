using AutoMapper;
using StudioStatistic.Models;
using StudioStatistic.Models.DTO;
using StudioStatistic.Repositories;

namespace StudioStatistic.Services
{
    public class EngineerService : IEngineerService
    {
        private readonly IEngineersRepository _repo;
        private readonly IMapper _mapper;

        public EngineerService(IEngineersRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить всех звукорежиссёров
        /// </summary>
        public async Task<IEnumerable<EngineerDto>> GetAllAsync()
        {
            var engineers = _repo.GetAll();
            return _mapper.Map<IEnumerable<EngineerDto>>(engineers);
        }

        /// <summary>
        /// Получить звукорежиссёра по ID
        /// </summary>
        public async Task<EngineerDto?> GetByIdAsync(int id)
        {
            var engineer = _repo.GetById(id);
            return engineer == null
                ? null
                : _mapper.Map<EngineerDto>(engineer);
        }

        /// <summary>
        /// Нанять нового звукорежиссёра
        /// </summary>
        public async Task<EngineerDto> CreateAsync(CreateEngineerDto dto)
        {
            var engineer = _mapper.Map<Engineers>(dto);
            var created = _repo.Create(engineer);
            return _mapper.Map<EngineerDto>(created);
        }

        /// <summary>
        /// Обновить данные звукорежиссёра
        /// </summary>
        public async Task<EngineerDto> UpdateAsync(int id, EngineerDto dto)
        {
            var engineer = _repo.GetById(id);
            if (engineer == null)
                throw new KeyNotFoundException("Звукорежиссёр не найден");

            _mapper.Map(dto, engineer);
            var updated = _repo.Update(engineer);
            return _mapper.Map<EngineerDto>(updated);
        }

        /// <summary>
        /// Уволить звукорежиссёра
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            return _repo.Delete(id);
        }
    }
}