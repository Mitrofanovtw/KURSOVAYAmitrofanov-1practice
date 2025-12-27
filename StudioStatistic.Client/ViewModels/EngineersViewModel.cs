using CommunityToolkit.Mvvm.ComponentModel;
using StudioStatistic.Client.Models.DTO;
using StudioStatistic.Client.Services;
using System.Collections.ObjectModel;

namespace StudioStatistic.Client.ViewModels
{
    public partial class EngineersViewModel : ObservableObject
    {
        private readonly ApiService _apiService;

        public ObservableCollection<EngineerDto> Engineers { get; } = new();

        public EngineersViewModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task LoadEngineersAsync()
        {
            var engineers = await _apiService.GetEngineersAsync();
            Engineers.Clear();
            foreach (var e in engineers)
                Engineers.Add(e);
        }
    }
}