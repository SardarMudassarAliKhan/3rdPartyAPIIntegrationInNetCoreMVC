using _3rdPartyAPIIntegrationInNetCoreMVC.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace _3rdPartyAPIIntegrationInNetCoreMVC.Controllers
{
    public class HolyDayControllerController : Controller
    {
        private readonly IApiRepository _apiRepository;

        public HolyDayControllerController(IApiRepository apiRepository)
        {
            _apiRepository = apiRepository;
        }
        public IActionResult Index()
        {
            var holidays = _apiRepository.GetHolidays("US", "2021");
            return View(holidays);
        }
    }
}
