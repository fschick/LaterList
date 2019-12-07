using FS.LaterList.Common.Models;
using FS.LaterList.Common.Routing;
using FS.LaterList.IoC.Interfaces.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FS.LaterList.Api.REST.Controllers
{
    [ApiController]
    public class LaterListController : ControllerBase
    {
        private readonly ILaterListService _laterListService;

        public LaterListController(ILaterListService laterListService)
            => _laterListService = laterListService;

        [HttpGet(Routes.LaterList.GetTodoLists)]
        public IEnumerable<TodoList> GetTodoLists()
            => _laterListService.GetTodoLists();

        [HttpGet(Routes.LaterList.GetTodoList + "/{todoListId:guid}")]
        public TodoList GetList([Required, FromRoute]Guid todoListId)
            => _laterListService.GetTodoList(todoListId);

        [HttpPut(Routes.LaterList.UpdateTodoItem)]
        public TodoItem UpdateTodoItem([FromBody]TodoItem todoItem)
            => _laterListService.UpdateTodoItem(todoItem);

        [HttpPost(Routes.LaterList.GenerateDemoTodoLists)]
        public List<TodoList> GenerateDemoTodoLists(string namePrefix = "Demo ", int listCount = 3, int maxListItemsCount = 5)
            => _laterListService.GenerateDemoTodoLists(namePrefix, listCount, maxListItemsCount);
    }
}