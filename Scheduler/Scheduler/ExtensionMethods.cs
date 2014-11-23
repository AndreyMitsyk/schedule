using System;

namespace Scheduler
{
    public static class ExtensionMethods
    {
        public static TResult Do<T, TResult>(this T o, Func<T, TResult> func, TResult defaultValue = default (TResult))
        {
            return !ReferenceEquals(o, null) && func != null ? func(o) : defaultValue;
        }
    }
}