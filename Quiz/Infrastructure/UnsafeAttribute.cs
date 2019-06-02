using System;

namespace Infrastructure
{
    /// <summary>
    ///     Marks method as unsafe to use
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class UnsafeAttribute : Attribute
    {
    }
}