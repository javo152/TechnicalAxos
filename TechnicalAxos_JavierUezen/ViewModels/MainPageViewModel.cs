using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
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
        ImageSource? imageSource;

        public ObservableCollection<Country> Countries { get; } = new();

        #endregion

        public MainPageViewModel(ICountryService countryService)
        {
            _countryService = countryService;

            PackageName = GetBundlePackageName();
            ImageSource = "https://random.dog/af70ad75-24af-4518-bf03-fec4a997004c.jpg";
        }

        [RelayCommand]
        private async Task PickImage()
        {
            try
            {
                ImageSource = await PickLocalImage();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Accept");
            }
        }

        [RelayCommand]
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

        protected virtual string GetBundlePackageName()
        {
            return AppInfo.Current.PackageName;
        }

        protected virtual async Task<ImageSource?> PickLocalImage()
        {
            var imageResult = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Select an image"
            });

            if (imageResult != null)
            {
                var stream = await imageResult.OpenReadAsync();
                return ImageSource.FromStream(() => stream);
            }

            return null;
        }

    }
}
