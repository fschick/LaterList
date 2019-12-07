using FS.LaterList.Common.Models;
using FS.LaterList.IoC.Interfaces.Application.Services;
using FS.LaterList.IoC.Interfaces.Repository.SQLite.Repositories;
using System;
using System.Collections.Generic;

namespace FS.LaterList.Application.Services
{
    public class LaterListService : ILaterListService
    {
        private readonly ILaterListRepository _laterListRepository;

        public LaterListService(ILaterListRepository laterListRepository)
            => _laterListRepository = laterListRepository;

        public IEnumerable<TodoList> GetTodoLists()
            => _laterListRepository
                .Get(
                    select: (TodoList x) => x,
                    where: x => !x.IsPrivate
                );

        public TodoList GetTodoList(Guid todoListId)
            => _laterListRepository
                .FirstOrDefault(
                    select: (TodoList x) => x,
                    where: x => x.Id == todoListId,
                    includes: new[] { nameof(TodoList.Items) }
                );

        public TodoList CreateTodoList(TodoList todoList)
        {
            var dbTodoList = new TodoList
            {
                Title = todoList.Title,
                IsPrivate = todoList.IsPrivate
            };

            return _laterListRepository.Add(dbTodoList);
        }

        public TodoList UpdateTodoList(TodoList todoList)
        {
            var origin = GetTodoList(todoList.Id);
            if (origin == null)
                throw new KeyNotFoundException($"Could not find a TodoList with id {todoList.Id}.");

            origin.Title = todoList.Title;
            origin.IsPrivate = todoList.IsPrivate;

            return _laterListRepository.Update(origin);
        }

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

        public List<TodoList> GenerateDemoTodoLists(string namePrefix = "Demo ", int listCount = 3, int maxListItemsCount = 5)
        {
            var todoLists = new List<TodoList>();
            for (var listIndex = 1; listIndex <= listCount; listIndex++)
            {
                var listItemsCount = new Random().Next(1, maxListItemsCount);
                var todoList = new TodoList { Title = $"{namePrefix}List {listIndex}" };

                for (var listItem = 1; listItem <= listItemsCount; listItem++)
                {
                    var todoItem = new TodoItem { Title = $"{namePrefix}Item {listItem}" };
                    todoList.Items.Add(todoItem);
                }

                todoLists.Add(todoList);
            }

            todoLists = _laterListRepository.AddRange(todoLists);
            return todoLists;
        }
    }
}
