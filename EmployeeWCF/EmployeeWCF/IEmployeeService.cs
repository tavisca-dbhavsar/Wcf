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
            void CreateEmployee(Employee employee);

            [OperationContract]
            void AddRemarksById(int id,string remark);
            
        }
        [ServiceContract]
        public interface IRetrieve
        {
            [OperationContract]
            List<Employee> GetAllEmployees();

            [OperationContract(Name = "SearchById")]
            Employee GetEmployeeDetails(int id);

            [OperationContract(Name = "SearchByName")]
            List<Employee> GetEmployeeDetails(string name);
           
        }

        [DataContract]
        public class Employee
        {
            [DataMember]
            public int Id;

            [DataMember]
            public string Name { get; set; }

            [DataMember]
            public DateTime RemarkDate { get; set; }

            [DataMember]
            public string RemarkText { get; set; }
        }

      

}
