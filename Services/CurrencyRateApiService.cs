using CurrencyProject.Api;
using CurrencyProject.Data.DTOs;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyProject.Services
{
    public class CurrencyRateApiService
    {
        private const string baseUrl = RatesExchangeServices.tableADataUrl;

        public async Task<RootDTO> GetRootDTOAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(baseUrl);
                
                if(response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var rootDto = JsonConvert.DeserializeObject<RootDTO>(responseContent);
                    return rootDto;
                }
                return null;
            }
        }
    }
}
