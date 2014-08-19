using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EmployeeWCF
{
    [DataContract]
    public class FaultExceptionContract
    {
        [DataMember]
        public string StatusCode { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
