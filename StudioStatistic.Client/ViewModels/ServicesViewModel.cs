using CommunityToolkit.Mvvm.ComponentModel;
using StudioStatistic.Client.Models.DTO;
using StudioStatistic.Client.Services;
using System.Collections.ObjectModel;

namespace StudioStatistic.Client.ViewModels
{
    public partial class ServicesViewModel : ObservableObject
    {
        private readonly ApiService _apiService;

        public ObservableCollection<ServiceDto> Services { get; } = new();

        public ServicesViewModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task LoadServicesAsync()
        {
            var services = await _apiService.GetServicesAsync();
            Services.Clear();
            foreach (var s in services)
                Services.Add(s);
        }
    }
}