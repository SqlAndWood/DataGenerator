using System;
using System.Collections.Generic;
using System.Configuration;

namespace DG
{
    static class Config
    {
        public static string GetValue(string paramName)
        {
            return String.Format(ConfigurationManager.AppSettings[paramName]);
        }
    }
}