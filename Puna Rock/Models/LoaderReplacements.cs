using System.Text.Json;
using System.Text.Json.Serialization;


namespace Puna_Rock.Models
{
    public class LoaderReplacements
    {
        public string Id { get; set; }
        public string Data { get; set; }    
        public string Type { get; set; }

        public override string ToString() => JsonSerializer.Serialize<LoaderReplacements>(this);
    }
}
