using System;
using System.Collections.Generic;
using FS.LaterList.Common.Comparer;
using FS.LaterList.Common.Enums;
using FS.LaterList.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FS.LaterList.Common.Tests.Comparer
{
    [TestClass]
    public class TodoItemComparerTests
    {
        [DataTestMethod]
        [DynamicData(nameof(TestData))]
        public void WhenTodoItemComparerRuns_ItReturnsExpectedResult(TodoItem x, TodoItem y, int expectedResult)
            => Assert.AreEqual(expectedResult, TodoItemComparer.Default.Compare(x, y));

        public static IEnumerable<object[]> TestData => new[]
        {
            // Same date
            new object[]
            {
                new TodoItem{ Status = TodoItemStatus.Open, Modified = new DateTime(2000, 01, 01)},
                new TodoItem{ Status = TodoItemStatus.Open, Modified = new DateTime(2000, 01, 01)},
                0
            },
            new object[]
            {
                new TodoItem{ Status = TodoItemStatus.Open, Modified = new DateTime(2000, 01, 01)},
                new TodoItem{ Status = TodoItemStatus.Done, Modified = new DateTime(2000, 01, 01)},
                -1
            },
            new object[]
            {
                new TodoItem{ Status = TodoItemStatus.Open, Modified = new DateTime(2000, 01, 01)},
                new TodoItem{ Status = TodoItemStatus.Canceled, Modified = new DateTime(2000, 01, 01)},
                -1
            },
            new object[]
            {
                new TodoItem{ Status = TodoItemStatus.Done, Modified = new DateTime(2000, 01, 01)},
                new TodoItem{ Status = TodoItemStatus.Done, Modified = new DateTime(2000, 01, 01)},
                0
            },
            new object[]
            {
                new TodoItem{ Status = TodoItemStatus.Done, Modified = new DateTime(2000, 01, 01)},
                new TodoItem{ Status = TodoItemStatus.Canceled, Modified = new DateTime(2000, 01, 01)},
                0
            },
            new object[]
            {
                new TodoItem{ Status = TodoItemStatus.Canceled, Modified = new DateTime(2000, 01, 01)},
                new TodoItem{ Status = TodoItemStatus.Canceled, Modified = new DateTime(2000, 01, 01)},
                0
            },

            //// X newer than Y
            new object[]
            {
                new TodoItem{ Status = TodoItemStatus.Open, Modified = new DateTime(2001, 01, 01)},
                new TodoItem{ Status = TodoItemStatus.Open, Modified = new DateTime(2000, 01, 01)},
                -1
            },
            new object[]
            {
                new TodoItem{ Status = TodoItemStatus.Open, Modified = new DateTime(2001, 01, 01)},
                new TodoItem{ Status = TodoItemStatus.Done, Modified = new DateTime(2000, 01, 01)},
                -1
            },
            new object[]
            {
                new TodoItem{ Status = TodoItemStatus.Open, Modified = new DateTime(2001, 01, 01)},
                new TodoItem{ Status = TodoItemStatus.Canceled, Modified = new DateTime(2000, 01, 01)},
                -1
            },
            new object[]
            {
                new TodoItem{ Status = TodoItemStatus.Done, Modified = new DateTime(2001, 01, 01)},
                new TodoItem{ Status = TodoItemStatus.Done, Modified = new DateTime(2000, 01, 01)},
                1
            },
            new object[]
            {
                new TodoItem{ Status = TodoItemStatus.Done, Modified = new DateTime(2001, 01, 01)},
                new TodoItem{ Status = TodoItemStatus.Canceled, Modified = new DateTime(2000, 01, 01)},
                1
            },
            new object[]
            {
                new TodoItem{ Status = TodoItemStatus.Canceled, Modified = new DateTime(2001, 01, 01)},
                new TodoItem{ Status = TodoItemStatus.Canceled, Modified = new DateTime(2000, 01, 01)},
                1
            },

            //// X older than Y
            new object[]
            {
                new TodoItem{ Status = TodoItemStatus.Open, Modified = new DateTime(2000, 01, 01)},
                new TodoItem{ Status = TodoItemStatus.Open, Modified = new DateTime(2001, 01, 01)},
                1
            },
            new object[]
            {
                new TodoItem{ Status = TodoItemStatus.Open, Modified = new DateTime(2000, 01, 01)},
                new TodoItem{ Status = TodoItemStatus.Done, Modified = new DateTime(2001, 01, 01)},
                -1
            },
            new object[]
            {
                new TodoItem{ Status = TodoItemStatus.Open, Modified = new DateTime(2000, 01, 01)},
                new TodoItem{ Status = TodoItemStatus.Canceled, Modified = new DateTime(2001, 01, 01)},
                -1
            },
            new object[]
            {
                new TodoItem{ Status = TodoItemStatus.Done, Modified = new DateTime(2000, 01, 01)},
                new TodoItem{ Status = TodoItemStatus.Done, Modified = new DateTime(2001, 01, 01)},
                -1
            },
            new object[]
            {
                new TodoItem{ Status = TodoItemStatus.Done, Modified = new DateTime(2000, 01, 01)},
                new TodoItem{ Status = TodoItemStatus.Canceled, Modified = new DateTime(2001, 01, 01)},
                -1
            },
            new object[]
            {
                new TodoItem{ Status = TodoItemStatus.Canceled, Modified = new DateTime(2000, 01, 01)},
                new TodoItem{ Status = TodoItemStatus.Canceled, Modified = new DateTime(2001, 01, 01)},
                -1
            },
        };
    }
}
