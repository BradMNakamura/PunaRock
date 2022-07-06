using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Puna_Rock.Models;
using Microsoft.AspNetCore.Hosting;


namespace Puna_Rock.Services
{
    public class JsonFileEquipmentService
    {
        public JsonFileEquipmentService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "equipment.json");

        public IEnumerable<Equipment> GetEquip()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            return JsonSerializer.Deserialize<Equipment[]>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
    }
}
