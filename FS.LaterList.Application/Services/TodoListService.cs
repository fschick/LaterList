using System;
using System.Collections.Generic;
using FS.LaterList.Common.Models;
using FS.LaterList.IoC.Interfaces.Application.Services;
using FS.LaterList.IoC.Interfaces.Repository.SQLite.Repositories;

namespace FS.LaterList.Application.Services
{
    public class TodoListService : ITodoListService
    {
        private readonly ILaterListRepository _laterListRepository;

        public TodoListService(ILaterListRepository laterListRepository)
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

        public void RemoveTodoList(Guid todoListId)
        {
            var origin = GetTodoList(todoListId);
            _laterListRepository.Remove(origin);
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
