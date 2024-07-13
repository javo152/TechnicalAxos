using System.Net.Http.Json;
using TechnicalAxos_JavierUezen.Models;

namespace TechnicalAxos_JavierUezen.Services
{
    public class CountryService : ICountryService
    {
        HttpClient httpClient;

        public CountryService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<Country>> GetCountries()
        {
            var response = await httpClient.GetAsync("https://restcountries.com/v3.1/all?fields=name,region,subregion,independent");
            if (response?.IsSuccessStatusCode == true && response.Content != null)
            {
                return await response.Content.ReadFromJsonAsync<List<Country>>();
            }
            else
            {
                return new List<Country>();
            }
        }
    }
}
