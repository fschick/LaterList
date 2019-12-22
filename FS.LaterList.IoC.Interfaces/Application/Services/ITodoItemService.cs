using FS.LaterList.Common.Models;
using System;

namespace FS.LaterList.IoC.Interfaces.Application.Services
{
    public interface ITodoItemService
    {
        TodoItem CreateTodoItem(Guid todoListId, TodoItem todoItem);
        TodoItem UpdateTodoItem(TodoItem todoItem);
        void RemoveTodoItem(Guid todoItemId);
    }
}