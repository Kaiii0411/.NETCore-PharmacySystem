using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmacySystem.APIIntergration;
using PharmacySystem.APIIntergration.Utilities;
using PharmacySystem.Models;
using PharmacySystem.Models.Common;
using PharmacySystem.Models.Identity;
using PharmacySystem.Models.Request;
using PharmacySystem.Models.ViewModels;
using PharmacySystem.WebAdmin.Models;
using System.Diagnostics;

namespace PharmacySystem.WebAdmin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserApiClient _userApiClient;
        private readonly IRolesApiClient _roleApiClient;
        private readonly IInvoiceApiClient _invoiceApiClient;

        public HomeController(ILogger<HomeController> logger, IUserApiClient userApiClient, IRolesApiClient roleApiClient, IInvoiceApiClient invoiceApiClient)
        {
            _logger = logger;
            _userApiClient = userApiClient;
            _roleApiClient = roleApiClient;
            _invoiceApiClient = invoiceApiClient;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _invoiceApiClient.GetRevenue();
            ViewBag.Percent = data.PercentDifference;
            ViewBag.Today = data.TotalPriceNow;
            ViewBag.Yesterday = data.TotalPriceYesterday;
            return View(data);
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

        private async Task<RoleAssignRequest> GetRoleAssignRequest(Guid id)
        {
            var userObj = await _userApiClient.GetById(id);
            var roleObj = await _roleApiClient.GetAll();
            var roleAssignRequest = new RoleAssignRequest();
            foreach (var role in roleObj.ResultObj)
            {
                roleAssignRequest.Roles.Add(new SelectItem()
                {
                    Id = role.Id.ToString(),
                    Name = role.Name,
                    Selected = userObj.ResultObj.Roles.Contains(role.Name)
                });
            }
            return roleAssignRequest;
        }
    }
}