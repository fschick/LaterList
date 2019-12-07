using Microsoft.AspNetCore.Components;
using System.Net.Http;

namespace FS.LaterList.UI.Blazor.Base
{
    public class LaterListComponent : ComponentBase
    {
        [Inject] protected HttpClient HttpClient { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }
    }
}
