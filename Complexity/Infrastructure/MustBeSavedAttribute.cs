using System;

namespace Domain
{
    /// <inheritdoc />
    /// <summary>
    /// This is a flag that means this property or object must be saved somewhere.
    /// It does not matter how, one can create mirror-objects or serialize it by himself.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class|AttributeTargets.Struct)]
    public class MustBeSavedAttribute : Attribute
    {
    }
}
