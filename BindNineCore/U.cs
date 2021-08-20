using System;

namespace BindNineCore
{
    /// <summary>
    /// Non-extension utilities
    /// </summary>
    public static class U
    {
        public static string Env(string key, string defaultValue = null) 
            => Environment.GetEnvironmentVariable(key) ?? defaultValue ?? throw new Exception("Env not found: " + key);
    }
}