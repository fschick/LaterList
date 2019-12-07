using FS.LaterList.Common.Models;
using FS.LaterList.Common.Routing;
using FS.LaterList.UI.Blazor.Base;
using FS.LaterList.UI.Blazor.Extensions;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FS.LaterList.UI.Blazor.Pages
{
    public class IndexController : LaterListPage
    {
        protected IEnumerable<TodoList> TodoLists = Enumerable.Empty<TodoList>();
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
            todoList = await HttpClient.PostJsonAsync<TodoList>(Routes.LaterList.CreateTodoList, todoList);
            NavigationManager.NavigateToPage<LaterList>(todoList.Id);
        }

        private async Task LoadData()
            => TodoLists = await HttpClient.GetJsonAsync<IEnumerable<TodoList>>(Routes.LaterList.GetTodoLists);
    }
}
