using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Puna_Rock.Pages
{
    public class Tab_5Model : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public Tab_5Model(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
