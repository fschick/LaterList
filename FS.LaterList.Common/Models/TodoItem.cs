using FS.LaterList.Common.Enums;
using FS.LaterList.Common.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace FS.LaterList.Common.Models
{
    public class TodoItem : IModel
    {
        public Guid Id { get; set; }

        public Guid TodoListId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public TodoItemStatus Status { get; set; } = TodoItemStatus.Open;

        public TodoItemPriority Priority { get; set; } = TodoItemPriority.None;

        public bool IsOpen => Status == TodoItemStatus.Open;

        public bool IsDone => Status == TodoItemStatus.Done;

        public bool IsCanceled => Status == TodoItemStatus.Canceled;

        public DateTime? DueDate { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
