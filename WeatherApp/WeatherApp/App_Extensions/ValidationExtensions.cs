using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherApp
{
    public static class ValidationExtensions
    {
        // Regexp for recurring validation purposes
        public const string TEXT_FIELD_REGEXP = @"^[0-9a-zA-ZåäöÅÄÖéèÈÉËëáàÁÀ\-_&\.,~\^@()/%\s\!]*$";

        //public const string TEXT_FIELD_REGEXP = @"^[\p{L}\p{N}_.-]*$";
        
    }
}

