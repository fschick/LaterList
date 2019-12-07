using FS.LaterList.Common.Enums;
using FS.LaterList.Common.Interfaces;
using System;

namespace FS.LaterList.Common.Models
{
    public class TodoItem : IModel
    {
        public Guid Id { get; set; }

        public Guid TodoListId { get; set; }

        public string Title { get; set; }

        public TodoItemStatus Status { get; set; } = TodoItemStatus.Open;

        public bool IsDone => Status == TodoItemStatus.Done;

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
