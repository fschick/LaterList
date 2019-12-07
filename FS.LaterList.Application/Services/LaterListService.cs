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

        public TodoItem UpdateTodoItem(TodoItem todoItem)
        {
            var origin = _laterListRepository.FirstOrDefault(
                select: (TodoItem x) => x,
                where: x => x.Id == todoItem.Id
            );

            origin.Title = todoItem.Title;
            origin.Status = todoItem.Status;

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
                    var todoListItem = new TodoItem { Title = $"{namePrefix}Item {listItem}" };
                    todoList.Items.Add(todoListItem);
                }

                todoLists.Add(todoList);
            }

            todoLists = _laterListRepository.AddRange(todoLists);
            return todoLists;
        }
    }
}
