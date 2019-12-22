using System.Diagnostics.CodeAnalysis;

namespace FS.LaterList.Common.Routing
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class Routes
    {
        public const string BASE_URL = "/"; // Set to null to use relative path.
        public const string ROUTE_PREFIX = BASE_URL + "api";

        public class Information
        {
            private const string ROOT = ROUTE_PREFIX + "/" + nameof(Information) + "/";

            public const string GetProductName = ROOT + nameof(GetProductName);
            public const string GetProductVersion = ROOT + nameof(GetProductVersion);
            public const string GetCopyright = ROOT + nameof(GetCopyright);
            public const string GetImprint = ROOT + nameof(GetImprint);
            public const string GetPrivacyPolicy = ROOT + nameof(GetPrivacyPolicy);
        }

        public class TodoList
        {
            private const string ROOT = ROUTE_PREFIX + "/" + nameof(TodoList) + "/";

            public const string GetTodoLists = ROOT + nameof(GetTodoLists);
            public const string GetTodoList = ROOT + nameof(GetTodoList);
            public const string CreateTodoList = ROOT + nameof(CreateTodoList);
            public const string UpdateTodoList = ROOT + nameof(UpdateTodoList);
            public const string RemoveTodoList = ROOT + nameof(RemoveTodoList);
            public const string GenerateDemoTodoLists = ROOT + nameof(GenerateDemoTodoLists);
        }

        public class TodoItem
        {
            private const string ROOT = ROUTE_PREFIX + "/" + nameof(TodoItem) + "/";

            public const string CreateTodoItem = ROOT + nameof(CreateTodoItem);
            public const string UpdateTodoItem = ROOT + nameof(UpdateTodoItem);
            public const string RemoveTodoItem = ROOT + nameof(RemoveTodoItem);
        }
    }
}
