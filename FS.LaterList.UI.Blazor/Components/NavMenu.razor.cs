using FS.LaterList.UI.Blazor.Base;

namespace FS.LaterList.UI.Blazor.Components
{
    public class NavMenuController : LaterListComponent
    {
        protected bool IsDevelopment =
#if DEBUG
            true;
#else
            false;
#endif
    }
}
