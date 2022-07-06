using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Puna_Rock.Pages
{
    public class Tab_7Model : PageModel
    {
        private readonly ILogger<Tab_7Model> _logger;

        public Tab_7Model(ILogger<Tab_7Model> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
