using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using TestEmployee.EmpService;

namespace TestEmployeeWCF
{
    [TestClass]
    public class TestEmployee
    {
        
        //[TestMethod]
        //public void AddEmployee()
        //{
        //    List<Employee> employeeList = new List<Employee>();
        //    Employee employee = new Employee();
        //    employee.Id = 1;
        //    employee.Name = "abc";
        //    employee.RemarkText = "Hello";
        //    employee.RemarkDate = Convert.ToDateTime("2 / 2 / 14");

        //    employeeList.Add(employee);
        //    Assert.AreNotEqual(employeeList[0].Id, 2);
        //}

        [TestMethod]
        public void AddEmployee()
        {
           
            Employee employee = new Employee();
            employee.Id = 1;
            employee.Name = "abc";
            employee.RemarkText = "Hi";
            employee.RemarkDate = Convert.ToDateTime("2 / 2 / 14");
           
            EmployeeService employeeService = new EmployeeService();
            employeeService.CreateEmployee(employee);


            Assert.AreEqual(employee.Id, 1);
        }


        [TestMethod]
        public void RetrieveEmployees()
        {

          
            EmployeeService employeeService = new EmployeeService();
            List<Employee> employeeList = employeeService.GetAllEmployees();
            foreach (var emp in employeeList)
            {
                Debug.Write("Employee Id " + emp.Id);
                Debug.Write("\n");
                Debug.Write("Employee Name" + emp.Name); Debug.Write("\n");
                Debug.Write("Employee Remark Date " + emp.RemarkDate); Debug.Write("\n");
                Debug.Write("Employee Remark Text" + emp.RemarkText); Debug.Write("\n");
            }

           Assert.AreEqual(1, employeeList.Count);
        }



        [TestMethod]
        public void RetrieveEmployeeById()
        {
            
            Employee employee = new Employee();
            employee.Id = 2;
            employee.Name = "xyz";
            employee.RemarkText = "Hello";
            employee.RemarkDate = Convert.ToDateTime("2 / 2 / 14");
            EmployeeService employeeService = new EmployeeService();
            employeeService.CreateEmployee(employee);

            Employee emp= employeeService.GetEmployeeDetails(1);
            Assert.AreEqual("abc", emp.Name);
        }

        [TestMethod]
        public void RetrieveEmployeeByName()
        {
            EmployeeService employeeService = new EmployeeService();
            List<Employee> emp = employeeService.GetEmployeeDetails("abc");
            Assert.AreEqual("abc", emp[0].Name);
        }

        [TestMethod]
        [ExpectedException (typeof(EmployeeWCF.CustomException.EmployeeAlreadyExist))]
        public void AddEmployeeAgain()
        {
          
            Employee employee = new Employee();
            employee.Id = 2;
            employee.Name = "xyz";
            employee.RemarkText = "Hello";
            employee.RemarkDate = Convert.ToDateTime("2 / 2 / 14");

            EmployeeService employeeService = new EmployeeService();
            employeeService.CreateEmployee(employee);
           
        }

        [TestMethod]
        public void AddRemarkToExistingEmployee()
        {

            EmployeeService employeeService = new EmployeeService();
            employeeService.AddRemarksById(1, "bye");
            Assert.AreEqual(EmployeeService._EmployeeList[0].RemarkText, "bye");

        }
        [TestMethod]
        public void EmployeesHavingRemarkTest()
        {

            EmployeeService employeeService = new EmployeeService();
            List<Employee> listEmployee = employeeService.GetAllEmployeesHavingRemark("bye");
            Assert.AreEqual(EmployeeService._EmployeeList[0].RemarkText, "bye");

        }

    }
}
