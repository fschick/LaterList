using Microsoft.AspNetCore.Components;
using System.Net.Http;

namespace FS.LaterList.UI.Blazor.Base
{
    public abstract class LaterListPage : ComponentBase
    {
        [Inject] protected HttpClient HttpClient { get; set; }
    }
}
