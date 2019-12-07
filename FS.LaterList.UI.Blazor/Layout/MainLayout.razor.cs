using FS.LaterList.Common.Routing;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace FS.LaterList.UI.Blazor.Layout
{
    public class MainLayoutController : LayoutComponentBase
    {
        protected string ProductName = string.Empty;
        protected string ProductVersion = string.Empty;

        [Inject] protected HttpClient HttpClient { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            ProductName = await HttpClient.GetStringAsync(Routes.Information.GetProductName);
            ProductVersion = await HttpClient.GetStringAsync(Routes.Information.GetProductVersion);
        }
    }
}
