using System.Text.Json;

namespace Puna_Rock.Models
{
    public class ScaleTickets
    {
        public string Id { get; set; }
        public string Type { get; set; }

        public override string ToString() => JsonSerializer.Serialize<ScaleTickets>(this);
    }
}
