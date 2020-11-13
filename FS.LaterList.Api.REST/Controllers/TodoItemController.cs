using System;
using System.ComponentModel.DataAnnotations;
using FS.LaterList.Common.Models;
using FS.LaterList.Common.Routing;
using FS.LaterList.IoC.Interfaces.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FS.LaterList.Api.REST.Controllers
{
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;

        public TodoItemController(ITodoItemService todoItemService)
            => _todoItemService = todoItemService;

        [HttpPost(Routes.TodoItem.CreateTodoItem + "/{todoListId:guid}")]
        public TodoItem CreateTodoItem([Required, FromRoute]Guid todoListId, [Required, FromBody]TodoItem todoItem)
            => _todoItemService.CreateTodoItem(todoListId, todoItem);

        [HttpPut(Routes.TodoItem.UpdateTodoItem)]
        public TodoItem UpdateTodoItem([Required, FromBody]TodoItem todoItem)
            => _todoItemService.UpdateTodoItem(todoItem);

        [HttpDelete(Routes.TodoItem.RemoveTodoItem + "/{todoItemId:guid}")]
        public void RemoveTodoItem([Required, FromRoute]Guid todoItemId)
            => _todoItemService.RemoveTodoItem(todoItemId);
    }
}
