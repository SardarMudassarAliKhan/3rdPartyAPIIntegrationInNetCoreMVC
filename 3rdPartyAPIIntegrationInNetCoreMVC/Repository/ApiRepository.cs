using _3rdPartyAPIIntegrationInNetCoreMVC.IRepository;
using _3rdPartyAPIIntegrationInNetCoreMVC.Model;
using System.Net;
using System.Text.Json;

namespace _3rdPartyAPIIntegrationInNetCoreMVC.Repository
{
    public class ApiRepository : IApiRepository
    {
        private readonly HttpClient httpClient;

        public ApiRepository()
        {
            httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://date.nager.at/api/v3/")
            };
        }

        public async Task<List<Holiday>> GetHolidays(string countryCode, string year)
        {
            try
            {
                var apiUrl = string.Format("/api/v2/PublicHolidays/{0}/{1}", year, countryCode);
                var result = new List<Holiday>();
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string stringResponse = await response.Content.ReadAsStringAsync();
                    var holidaysResponse = JsonSerializer.Deserialize<List<Holiday>>(stringResponse, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                    result.AddRange(holidaysResponse);
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new Exception("Data not found.");
                    }
                    else
                    {
                        throw new Exception("Failed to fetch data from the server. Status code: " + response.StatusCode);
                    }
                }

                
                return result;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("HTTP request failed: " + ex.Message);
            }
            catch (JsonException ex)
            {
                throw new Exception("JSON deserialization failed: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
