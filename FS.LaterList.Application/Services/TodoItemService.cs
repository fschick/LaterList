using System;
using System.Collections.Generic;
using FS.LaterList.Common.Models;
using FS.LaterList.IoC.Interfaces.Application.Services;
using FS.LaterList.IoC.Interfaces.Repository.SQLite.Repositories;

namespace FS.LaterList.Application.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ILaterListRepository _laterListRepository;

        public TodoItemService(ILaterListRepository laterListRepository)
            => _laterListRepository = laterListRepository;

        public TodoItem CreateTodoItem(Guid todoListId, TodoItem todoItem)
        {
            var originTodoList = _laterListRepository.FirstOrDefault(
                select: (TodoList x) => x,
                where: x => x.Id == todoListId
            );

            if (originTodoList == null)
                throw new KeyNotFoundException($"Could not find a TodoList with id {todoListId}.");

            var dbTodoItem = new TodoItem
            {
                Title = todoItem.Title,
                Status = todoItem.Status,
                Priority = todoItem.Priority,
                DueDate = todoItem.DueDate
            };

            originTodoList.Items.Add(dbTodoItem);
            _laterListRepository.Update(originTodoList);
            return dbTodoItem;
        }

        public TodoItem UpdateTodoItem(TodoItem todoItem)
        {
            var origin = _laterListRepository.FirstOrDefault(
                select: (TodoItem x) => x,
                where: x => x.Id == todoItem.Id
            );

            if (origin == null)
                throw new KeyNotFoundException($"Could not find a TodoList item with id {todoItem.Id}.");

            origin.Title = todoItem.Title;
            origin.Status = todoItem.Status;
            origin.Priority = todoItem.Priority;
            origin.DueDate = todoItem.DueDate;

            return _laterListRepository.Update(origin);
        }

        public void RemoveTodoItem(Guid todoItemId)
        {
            var origin = _laterListRepository
                .FirstOrDefault(
                    select: (TodoItem x) => x,
                    where: x => x.Id == todoItemId
                );

            _laterListRepository.Remove(origin);
        }
    }
}
