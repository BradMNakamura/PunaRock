﻿using System;
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
        private string spreadsheetId = "1s7asiUgljjTxqMfFQGHAKKOl3oBDGpL6SggzBnen-gM";
        private string worksheetName = "Sheet1";

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
        [HttpPost]
        public IActionResult SafetyCheck(IFormCollection form)
        {
            GoogleSheets sheet = new GoogleSheets();
            IList<IList<object>> sheetsValues = new List<IList<object>>();
            foreach (var item in form)
            {
                if(item.Key.ToString()!="submit" && item.Key.ToString() != "__RequestVerificationToken")
                {
                    sheetsValues.Add(new List<object>());
                    sheetsValues[0].Add(item.Value[0].ToString());
                    sheetsValues.Add(new List<object>());
                    if (item.Value.Count > 1)
                    {
                        sheetsValues[0].Add(item.Value[1].ToString());
                    }
                    else if(item.Key.ToString()!="date" && item.Key.ToString()!="EquipNo")
                    {
                        sheetsValues[0].Add("");
                    }
                }
            }
            sheet.Append(spreadsheetId, worksheetName, sheetsValues);
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
            int i = 0;
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
                        sheetsValues.Add(new List<object>());
                        sheetsValues[0].Add(item.Value[i++].ToString());
                       
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
