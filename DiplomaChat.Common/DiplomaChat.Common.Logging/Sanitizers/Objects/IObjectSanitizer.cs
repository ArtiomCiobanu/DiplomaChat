﻿using System;

namespace DiplomaChat.Common.Logging.Sanitizers.Objects
{
    public interface IObjectSanitizer
    {
        public string GetSanitizedJson(object value);
        public string GetSanitizedJson(object value, Type valueType);
    }
}
