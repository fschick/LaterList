using FS.LaterList.Common.Routing;
using FS.LaterList.UI.Blazor.Base;
using System.Threading.Tasks;

namespace FS.LaterList.UI.Blazor.Pages
{
    public class PrivacyPolicyController : LaterListPage
    {
        protected string PrivacyPolicy = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            PrivacyPolicy = await HttpClient.GetStringAsync(Routes.Information.GetPrivacyPolicy);
        }
    }
}
