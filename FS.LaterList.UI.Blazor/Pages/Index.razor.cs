using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FS.LaterList.Common.Models;
using FS.LaterList.Common.Routing;
using FS.LaterList.UI.Blazor.Base;
using FS.LaterList.UI.Blazor.Extensions;
using Microsoft.AspNetCore.Components;

namespace FS.LaterList.UI.Blazor.Pages
{
    public class IndexController : LaterListPage
    {
        protected List<TodoList> TodoLists = new List<TodoList>();
        protected TodoList NewTodoList;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            InitializeNewTodoList();
            await LoadData();
        }

        protected void InitializeNewTodoList()
            => NewTodoList = new TodoList();

        protected async Task CreateTodoList(TodoList todoList)
        {
            todoList = await HttpClient.PostJsonAsync<TodoList>(Routes.TodoList.CreateTodoList, todoList);
            NavigationManager.NavigateToPage<LaterList>(todoList.Id);
        }

        protected async Task RemoveTodoList(TodoList todoList)
        {
            await HttpClient.DeleteAsync($"{Routes.TodoList.RemoveTodoList}/{todoList.Id}");
            TodoLists.Remove(todoList);
        }

        private async Task LoadData()
            => TodoLists = await HttpClient.GetJsonAsync<List<TodoList>>(Routes.TodoList.GetTodoLists);
    }
}
