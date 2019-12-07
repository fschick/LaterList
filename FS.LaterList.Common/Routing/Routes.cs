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
        }

        public class LaterList
        {
            private const string ROOT = ROUTE_PREFIX + "/" + nameof(LaterList) + "/";

            public const string GetTodoLists = ROOT + nameof(GetTodoLists);
            public const string GetTodoList = ROOT + nameof(GetTodoList);
            public const string CreateTodoList = ROOT + nameof(CreateTodoList);
            public const string UpdateTodoList = ROOT + nameof(UpdateTodoList);
            public const string CreateTodoItem = ROOT + nameof(CreateTodoItem);
            public const string UpdateTodoItem = ROOT + nameof(UpdateTodoItem);
            public const string GenerateDemoTodoLists = ROOT + nameof(GenerateDemoTodoLists);
        }
    }
}
