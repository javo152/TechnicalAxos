using System.Text.Json.Serialization;

namespace TechnicalAxos_JavierUezen.Models
{
    public class Country
    {
        public CountryName? Name { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }

        public bool Independent { get; set; }
    }
}
