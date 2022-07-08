using System.Text.Json;
using System.Text.Json.Serialization;

namespace Puna_Rock.Models
{
    public class Equipment
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => JsonSerializer.Serialize<Equipment>(this);
    }
}
