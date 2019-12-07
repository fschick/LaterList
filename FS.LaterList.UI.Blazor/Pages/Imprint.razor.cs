using FS.LaterList.Common.Routing;
using FS.LaterList.UI.Blazor.Base;
using System.Threading.Tasks;

namespace FS.LaterList.UI.Blazor.Pages
{
    public class ImprintController : LaterListPage
    {
        protected string Imprint = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Imprint = await HttpClient.GetStringAsync(Routes.Information.GetImprint);
        }
    }
}
