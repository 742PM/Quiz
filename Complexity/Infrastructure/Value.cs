using System;

namespace Infrastructure
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Struct)]
    public class ValueAttribute : Attribute
    {
    }
}