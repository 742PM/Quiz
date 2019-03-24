using System;

namespace Domain
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Interface)]
    public class EntityAttribute : Attribute
    {
    }
}