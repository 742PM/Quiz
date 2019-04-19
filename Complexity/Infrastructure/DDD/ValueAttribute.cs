using System;

namespace Infrastructure.DDD
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class ValueAttribute : Attribute
    {
    }
}