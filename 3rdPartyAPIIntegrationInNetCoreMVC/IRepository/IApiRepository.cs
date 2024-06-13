using _3rdPartyAPIIntegrationInNetCoreMVC.Model;

namespace _3rdPartyAPIIntegrationInNetCoreMVC.IRepository
{
    public interface IApiRepository
    {
        Task<List<Holiday>> GetHolidays(string countryCode, string Year);
    }
}
