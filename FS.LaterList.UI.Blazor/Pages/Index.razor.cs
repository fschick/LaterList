using FS.LaterList.Common.Models;
using FS.LaterList.Common.Routing;
using FS.LaterList.UI.Blazor.Base;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FS.LaterList.UI.Blazor.Pages
{
    public class IndexController : LaterListPage
    {
        protected IEnumerable<TodoList> TodoLists = Enumerable.Empty<TodoList>();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            TodoLists = await HttpClient.GetJsonAsync<IEnumerable<TodoList>>(Routes.LaterList.GetTodoLists);
        }
    }
}
