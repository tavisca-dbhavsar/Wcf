using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace EmployeeWCF.CustomException
{
    public class EmployeeAlreadyExist:Exception
    {
        public EmployeeAlreadyExist(string message)
        {
            Console.WriteLine("Employee Already Exist");
        }
      
    }
}