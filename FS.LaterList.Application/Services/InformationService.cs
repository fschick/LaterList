using FS.LaterList.IoC.Interfaces.Application.Services;
using System.Diagnostics;
using System.Reflection;

namespace FS.LaterList.Application.Services
{
    public class InformationService : IInformationService
    {
        private static readonly Assembly _entryAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();

        public string GetProductName()
            => FileVersionInfo.GetVersionInfo(_entryAssembly.Location).ProductName;

        public string GetProductVersion()
            => FileVersionInfo.GetVersionInfo(_entryAssembly.Location).ProductVersion;
    }
}
