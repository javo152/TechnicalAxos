using Moq;
using TechnicalAxos_JavierUezen.Models;
using TechnicalAxos_JavierUezen.Services;
using TechnicalAxos_JavierUezen.ViewModels;

namespace UnitTest
{
    public class MainPageTests
    {
        Mock<ICountryService> _countryServiceMock;

        [SetUp]
        public void Setup()
        {
            _countryServiceMock = new Mock<ICountryService>();
        }

        public class ModalLessMainPageViewModel : MainPageViewModel
        {
            public ModalLessMainPageViewModel(ICountryService countryService) : base(countryService)
            { }

            protected override string GetBundlePackageName()
            {
                return string.Empty;
            }

            protected override async Task<ImageSource?> PickLocalImage()
            {
                var image = ImageSource.FromUri(new Uri("https://random.dog/"));
                await Task.Delay(1);
                return image;
            }
        }

        [Test]
        public void GetCountries()
        {
            _countryServiceMock
                .Setup(s => s.GetCountries())
                .Returns(Task.FromResult(new List<Country>
                {
                    new Country
                    {
                        Region = "Asia"
                    },
                    new Country
                    {
                        Region = "America"
                    }
                }));

            var viewModel = new ModalLessMainPageViewModel(_countryServiceMock.Object);

            viewModel.GetCountriesCommand.Execute(null);

            Assert.IsTrue(viewModel.Countries.Count == 2);
        }

        [Test]
        public async Task PickImage()
        {
            var viewModel = new ModalLessMainPageViewModel(_countryServiceMock.Object);

            viewModel.PickImageCommand.Execute(null);

            await Task.Delay(10);

            Assert.That(viewModel.ImageSource?.ToString(), Is.EqualTo("Uri: https://random.dog/"));
        }
    }
}