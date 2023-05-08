using CurrencyProject.Data;
using System.Threading.Tasks;


namespace CurrencyProject.Services
{
    public class CurrencyRateService
    {
        private readonly CurrencyRateApiService _currencyRateApiService;
        private readonly CurrencyDbContext _currencyDbContext;

        public CurrencyRateService(CurrencyRateApiService apiservice, CurrencyDbContext dbContext)
        {
            _currencyRateApiService = apiservice;
            _currencyDbContext = dbContext;
        }

        public async Task ImportDataAsync()
        {
            var rootDto = await _currencyRateApiService.GetRootDTOAsync();

            if (rootDto == null)
            {
                return;
            }

            _currencyDbContext.Roots.Add(rootDto);
            foreach(var rateDto in rootDto.rates)
            {
                _currencyDbContext.Rate.Add(rateDto);
            }

            await _currencyDbContext.SaveChangesAsync();
        }
    }
}
