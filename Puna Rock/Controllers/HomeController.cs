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
using System.Security.Claims;

namespace Puna_Rock.Controllers
{
    public class HomeController : Controller
    {
        private string spreadsheetId = "19ZzHAu0oKC68hdrAc2uDYlj4MH4HRZSdIaPnblsXg70";
        private string SafetySheet = "SafetyCheck";
        private string ScaleSheet = "ScaleTickets";
        private string LoaderSheet = "LoaderReplacements";
        //private string spreadsheetId = "1s7asiUgljjTxqMfFQGHAKKOl3oBDGpL6SggzBnen-gM";
        private string worksheetName = "Sheet1";

        

        private readonly ILogger<HomeController> _logger;

        public JsonFileSafetyCheckService SafetyCheckService;

        public JsonFileScaleTicketsService ScaleTicketsService;

        public JsonFileLoaderReplacementsService LoaderReplacementsService;
        public HomeController(ILogger<HomeController> logger, JsonFileSafetyCheckService safetyCheckService,
            JsonFileScaleTicketsService scaleTicketsService, JsonFileLoaderReplacementsService loaderReplacementsService)
        {
            _logger = logger;
            SafetyCheckService = safetyCheckService;
            ScaleTicketsService = scaleTicketsService;
            LoaderReplacementsService = loaderReplacementsService;

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
            var SafetyCheck = SafetyCheckService.GetData();
            dynamic model = new ExpandoObject();
            model.SafetyCheck = SafetyCheck;
            ViewBag.SuccessMessage = null;
            if (User.Identity.IsAuthenticated == true)
            {
                Console.WriteLine(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
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
            sheet.Append(spreadsheetId, ScaleSheet, sheetsValues);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult SafetyCheck(IFormCollection form)
        {
            GoogleSheets sheet = new GoogleSheets();
            IList<IList<object>> sheetsValues = new List<IList<object>>();
            foreach (var item in form)
            {
                if (item.Key.ToString()!="submit" && item.Key.ToString() != "__RequestVerificationToken")
                {
                    sheetsValues.Add(new List<object>());
                    sheetsValues[0].Add(item.Value[0].ToString());
                    sheetsValues.Add(new List<object>());
                    if (item.Value.Count > 1 && item.Value[0] != "Good")
                    {
                        sheetsValues[0].Add(item.Value[1].ToString());
                    }
                    else if(item.Key.ToString()!="date" && item.Key.ToString()!="EquipNo" && item.Key.ToString() != "hourmeter")
                    {
                        sheetsValues[0].Add("");
                    }
                }
            }
            sheet.Append(spreadsheetId, SafetySheet, sheetsValues);
            ViewBag.SuccessMessage = "Success";
            return View();
        }
        public IActionResult LoaderReplacements()
        {
            var LoaderReplacements = LoaderReplacementsService.GetList();
            dynamic model = new ExpandoObject();
            model.LoaderReplacements = LoaderReplacements;
            return View(model);
        }
        [HttpPost]
        public IActionResult LoaderReplacements(IFormCollection form)
        {
            GoogleSheets sheet = new GoogleSheets();
            IList<IList<object>> sheetsValues = new List<IList<object>>();  
            foreach(var item in form)
            {
                if(item.Key.ToString()!="submit" && item.Key.ToString()!="__RequestVerificationToken")
                {
                    sheetsValues.Add(new List<object>());
                    sheetsValues[0].Add(item.Value[0].ToString());
                    sheetsValues.Add(new List<object>());
                    if(item.Value.Count > 1 && item.Value[0] != "Good")
                    {
                        sheetsValues[0].Add(item.Value[1].ToString());
                    }
                    else if (item.Key.ToString()!="date" && item.Key.ToString()!="equipNo" && item.Key.ToString()!="hourMeter" &&
                        item.Key.ToString()!="dateCorrected" && item.Key.ToString()!="tagOut" && item.Key.ToString()!="correctionDesc" &&
                        item.Key.ToString() != "sign" && item.Key.ToString() != "operatorName" && item.Key.ToString() != "dateInspected" && 
                        item.Key.ToString() != "digitalSignature")
                    {
                        sheetsValues[0].Add("");
                    }
                }
            }
            sheet.Append(spreadsheetId, LoaderSheet, sheetsValues);
            return RedirectToAction("Index");
        }

        public IActionResult Placeholder()
        {
            return View();
        }

        public IActionResult TimeSheet()
        {
            return View();
        }
        [HttpPost]
        public IActionResult TimeSheet(IFormCollection form)
        {
            GoogleSheets sheet = new GoogleSheets();
            IList<IList<object>> sheetsValues = new List<IList<object>>();
            foreach (var item in form)
            {   
                // Just to see the pushed info
                Console.WriteLine(item);
                if (item.Key.ToString() != "submit" && item.Key.ToString() != "__RequestVerificationToken")
                {
                    foreach (var temp in item.Value)
                    {
                        if (temp != "")
                        {
                            sheetsValues.Add(new List<object>());
                            sheetsValues[0].Add(item.Value[0].ToString());
                        }
                    }
                }
            }
            sheet.Append(spreadsheetId, worksheetName, sheetsValues);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
