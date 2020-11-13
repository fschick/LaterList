using FS.LaterList.Common.Comparer;
using FS.LaterList.Common.Models;
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
            TodoList = await HttpClient.GetJsonAsync<TodoList>($"{Routes.TodoList.GetTodoList}/{TodoListId}");
            StateHasChanged();
        }

        protected async Task UpdateTodoList(TodoList todoList)
            => TodoList = await HttpClient.PutJsonAsync<TodoList>(Routes.TodoList.UpdateTodoList, todoList);

        protected async Task UpdateTodoItem(TodoItem todoItem)
        {
            TodoList.Items.Remove(todoItem);
            var result = await HttpClient.PutJsonAsync<TodoItem>(Routes.TodoItem.UpdateTodoItem, todoItem);
            TodoList.Items.Add(result);
            TodoList.Items.Sort(TodoItemComparer.Default);
        }

        protected async Task DeleteTodoItem(TodoItem todoItem)
        {
            await HttpClient.DeleteAsync($"{Routes.TodoItem.RemoveTodoItem}/{todoItem.Id}");
            TodoList.Items.Remove(todoItem);
            TodoList.Items.Sort(TodoItemComparer.Default);
        }

        protected async Task RemoveTodoList()
        {
            await HttpClient.DeleteAsync($"{Routes.TodoList.RemoveTodoList}/{TodoListId}");
            NavigationManager.NavigateToPage<Index>();
        }

        protected async Task CreateTodoItem()
        {
            var todoItem = new TodoItem { Title = "New Todo" };
            todoItem = await HttpClient.PostJsonAsync<TodoItem>($"{Routes.TodoItem.CreateTodoItem}/{TodoListId}", todoItem);
            TodoList.Items.Add(todoItem);
            TodoList.Items.Sort(TodoItemComparer.Default);
        }
    }
}
