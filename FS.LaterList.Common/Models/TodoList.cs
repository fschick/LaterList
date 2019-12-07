using FS.LaterList.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace FS.LaterList.Common.Models
{
    public class TodoList : IModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public bool IsPrivate { get; set; }

        public List<TodoItem> Items { get; set; } = new List<TodoItem>();

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
