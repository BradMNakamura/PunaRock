using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Puna_Rock.Models;
using Microsoft.AspNetCore.Hosting;

namespace Puna_Rock.Services
{
    public class JsonFileLoaderReplacementsService
    {
        public JsonFileLoaderReplacementsService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        public IWebHostEnvironment WebHostEnvironment { get; }
        private string JsonFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "loaderReplacements.json");
        public IEnumerable<LoaderReplacements> GetList()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            return JsonSerializer.Deserialize<LoaderReplacements[]>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

        }
    }
}
