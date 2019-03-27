using System;

namespace Domain
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Struct)]
    public class ValueAttribute : Attribute
    {
    }
}