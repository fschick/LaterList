using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FS.LaterList.Common.Models;
using FS.LaterList.Common.Routing;
using FS.LaterList.IoC.Interfaces.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FS.LaterList.Api.REST.Controllers
{
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoListService _todoListService;

        public TodoListController(ITodoListService laterListService)
            => _todoListService = laterListService;

        [HttpGet(Routes.TodoList.GetTodoLists)]
        public IEnumerable<TodoList> GetTodoLists()
            => _todoListService.GetTodoLists();

        [HttpGet(Routes.TodoList.GetTodoList + "/{todoListId:guid}")]
        public TodoList GetList([Required, FromRoute]Guid todoListId)
            => _todoListService.GetTodoList(todoListId);

        [HttpPost(Routes.TodoList.CreateTodoList)]
        public TodoList CreateTodoList([Required, FromBody]TodoList todoList)
            => _todoListService.CreateTodoList(todoList);

        [HttpPut(Routes.TodoList.UpdateTodoList)]
        public TodoList UpdateTodoList([Required, FromBody]TodoList todoList)
            => _todoListService.UpdateTodoList(todoList);

        [HttpDelete(Routes.TodoList.RemoveTodoList + "/{todoListId:guid}")]
        public void RemoveTodoList([Required, FromRoute]Guid todoListId)
            => _todoListService.RemoveTodoList(todoListId);

        [HttpPost(Routes.TodoList.GenerateDemoTodoLists)]
        public List<TodoList> GenerateDemoTodoLists(string namePrefix = "Demo ", int listCount = 3, int maxListItemsCount = 5)
            => _todoListService.GenerateDemoTodoLists(namePrefix, listCount, maxListItemsCount);
    }
}