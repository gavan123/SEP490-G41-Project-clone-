using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AR_Navigation.Pages.Buildings
{
    public class DetailModel : PageModel
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<listModel> _logger;


        public DetailModel(IWebHostEnvironment webHostEnvironment, ILogger<listModel> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        [BindProperty]
        public int FloorId { get; set; }
        [BindProperty]
        public IFormFile ImageFile { get; set; }
        public async Task<IActionResult> OnPostAddOrEditMapAsync()
        {

            string imagesDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "Images");

            string uniqueFileName = null;
            if (ImageFile != null && ImageFile.Length > 0)
            {
                uniqueFileName = ImageFile.FileName;

                string filePath = Path.Combine(imagesDirectory, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }
            }
            _logger.LogInformation($"Images directory: {imagesDirectory}");


            if (FloorId != 0)
            {
                return RedirectToPage("/Building/detail/" + FloorId);
            }
            else
            {
                return Page();
            }
        }
    }
}
