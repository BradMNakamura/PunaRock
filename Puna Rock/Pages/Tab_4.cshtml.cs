using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Puna_Rock.Pages
{
    public class Tab_4Model : PageModel
    {
        private readonly ILogger<Tab_4Model> _logger;

        public Tab_4Model(ILogger<Tab_4Model> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
