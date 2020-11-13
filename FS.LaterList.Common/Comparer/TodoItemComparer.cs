using System;
using System.Collections.Generic;
using FS.LaterList.Common.Models;

namespace FS.LaterList.Common.Comparer
{
    public class TodoItemComparer : IComparer<TodoItem>
    {
        public static TodoItemComparer Default = new TodoItemComparer();

        public int Compare(TodoItem x, TodoItem y)
        {
            if (ReferenceEquals(x, y))
                return 0;

            if (x == null)
                return -1;

            if (y == null)
                return 0;

            if (x.IsOpen && !y.IsOpen)
                return -1;

            if (!x.IsOpen && y.IsOpen)
                return 1;

            if (x.IsOpen)
                return Comparer<DateTime>.Default.Compare(x.Modified, y.Modified) * -1;
            else
                return Comparer<DateTime>.Default.Compare(x.Modified, y.Modified);
        }
    }
}
