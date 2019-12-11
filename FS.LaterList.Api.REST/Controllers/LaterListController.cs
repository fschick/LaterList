﻿using FS.LaterList.Common.Models;
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

        [HttpPost(Routes.LaterList.CreateTodoList)]
        public TodoList CreateTodoList([Required, FromBody]TodoList todoList)
            => _laterListService.CreateTodoList(todoList);

        [HttpPut(Routes.LaterList.UpdateTodoList)]
        public TodoList UpdateTodoList([Required, FromBody]TodoList todoList)
            => _laterListService.UpdateTodoList(todoList);

        [HttpDelete(Routes.LaterList.RemoveTodoList + "/{todoListId:guid}")]
        public void RemoveTodoList([Required, FromRoute]Guid todoListId)
            => _laterListService.RemoveTodoList(todoListId);

        [HttpPost(Routes.LaterList.CreateTodoItem + "/{todoListId:guid}")]
        public TodoItem CreateTodoItem([Required, FromRoute]Guid todoListId, [Required, FromBody]TodoItem todoItem)
            => _laterListService.CreateTodoItem(todoListId, todoItem);

        [HttpPut(Routes.LaterList.UpdateTodoItem)]
        public TodoItem UpdateTodoItem([Required, FromBody]TodoItem todoItem)
            => _laterListService.UpdateTodoItem(todoItem);

        [HttpDelete(Routes.LaterList.RemoveTodoItem + "/{todoItemId:guid}")]
        public void RemoveTodoItem([Required, FromRoute]Guid todoItemId)
            => _laterListService.RemoveTodoItem(todoItemId);

        [HttpPost(Routes.LaterList.GenerateDemoTodoLists)]
        public List<TodoList> GenerateDemoTodoLists(string namePrefix = "Demo ", int listCount = 3, int maxListItemsCount = 5)
            => _laterListService.GenerateDemoTodoLists(namePrefix, listCount, maxListItemsCount);
    }
}