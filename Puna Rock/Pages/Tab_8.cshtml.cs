using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Puna_Rock.Pages
{
    public class Tab_8Model : PageModel
    {
        private readonly ILogger<Tab_8Model> _logger;

        public Tab_8Model(ILogger<Tab_8Model> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
