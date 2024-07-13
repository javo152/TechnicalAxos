using TechnicalAxos_JavierUezen.ViewModels;

namespace TechnicalAxos_JavierUezen
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            BindingContext = mainPageViewModel;
        }
    }

}
