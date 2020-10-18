using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogParameter//loglanacak alanın bilgileri yazılır
    {
        public string Name { get; set; }//product nesnesi var name :product
        public object Value { get; set; }//product name Id=1 var değeri : elma
        public string Type { get; set; }//product
    }
}
