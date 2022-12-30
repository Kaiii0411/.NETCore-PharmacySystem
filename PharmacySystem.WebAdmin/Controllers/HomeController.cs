using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmacySystem.APIIntergration;
using PharmacySystem.APIIntergration.Utilities;
using PharmacySystem.Models;
using PharmacySystem.Models.Identity;
using PharmacySystem.Models.ViewModels;
using PharmacySystem.WebAdmin.Models;
using System.Diagnostics;

namespace PharmacySystem.WebAdmin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserApiClient _userApiClient;

        public HomeController(ILogger<HomeController> logger, IUserApiClient userApiClient)
        {
            _logger = logger;
            _userApiClient = userApiClient; 
        }

        public async Task<IActionResult> Index()
        {
            var userName = User.Identity.Name;
            var user = await _userApiClient.GetByName(userName);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}