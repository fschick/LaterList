﻿using FS.LaterList.Common.Models;
using FS.LaterList.Common.Routing;
using FS.LaterList.UI.Blazor.Base;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace FS.LaterList.UI.Blazor.Pages
{
    public class LaterListController : LaterListPage
    {
        protected TodoList TodoList = new TodoList();

        [Parameter] public Guid TodoListId { get; set; }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);
            TodoList = await HttpClient.GetJsonAsync<TodoList>($"{Routes.LaterList.GetTodoList}/{TodoListId}");
            StateHasChanged();
        }
    }
}