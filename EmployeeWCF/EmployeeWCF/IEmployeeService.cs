using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EmployeeWCF
{
    
    [ServiceContract]
  
        public interface IAddandCreate
        {
            [OperationContract]
            [FaultContract(typeof(FaultExceptionContract))]
            List<Employee> CreateEmployee(Employee employee);

            [OperationContract]
            [FaultContract(typeof(FaultExceptionContract))]
            Employee AddRemarksById(int id,string remark);
            
        }
        [ServiceContract]
        public interface IRetrieve
        {
            [OperationContract]
            [FaultContract(typeof(FaultExceptionContract))]
            List<Employee> GetAllEmployees();

            [OperationContract]
            List<Employee> GetAllEmployeesHavingRemark(string remark);

            [OperationContract(Name = "SearchById")]
            [FaultContract(typeof(FaultExceptionContract))]
            Employee GetEmployeeDetails(int id);

            [OperationContract(Name = "SearchByName")]
            [FaultContract(typeof(FaultExceptionContract))]
            List<Employee> GetEmployeeDetails(string name);

        }

        [DataContract]
        public class Employee
        {
            [DataMember]
            public int Id {get;set;}

            [DataMember]
            public string Name { get; set; }

            [DataMember]
            public DateTime RemarkDate { get; set; }

            [DataMember]
            public string RemarkText { get; set; }
        }

  

}
