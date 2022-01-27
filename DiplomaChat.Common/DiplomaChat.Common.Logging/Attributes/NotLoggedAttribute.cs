using System;

namespace DiplomaChat.Common.Logging.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property, AllowMultiple = false)]
    public class NotLoggedAttribute : Attribute
    {
    }
}
