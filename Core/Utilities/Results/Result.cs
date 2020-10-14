using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success,string message):this(success) //tek basşına success contsructor varsa onu da set eder
        {
            Message = message;
            //Success = success; -- bunu 2. kez yazmak yerine :this(success) yazarız, yine message gönderir
        }
        public Result(bool success)
        {
            Success = success;
        }
        public bool Success { get; }

        public string Message { get; }
    }
}
