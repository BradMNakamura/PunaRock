using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Puna_Rock.Services;
using Puna_Rock.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Dynamic;

namespace Puna_Rock.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public JsonFileSafetyCheckService SafetyCheckService;

        public JsonFileEquipmentService EquipmentService;
        public HomeController(ILogger<HomeController> logger, JsonFileSafetyCheckService safetyCheckService,
                JsonFileEquipmentService equipmentService)
        {
            _logger = logger;
            SafetyCheckService = safetyCheckService;
            EquipmentService = equipmentService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult SafetyCheck()
        {
            var SafetyCheck = SafetyCheckService.GetQuestions();
            var Equipment = EquipmentService.GetEquip();
            dynamic model = new ExpandoObject();
            model.SafetyCheck = SafetyCheck;
            model.Equipment = Equipment;
            return View(model);
        }
        public IActionResult ScaleTickets()
        {
            dynamic model = new ExpandoObject();
            return View(model);
        }
        public IActionResult Placeholder()
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
