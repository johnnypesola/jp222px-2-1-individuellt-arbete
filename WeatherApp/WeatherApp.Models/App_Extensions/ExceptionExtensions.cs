using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherApp
{
    public class HandleableExeption : Exception
    {
        public HandleableExeption (string message)
            : base(message)
        {
        }

        public HandleableExeption(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}