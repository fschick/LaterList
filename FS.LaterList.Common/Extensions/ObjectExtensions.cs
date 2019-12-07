﻿using Newtonsoft.Json;

namespace FS.LaterList.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object obj, Formatting formatting = Formatting.None)
            => JsonConvert.SerializeObject(obj, formatting);
    }
}
