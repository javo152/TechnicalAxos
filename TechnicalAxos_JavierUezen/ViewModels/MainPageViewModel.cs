using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TechnicalAxos_JavierUezen.Models;
using TechnicalAxos_JavierUezen.Services;

namespace TechnicalAxos_JavierUezen.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private ICountryService _countryService;

        #region ObservableProperties

        [ObservableProperty]
        bool isBusy;

        [ObservableProperty]
        string packageName;

        [ObservableProperty]
        ImageSource imageSource;

        public ObservableCollection<Country> Countries { get; } = new();

        #endregion

        public MainPageViewModel(ICountryService countryService)
        {
            _countryService = countryService;

            PackageName = AppInfo.Current.PackageName;
            ImageSource = "https://random.dog/af70ad75-24af-4518-bf03-fec4a997004c.jpg";

            Task.Run(GetCountries);
        }

        [RelayCommand]
        private async Task PickImage()
        {
            var imageResult = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Select an image"
            });

            if (imageResult != null)
            {
                var stream = await imageResult.OpenReadAsync();
                ImageSource = ImageSource.FromStream(() => stream);
            }

            OnPropertyChanged(nameof(ImageSource));
        }

        private async Task GetCountries()
        {
            try
            {
                IsBusy = true;

                var countryList = await _countryService.GetCountries();

                Countries.Clear();

                countryList.ForEach(Countries.Add);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Accept");
            }
            finally
            {
                IsBusy = false;
            }

        }

    }
}
