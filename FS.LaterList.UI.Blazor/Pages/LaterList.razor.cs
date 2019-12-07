﻿using FS.LaterList.Common.Models;
using FS.LaterList.Common.Routing;
using FS.LaterList.UI.Blazor.Base;
using FS.LaterList.UI.Blazor.Extensions;
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

        protected async Task UpdateTodoList(TodoList todoList)
            => TodoList = await HttpClient.PutJsonAsync<TodoList>(Routes.LaterList.UpdateTodoList, todoList);

        protected Task UpdateTodoItem(TodoItem todoItem)
            => HttpClient.PutJsonAsync<TodoItem>(Routes.LaterList.UpdateTodoItem, todoItem);

        protected async Task DeleteTodoItem(TodoItem todoItem)
        {
            await HttpClient.DeleteAsync($"{Routes.LaterList.RemoveTodoItem}/{todoItem.Id}");
            TodoList.Items.Remove(todoItem);
        }

        protected async Task RemoveTodoList()
        {
            await HttpClient.DeleteAsync($"{Routes.LaterList.RemoveTodoList}/{TodoListId}");
            NavigationManager.NavigateToPage<Index>();
        }

        protected async Task CreateTodoItem()
        {
            var todoItem = new TodoItem { Title = "New Todo" };
            todoItem = await HttpClient.PostJsonAsync<TodoItem>($"{Routes.LaterList.CreateTodoItem}/{TodoListId}", todoItem);
            TodoList.Items.Add(todoItem);
        }
    }
}
