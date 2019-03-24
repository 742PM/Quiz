using System;

namespace Domain
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class|AttributeTargets.Struct)]
    public class MustBeSavedAttribute : Attribute
    {
    }
}
