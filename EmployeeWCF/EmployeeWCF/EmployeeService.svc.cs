using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EmployeeWCF
{

    public class EmployeeService : IAddandCreate,IRetrieve
    {
        private  List<Employee> _EmployeeList = new List<Employee>();
        private static int id = 0;
        public void CreateEmployee(Employee employee)
        {
            id++;
            employee.Id = id;
            _EmployeeList.Add(employee);

        }

        public void AddRemarksById(int id,string remark)
        {
            
            int index = _EmployeeList.FindIndex(x => x.Id == id);
            if (index >= 0)
            {
                _EmployeeList[index].RemarkText = remark;
            }
            else
            {
                Console.WriteLine("Id does not exist");
            }
            
        }

        public List<Employee> GetAllEmployees()
        {
          
            return _EmployeeList;
        }

        public Employee GetEmployeeDetails(int id)
        {
            return this._EmployeeList.FirstOrDefault(x => x.Id == id);
        }

        public List<Employee> GetEmployeeDetails(string name)
        {
           return _EmployeeList.Where(x => x.Name == name).Select(s=>s).ToList();
         
        }
    }
}
