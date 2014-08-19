
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace EmployeeWCF
{
   // [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public class EmployeeService : IAddandCreate,IRetrieve
    {
        private static List<Employee> EmployeeList = new List<Employee>();

        public List<Employee> CreateEmployee(Employee employee)
        {

            var isIdPresent =EmployeeList.FindIndex(x => x.Id == employee.Id);
            try{
                if (isIdPresent <0 && employee.Name!=string.Empty)
                {
                    EmployeeList.Add(employee);
                    return EmployeeList;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(Exception e){
                FaultExceptionContract fault = new FaultExceptionContract
                {
                    StatusCode = "101",
                    Message = "Employee with Id " + employee.Id + " already exists"
                };
                throw new FaultException<FaultExceptionContract>(fault, "Employee with Id " + employee.Id + " already exists");
            }
        }

        public Employee AddRemarksById(int id,string remark)
        {
            
            int index = EmployeeList.FindIndex(x => x.Id == id);
            try
            {
                if (index >= 0)
                {
                    EmployeeList[index].RemarkText = remark;
                    return EmployeeList[index];
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(Exception e)
            {
                FaultExceptionContract fault = new FaultExceptionContract
                {
                    StatusCode = "101",
                    Message = "Employee with Id " + id + "does not exists"
                };
                throw new FaultException<FaultExceptionContract>(fault, "Employee with Id " + id + "does not exists");
                
            }
            
        }

        public List<Employee> GetAllEmployees()
        {
            try
            {
                if(EmployeeList.Count!=0)
                {
                      return EmployeeList;
                }
                else{
                    throw new Exception();
                }
            }
            catch(Exception e)
            {
                FaultExceptionContract fault = new FaultExceptionContract
                {
                    StatusCode = "101",
                    Message = "No Employee in the list"
                };
                throw new FaultException<FaultExceptionContract>(fault, "No Employee in the list");
            }
        }

        public Employee GetEmployeeDetails(int id)
        {
            var empList=EmployeeList.FirstOrDefault(x => x.Id == id);
            try
            {
                  if(empList.Id!=0)
                  {
                      return empList;
                  }
                  else{
                      throw new Exception();
                  }
            }
            catch(Exception e)
            {
                FaultExceptionContract fault = new FaultExceptionContract
                {
                    StatusCode = "101",
                    Message = "Employee with Id " + id + "does not exists"
                };
                throw new FaultException<FaultExceptionContract>
                (fault, "Employee with Id " + id + "does not exists");
            }
         
        }

        public List<Employee> GetEmployeeDetails(string name)
        {

           List<Employee> listEmployee = EmployeeList.Where(x => x.Name == name).Select(s=>s).ToList();
           try
           {
               if (listEmployee.Count != 0)
               {
                   return listEmployee;
               }
               else
               {
                   throw new Exception();
               }
           }
           catch (Exception e)
           {
               FaultExceptionContract fault = new FaultExceptionContract
               {
                   StatusCode = "101",
                   Message = "Employee with Name " + name + " does not exists"
               };
               throw new FaultException<FaultExceptionContract>(fault, "Employee with Name " + name + " does not exists");
           }
        }

        public List<Employee> GetAllEmployeesHavingRemark(string remark)
        {
          
           List<Employee> listEmployee = EmployeeList.Where(x => x.RemarkText == remark).Select(s => s).ToList();
           try
           {
               if (listEmployee.Count != 0)
               {
                   return listEmployee;
               }
               else
               {
                   throw new Exception();
               }
           }
           catch (Exception e)
           {
               FaultExceptionContract fault = new FaultExceptionContract
               {
                   StatusCode = "101",
                   Message = "Employee with No Remark " + remark + " like this"
               };
               throw new FaultException<FaultExceptionContract>(fault, "Employee with No Remark " + remark + " like this");
           }

           }

        }
    
}
