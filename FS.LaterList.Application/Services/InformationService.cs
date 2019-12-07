using FS.LaterList.Common.Extensions;
using FS.LaterList.IoC.Interfaces.Application.Services;
using Markdig;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace FS.LaterList.Application.Services
{
    public class InformationService : IInformationService
    {
        private const string IMPRINT_FILE = "legal\\imprint.de.md";
        private const string PRIVACY_POLICY_FILE = "legal\\privacypolicy.de.md";

        private static readonly Assembly _entryAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();

        private readonly IHostEnvironment _hostEnvironment;

        public InformationService(IHostEnvironment hostEnvironment)
            => _hostEnvironment = hostEnvironment;

        public string GetProductName()
            => FileVersionInfo.GetVersionInfo(_entryAssembly.Location).ProductName;

        public string GetProductVersion()
            => FileVersionInfo.GetVersionInfo(_entryAssembly.Location).ProductVersion;

        public string GetCopyright()
            => FileVersionInfo.GetVersionInfo(_entryAssembly.Location).LegalCopyright;

        public string GetImprint()
            => GetFileContent(IMPRINT_FILE);

        public string GetPrivacyPolicy()
            => GetFileContent(PRIVACY_POLICY_FILE);

        private string GetFileContent(string fileName)
        {
            fileName = Path.Combine(_hostEnvironment.GetWebRootPath(), fileName);
            if (!File.Exists(fileName))
                return null;

            var pipeline = new MarkdownPipelineBuilder().UseSoftlineBreakAsHardlineBreak().Build();
            return Markdown.ToHtml(File.ReadAllText(fileName), pipeline);
        }
    }
}
