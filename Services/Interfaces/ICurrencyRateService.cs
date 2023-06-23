using System.Threading.Tasks;

namespace CurrencyProject.Services.Interfaces
{
    public interface ICurrencyRateService
    {
        Task ImportDataAsync();
    }
}
