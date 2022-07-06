using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Puna_Rock.Pages
{
    public class Tab_1Model : PageModel
    {
        private readonly ILogger<Tab_1Model> _logger;

        public Tab_1Model(ILogger<Tab_1Model> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
