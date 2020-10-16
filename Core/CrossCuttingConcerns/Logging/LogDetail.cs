using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogDetail
    {
        public string MethodName { get; set; }//çalıştırılan method adı
        public List<LogParameter> LogParameters { get; set; }//bu metodun 1 den fazla parametresi olabilir.
    }
}
