using FS.LaterList.Common.Enums;
using FS.LaterList.Common.Models;
using FS.LaterList.Common.Routing;
using FS.LaterList.UI.Blazor.Base;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace FS.LaterList.UI.Blazor.Components
{
    public class TodoItemRowController : LaterListComponent
    {
        [Parameter] public TodoItem TodoItem { get; set; }

        protected async Task UpdateTodoItem()
            => TodoItem = await HttpClient.PutJsonAsync<TodoItem>(Routes.LaterList.UpdateTodoItem, TodoItem);

        protected void SetItemStatus(bool isDone)
            => TodoItem.Status = isDone
                ? TodoItemStatus.Done
                : TodoItemStatus.Open;
    }
}
