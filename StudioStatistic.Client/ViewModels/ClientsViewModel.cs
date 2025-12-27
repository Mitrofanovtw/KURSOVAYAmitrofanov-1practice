using CommunityToolkit.Mvvm.ComponentModel;
using StudioStatistic.Client.Models.DTO;
using StudioStatistic.Client.Services;
using System.Collections.ObjectModel;

namespace StudioStatistic.Client.ViewModels
{
    public partial class ClientsViewModel : ObservableObject
    {
        private readonly ApiService _apiService;

        public ObservableCollection<ClientDto> Clients { get; } = new();

        public ClientsViewModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task LoadClientsAsync()
        {
            var clients = await _apiService.GetClientsAsync();
            Clients.Clear();
            foreach (var c in clients)
                Clients.Add(c);
        }
    }
}