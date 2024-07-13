using TechnicalAxos_JavierUezen.ViewModels;

namespace TechnicalAxos_JavierUezen
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel _viewModel;
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = mainPageViewModel;
        }

        protected override void OnAppearing()
        {
            _viewModel.GetCountriesCommand.Execute(null);
        }
    }

}
