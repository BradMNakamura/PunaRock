using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Puna_Rock.Pages
{
    public class Tab_2Model : PageModel
    {
        private readonly ILogger<Tab_2Model> _logger;

        public Tab_2Model(ILogger<Tab_2Model> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
