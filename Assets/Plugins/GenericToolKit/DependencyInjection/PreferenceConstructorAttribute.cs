using System;

namespace GenericToolKit.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
    public class PreferenceConstructorAttribute : Attribute
    {
    }
}