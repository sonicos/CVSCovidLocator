using System;
using System.Collections.Generic;
using System.Text;

namespace CVSCovidLocator
{
    static class Globals
    {
        public const string ZIP_REPLACE = "___ZIP___";
        public static string BasePayload { get; set; }
        public static Dictionary<string, List<Availability>> CurrentAvailability;
        public static object LOCK = new object();
    }
}
