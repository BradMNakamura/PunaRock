using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Puna_Rock.Pages
{
    public class Tab_5Model : PageModel
    {
        private readonly ILogger<Tab_5Model> _logger;

        public Tab_5Model(ILogger<Tab_5Model> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
