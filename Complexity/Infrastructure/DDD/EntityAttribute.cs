using System;

namespace Infrastructure.DDD
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class EntityAttribute : Attribute
    {
    }
}