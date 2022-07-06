using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Puna_Rock.Services;
using Puna_Rock.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Puna_Rock.Pages
{
    public class Tab_3Model : PageModel
    {
        private readonly ILogger<Tab_3Model> _logger;
        public JsonFileSafetyCheckService SafetyCheckService;
        public IEnumerable<SafetyCheck> SafetyCheck { get; private set; }

        public JsonFileEquipmentService EquipmentService;
        public IEnumerable<Equipment> Equipment { get; private set; }

        public Tab_3Model(ILogger<Tab_3Model> logger,
            JsonFileSafetyCheckService safetyCheckService,
            JsonFileEquipmentService equipmentService)
        {
            _logger = logger;
            SafetyCheckService = safetyCheckService;
            EquipmentService = equipmentService;
        }

        public void OnGet()
        {
            SafetyCheck = SafetyCheckService.GetQuestions();
            Equipment = EquipmentService.GetEquip();
        }

        public void OnPost()
        {

        }
    }
}
