﻿using System;
using Push.Foundation.Utilities.Http;

namespace Push.Acumatica.Utility
{
    public static class Extensions
    {
        public static string ToAcumaticaRestDate(this DateTime input)
        {
            return input.ToString("yyyy-MM-ddTHH:mm:ss.fffK")
                        .UrlEncode();
        }
    }
}
