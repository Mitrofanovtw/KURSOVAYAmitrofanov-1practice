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
            var engineers = await Task.Run(() => _repo.GetAll());
            return _mapper.Map<IEnumerable<EngineerDto>>(engineers);
        }

        /// <summary>
        /// Получить звукорежиссёра по ID
        /// </summary>
        public async Task<EngineerDto?> GetByIdAsync(int id)
        {
            var engineer = await Task.Run(() => _repo.GetById(id));
            return engineer == null ? null : _mapper.Map<EngineerDto>(engineer);
        }

        /// <summary>
        /// Нанять нового звукорежиссёра
        /// </summary>
        public async Task<EngineerDto> CreateAsync(CreateEngineerDto dto)
        {
            var existing = _repo.GetAll().FirstOrDefault(e =>
                e.FirstName.ToLower() == dto.FirstName.ToLower() &&
                e.LastName.ToLower() == dto.LastName.ToLower());

            if (existing != null)
                throw new InvalidOperationException("Звукоинженер с таким именем и фамилией уже существует");

            var engineer = _mapper.Map<Engineers>(dto);
            var created = await Task.Run(() => _repo.Create(engineer));
            return _mapper.Map<EngineerDto>(created);
        }

        /// <summary>
        /// Обновить данные звукорежиссёра
        /// </summary>
        public async Task<EngineerDto> UpdateAsync(int id, UpdateEngineerDto dto)
        {
            var engineer = await Task.Run(() => _repo.GetById(id));
            if (engineer == null)
                throw new KeyNotFoundException("Звукорежиссёр не найден");

            var existing = _repo.GetAll().FirstOrDefault(e =>
                e.Id != id &&
                e.FirstName.ToLower() == dto.FirstName.ToLower() &&
                e.LastName.ToLower() == dto.LastName.ToLower());

            if (existing != null)
                throw new InvalidOperationException("Звукорежиссёр с таким именем и фамилией уже существует");

            _mapper.Map(dto, engineer);
            var updated = await Task.Run(() => _repo.Update(engineer));
            return _mapper.Map<EngineerDto>(updated);
        }

        /// <summary>
        /// Уволить звукорежиссёра
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            return await Task.Run(() => _repo.Delete(id));
        }
    }
}