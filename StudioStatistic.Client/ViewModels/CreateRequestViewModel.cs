using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudioStatistic.Client.Models.DTO;
using StudioStatistic.Client.Services;
using System.Collections.ObjectModel;

namespace StudioStatistic.Client.ViewModels
{
    public partial class CreateRequestViewModel : ObservableObject
    {
        private readonly ApiService _apiService;

        [ObservableProperty]
        private ObservableCollection<ServiceDto> services = new();

        [ObservableProperty]
        private ServiceDto selectedService;

        [ObservableProperty]
        private DateTime selectedDate = DateTime.Today.AddDays(1);

        [ObservableProperty]
        private TimeSpan selectedTime = new TimeSpan(12, 0, 0);

        [ObservableProperty]
        private string description = string.Empty;

        [ObservableProperty]
        private bool isBusy;

        public CreateRequestViewModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task LoadServicesAsync()
        {
            IsBusy = true;
            try
            {
                var list = await _apiService.GetServicesAsync();
                Services.Clear();
                foreach (var s in list)
                    Services.Add(s);
            }
            catch
            {
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task CreateRequest()
        {
            if (SelectedService == null)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Выберите услугу", "OK");
                return;
            }

            var dateTime = SelectedDate.Date + SelectedTime;

            var dto = new CreateRequestDto
            {
                ServiceId = SelectedService.Id,
                DateOfVisit = dateTime,
                Description = Description
            };

            IsBusy = true;
            try
            {
                await _apiService.CreateRequestAsync(dto);
                await Application.Current.MainPage.DisplayAlert("Успех", "Заявка создана", "OK");
                await Shell.Current.GoToAsync("..");
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Не удалось создать заявку", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}