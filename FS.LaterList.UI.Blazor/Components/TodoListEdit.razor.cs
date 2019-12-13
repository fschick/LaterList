using FS.LaterList.Common.Extensions;
using FS.LaterList.Common.Models;
using FS.LaterList.UI.Blazor.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FS.LaterList.UI.Blazor.Components
{
    public class TodoListEditController : LaterListComponent
    {
        protected TodoList InternalTodoList;

        [Inject] private IJSRuntime JsRuntime { get; set; }

        [Parameter] public TodoList TodoList { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> AdditionalAttributes { get; set; }

        [Parameter] public EventCallback<TodoList> OnSave { get; set; }

        protected bool IsNewTodoList => TodoList.Id == Guid.Empty;

        protected string CollapseCardId => (string)AdditionalAttributes.FirstOrDefault(x => x.Key == "id").Value;

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            ResetInternalTodoList();
        }

        protected void ResetInternalTodoList()
            => InternalTodoList = TodoList.JsonClone();

        protected async Task Save()
        {
            await JsRuntime.InvokeVoidAsync("laterlist.collapse", $"#{CollapseCardId}", "hide");
            await OnSave.InvokeAsync(InternalTodoList);
        }
    }
}
