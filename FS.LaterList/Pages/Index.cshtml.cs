using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;

namespace FS.LaterList.Pages
{
    public class IndexController : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public bool IsDevelopment => _webHostEnvironment.IsDevelopment();

        public IndexController(IWebHostEnvironment webHostEnvironment)
            => _webHostEnvironment = webHostEnvironment;
    }
}
