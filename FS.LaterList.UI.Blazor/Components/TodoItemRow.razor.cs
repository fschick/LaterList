using FS.LaterList.Common.Enums;
using FS.LaterList.Common.Models;
using FS.LaterList.UI.Blazor.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Threading.Tasks;

namespace FS.LaterList.UI.Blazor.Components
{
    public class TodoItemRowController : LaterListComponent
    {
        protected EditContext EditContext;
        protected string RowId = Guid.NewGuid().ToString();

        [Parameter] public TodoItem TodoItem { get; set; }
        [Parameter] public EventCallback<TodoItem> OnSave { get; set; }
        [Parameter] public EventCallback<TodoItem> OnRemove { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            EditContext = new EditContext(TodoItem);
        }

        protected async Task SetTodoStatus(TodoItemStatus status)
        {
            TodoItem.Status = TodoItem.Status == status ? TodoItemStatus.Open : status;
            if (EditContext.Validate())
                await Save();
        }

        protected async Task SetTodoTitle(string newTitle)
        {
            TodoItem.Title = newTitle;
            if (EditContext.Validate())
                await Save();
        }

        protected Task Save()
            => OnSave.InvokeAsync(TodoItem);

        protected Task Remove()
            => OnRemove.InvokeAsync(TodoItem);
    }
}
