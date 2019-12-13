using FS.LaterList.Common.Extensions;
using FS.LaterList.IoC.Interfaces.Application.Services;
using Markdig;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Threading.Tasks;

namespace FS.LaterList.Pages
{
    public class OverviewModel : PageModel
    {
        private const string README_FILE = "README.md";

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IInformationService _informationService;

        public string ReadmeMarkup = string.Empty;
        public string ProductName = string.Empty;
        public string ProductVersion = string.Empty;
        public string Copyright = string.Empty;

        public OverviewModel(IWebHostEnvironment webHostEnvironment, IInformationService informationService)
        {
            _webHostEnvironment = webHostEnvironment;
            _informationService = informationService;
        }

        public async Task OnGet()
        {
            var readmePath = System.IO.Path.Combine(_webHostEnvironment.GetWebRootPath(), README_FILE);
            if (!System.IO.File.Exists(readmePath))
                readmePath = System.IO.Path.Combine(_webHostEnvironment.ContentRootPath, "..", README_FILE);

            using (var httpClient = new HttpClient())
            {
                //var readMeMarkdown = System.IO.File.ReadAllText(readmePath);
                var readMeMarkdown = await httpClient.GetStringAsync("https://raw.githubusercontent.com/fschick/LaterList/master/README.md");
                var pipeline = new MarkdownPipelineBuilder().UseSoftlineBreakAsHardlineBreak().Build();
                ReadmeMarkup = Markdown
                    .ToHtml(readMeMarkdown, pipeline)
                    .Replace("https://laterlist.de", $"{Request.Scheme}://{Request.Host}")
                    .Replace("<a href=", "<a target=\"_blank\" href=");
            }

            ProductName = _informationService.GetProductName();
            ProductVersion = _informationService.GetProductVersion();
            Copyright = _informationService.GetCopyright();
        }
    }
}
