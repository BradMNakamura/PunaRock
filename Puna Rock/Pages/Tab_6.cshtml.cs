using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Puna_Rock.Pages
{
    public class Tab_6Model : PageModel
    {
        private readonly ILogger<Tab_6Model> _logger;

        public Tab_6Model(ILogger<Tab_6Model> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
