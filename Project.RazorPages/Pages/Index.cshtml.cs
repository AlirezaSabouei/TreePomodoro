using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Business.Services.Gardens;
using Project.Domain.Entities.Gardens;

namespace Project.RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly GardenServices _gardenServices;
        public Garden Garden { get; set; }

        public IndexModel(ILogger<IndexModel> logger, GardenServices gardenServices)
        {
            _logger = logger;
            _gardenServices = gardenServices;
        }

        public async Task OnGet()
        {
            Garden = await _gardenServices.GetOrCreateGarden();
        }

        public async Task<IActionResult> OnPost()
        {
            Garden = await _gardenServices.SpawnATree();
    
            // Use RedirectToPage() for the Post-Redirect-Get pattern
            return Page();
        }
    }
}
