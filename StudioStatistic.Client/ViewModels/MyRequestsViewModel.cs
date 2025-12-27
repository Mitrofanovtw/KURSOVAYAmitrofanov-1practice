using CommunityToolkit.Mvvm.ComponentModel;
using StudioStatistic.Client.Models.DTO;
using StudioStatistic.Client.Services;
using System.Collections.ObjectModel;

namespace StudioStatistic.Client.ViewModels
{
    public partial class MyRequestsViewModel : ObservableObject
    {
        private readonly ApiService _apiService;

        public ObservableCollection<RequestDto> Requests { get; } = new();

        public MyRequestsViewModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task LoadRequestsAsync()
        {
            var requests = await _apiService.GetMyRequestsAsync();
            Requests.Clear();
            foreach (var r in requests)
                Requests.Add(r);
        }
    }
}