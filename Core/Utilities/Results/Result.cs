﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success, string message):this(success) // aşağıdaki const da çalışsın diye böyle bir yol izlendi
        {
            Message = message;
        }
        
        // Overloading
        public Result(bool success)
        {
            Success = success;
        }

        public bool Success
        {
            get;
        }

        public string Message
        {
            get;
        }
    }
}
