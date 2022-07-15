using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Puna_Rock.Models;
using Microsoft.AspNetCore.Hosting;

namespace Puna_Rock.Services
{
    public class JsonFileScaleTicketsService
    {
        public JsonFileScaleTicketsService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        public IWebHostEnvironment WebHostEnvironment { get; }
        private string JsonFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "scaleTickets.json");
        public IEnumerable<ScaleTickets> GetList()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            return JsonSerializer.Deserialize<ScaleTickets[]>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

        }
    }
}
