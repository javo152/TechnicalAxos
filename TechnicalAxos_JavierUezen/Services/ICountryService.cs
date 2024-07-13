using TechnicalAxos_JavierUezen.Models;

namespace TechnicalAxos_JavierUezen.Services
{
    public interface ICountryService
    {
        Task<List<Country>> GetCountries();
    }
}
