using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Puna_Rock.Pages
{
    public class Tab_7Model : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public Tab_7Model(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
