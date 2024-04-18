using AR_Navigation.Pages.Buildings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AR_Navigation.Pages.Profile
{
    public class MyprofileModel : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<MyprofileModel> _logger;
        public MyprofileModel(IWebHostEnvironment webHostEnvironment, ILogger<MyprofileModel> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
      
    }
}
