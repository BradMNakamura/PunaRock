using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Puna_Rock.Services;
using Puna_Rock.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Dynamic;
using SheetsQuickstart;
using System.Collections.Generic;

namespace Puna_Rock.Controllers
{
    public class HomeController : Controller
    {
        private string spreadsheetId = "19ZzHAu0oKC68hdrAc2uDYlj4MH4HRZSdIaPnblsXg70";
        private string worksheetName = "Sheet1";
        private string worksheetName2 = "ScaleTickets";

        private readonly ILogger<HomeController> _logger;

        public JsonFileSafetyCheckService SafetyCheckService;

        public JsonFileEquipmentService EquipmentService;

        public JsonFileScaleTicketsService ScaleTicketsService;
        public HomeController(ILogger<HomeController> logger, JsonFileSafetyCheckService safetyCheckService,
                JsonFileEquipmentService equipmentService, JsonFileScaleTicketsService scaleTicketsService)
        {
            _logger = logger;
            SafetyCheckService = safetyCheckService;
            EquipmentService = equipmentService;
            ScaleTicketsService = scaleTicketsService;
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
            var ScaleTickets = ScaleTicketsService.GetList();
            dynamic model = new ExpandoObject();
            model.ScaleTickets = ScaleTickets;
            return View(model);
        }
        [HttpPost]
        public IActionResult ScaleTickets(IFormCollection form)
        {
            GoogleSheets sheet = new GoogleSheets();
            IList<IList<object>> sheetsValues = new List<IList<object>>();
            decimal gWeight = 0;
            decimal tWeight = 0;
            decimal nWeight = 0;
            foreach (var item in form)
            {               
                if (item.Key.ToString()!="submit" && item.Key.ToString()!= "__RequestVerificationToken")
                {
                    sheetsValues.Add(new List<object>());
                    sheetsValues[0].Add(item.Value[0].ToString());
                    sheetsValues.Add(new List<object>());                  
                    if(item.Value.Count > 1)
                    {
                        sheetsValues[0].Add(item.Value[1].ToString());
                    }
                    else if(item.Key.ToString()=="grossWeight")
                    {
                        gWeight = System.Convert.ToDecimal(item.Value[0].ToString());
                    }
                    else if (item.Key.ToString() == "tareWeight")
                    {
                        tWeight = System.Convert.ToDecimal(item.Value[0].ToString());

                        nWeight = gWeight - tWeight;
                        sheetsValues[0].Add(nWeight.ToString());
                    }
                }
            }
            sheet.Append(spreadsheetId, worksheetName2, sheetsValues);
            return RedirectToAction("Index");
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
