using FS.LaterList.Common.Models;
using System;
using System.Collections.Generic;

namespace FS.LaterList.IoC.Interfaces.Application.Services
{
    public interface ILaterListService
    {
        IEnumerable<TodoList> GetTodoLists();
        TodoList GetTodoList(Guid todoListId);
        TodoList CreateTodoList(TodoList todoList);
        TodoList UpdateTodoList(TodoList todoList);
        void RemoveTodoList(Guid todoListId);
        TodoItem CreateTodoItem(Guid todoListId, TodoItem todoItem);
        TodoItem UpdateTodoItem(TodoItem todoItem);
        void RemoveTodoItem(Guid todoItemId);
        List<TodoList> GenerateDemoTodoLists(string namePrefix = "Demo ", int listCount = 3, int maxListItemsCount = 5);
    }
}