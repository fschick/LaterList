using System;

namespace FS.LaterList.Common.Interfaces
{
    public interface IModel
    {
        Guid Id { get; set; }
        DateTime Created { get; set; }
        DateTime Modified { get; set; }
    }
}
