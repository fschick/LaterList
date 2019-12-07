using FS.LaterList.Common.Extensions;
using FS.LaterList.IoC.Interfaces.Application.Services;
using Markdig;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public OverviewModel(IWebHostEnvironment webHostEnvironment, IInformationService informationService)
        {
            _webHostEnvironment = webHostEnvironment;
            _informationService = informationService;
        }

        public void OnGet()
        {
            var readmePath = System.IO.Path.Combine(_webHostEnvironment.GetWebRootPath(), README_FILE);
            if (!System.IO.File.Exists(readmePath))
                readmePath = System.IO.Path.Combine(_webHostEnvironment.ContentRootPath, "..", README_FILE);

            var pipeline = new MarkdownPipelineBuilder().UseSoftlineBreakAsHardlineBreak().Build();
            ReadmeMarkup = Markdown
                .ToHtml(System.IO.File.ReadAllText(readmePath), pipeline)
                .Replace("https://laterlist.de", $"{Request.Scheme}://{Request.Host}")
                .Replace("<a href=", "<a target=\"_blank\" href=");

            ProductName = _informationService.GetProductName();
            ProductVersion = _informationService.GetProductVersion();
        }
    }
}
