using System.Text.Json;
using System.Text.Json.Serialization;


namespace Puna_Rock.Models
{
    public class SafetyCheck
    {
        public string Id { get; set; }
        public string Question { get; set; }

        public override string ToString() => JsonSerializer.Serialize<SafetyCheck>(this);
    }
}
