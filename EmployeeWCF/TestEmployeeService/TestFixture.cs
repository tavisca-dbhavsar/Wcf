using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestEmployeeService.EmpService;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;



namespace TestEmployeeService
{
    [TestClass]
    public class TestFixture
    {

       private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        Employee employee = new Employee();
            
        AddandCreateClient employeeCreateObject = new AddandCreateClient("BasicHttpBinding_IAddandCreate");
        RetrieveClient employeeRetrieveObject = new RetrieveClient("WSHttpBinding_IRetrieve");

        [TestMethod]
        [DeploymentItem(@"D:\WCF\EmployeeManagement\Wcf\EmployeeWCF\TestEmployeeService\EmployeeXmlData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                   @"D:\WCF\EmployeeManagement\Wcf\EmployeeWCF\TestEmployeeService\EmployeeXmlData.xml",
                   "Employee",
                    DataAccessMethod.Sequential)]

        public void AddEmployee()
        {
            employee.Id = Int32.Parse(testContextInstance.DataRow["EmployeeId"].ToString());
            employee.Name = testContextInstance.DataRow["EmployeeName"].ToString();
            employee.RemarkText = testContextInstance.DataRow["EmployeeRemark"].ToString();
            employee.RemarkDate = Convert.ToDateTime(testContextInstance.DataRow["EmployeeRemarkDate"].ToString());
            List<Employee> employeeList = new List<Employee>();
            employeeList.AddRange(employeeCreateObject.CreateEmployee(employee));
            Assert.AreEqual(employeeList[0].Id, 1);
        }

        [TestMethod]

        [ExpectedException(typeof(FaultException<FaultExceptionContract>))]

        [DeploymentItem(@"D:\WCF\EmployeeManagement\Wcf\EmployeeWCF\TestEmployeeService\EmployeeXmlData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                   @"D:\WCF\EmployeeManagement\Wcf\EmployeeWCF\TestEmployeeService\EmployeeXmlData.xml",
                   "Employee",
                    DataAccessMethod.Sequential)]
        public void AddEmployeeAgain()
        {
            
            //employee.Id = 1;
            //employee.Name = "xyz";
            //employee.RemarkText = "Hello";
            //employee.RemarkDate = Convert.ToDateTime("2 / 2 / 14");
            //employeeCreateObject.CreateEmployee(employee);
           
            //Employee newEmployee = new Employee();
            //newEmployee.Id = 1;
            //newEmployee.Name = "xyz";
            //newEmployee.RemarkText = "Hello";
            //newEmployee.RemarkDate = Convert.ToDateTime("2 / 2 / 14");
            //employeeCreateObject.CreateEmployee(newEmployee);

            employee.Id = Int32.Parse(testContextInstance.DataRow["EmployeeId"].ToString());
            employee.Name = testContextInstance.DataRow["EmployeeName"].ToString();
            employee.RemarkText = testContextInstance.DataRow["EmployeeRemark"].ToString();
            employee.RemarkDate = Convert.ToDateTime(testContextInstance.DataRow["EmployeeRemarkDate"].ToString());
            List<Employee> employeeList = new List<Employee>();
            employeeList.AddRange(employeeCreateObject.CreateEmployee(employee));

            employee.Id = Int32.Parse(testContextInstance.DataRow["EmployeeId"].ToString());
            employee.Name = testContextInstance.DataRow["EmployeeName"].ToString();
            employee.RemarkText = testContextInstance.DataRow["EmployeeRemark"].ToString();
            employee.RemarkDate = Convert.ToDateTime(testContextInstance.DataRow["EmployeeRemarkDate"].ToString());
            List<Employee> newEmployeeList = new List<Employee>();
            newEmployeeList.AddRange(employeeCreateObject.CreateEmployee(employee));
           
        }

        [TestMethod]
        public void AddRemarkToExistingEmployee()
        {
            employee.Id = 2;
            employee.Name = "abc";
            employee.RemarkText = "Hello";
            employee.RemarkDate = Convert.ToDateTime("2 / 2 / 14");
            employeeCreateObject.CreateEmployee(employee);
            Employee employeeDetail = employeeCreateObject.AddRemarksById(2, "bye");
            Assert.AreEqual(employeeDetail.RemarkText, "bye");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<FaultExceptionContract>))]
        public void AddRemarkToNonExistingId()
        {
          
            employee.Id = 2;
            employee.Name = "abc";
            employee.RemarkText = "Hi";
            employee.RemarkDate = Convert.ToDateTime("5/ 5 / 14");
            employeeCreateObject.CreateEmployee(employee);
            Employee employeeDetail = employeeCreateObject.AddRemarksById(12, "bye");
           
        }

       [TestMethod]
        public void RetrieveEmployees()
        {
           
            employee.Id = 3;
            employee.Name = "efg";
            employee.RemarkText = "Hifi";
            employee.RemarkDate = Convert.ToDateTime("5/ 5 / 14");
            employeeCreateObject.CreateEmployee(employee);
            List<Employee> employeeList = new List<Employee>();
            employeeList.AddRange(employeeRetrieveObject.GetAllEmployees());
          
        }

        [TestMethod]
         public void RetrieveEmployeeById()
         {
            
             employee.Id = 4;
             employee.Name = "gauri";
             employee.RemarkText = "good";
             employee.RemarkDate = Convert.ToDateTime("2 / 2 / 14");
             employeeCreateObject.CreateEmployee(employee);

             Employee emp = employeeRetrieveObject.SearchById(4);
             Assert.AreEqual(4, emp.Id);
         }

         [TestMethod]
         [ExpectedException(typeof(FaultException<FaultExceptionContract>))]
         public void RetrieveNonExistingEmployeeId()
         {

             employee.Id = 5;
             employee.Name = "xyz";
             employee.RemarkText = "Hello";
             employee.RemarkDate = Convert.ToDateTime("2 / 2 / 14");
             employeeCreateObject.CreateEmployee(employee);

             Employee emp = employeeRetrieveObject.SearchById(12);
             Assert.AreEqual(12, emp.Id);
         }
        

        [TestMethod]
        public void RetrieveEmployeeByName()
        {
            
            employee.Id = 6;
            employee.Name = "shri";
            employee.RemarkText = "Helloji";
            employee.RemarkDate = Convert.ToDateTime("2 / 2 / 14");
            employeeCreateObject.CreateEmployee(employee);

            List<Employee> employeeList = new List<Employee>();
            employeeList.AddRange(employeeRetrieveObject.SearchByName("shri"));
            Assert.AreEqual("shri", employeeList[0].Name);

        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<FaultExceptionContract>))]
        public void RetrieveNonExistingEmployeeByName()
        {
           
            employee.Id = 6;
            employee.Name = "shri";
            employee.RemarkText = "Helloji";
            employee.RemarkDate = Convert.ToDateTime("2 / 2 / 14");
            employeeCreateObject.CreateEmployee(employee);

            List<Employee> employeeList = new List<Employee>();
            employeeList.AddRange(employeeRetrieveObject.SearchByName("shri"));
            Assert.AreEqual("abc", employeeList[0].Name);
        }

       [TestMethod]
       public void EmployeesHavingRemark()
       {
           
           employee.Id = 7;
           employee.Name = "om";
           employee.RemarkText = "goodmorning";
           employee.RemarkDate = Convert.ToDateTime("2 / 2 / 14");
           employeeCreateObject.CreateEmployee(employee);

            List<Employee> employeeList = new List<Employee>();
            employeeList.AddRange(employeeRetrieveObject.GetAllEmployeesHavingRemark("goodmorning"));
            Assert.AreEqual(employeeList[0].RemarkText, "goodmorning");
        }

       [TestMethod]
       public void RetrieveEmployeesByNameHavingNoRemarkTest()
       {

           employee.Id = 8;
           employee.Name = "shivali";
           employee.RemarkText = "";
           employee.RemarkDate = Convert.ToDateTime("2 / 2 / 14");
           employeeCreateObject.CreateEmployee(employee);

           List<Employee> employeeList = new List<Employee>();
           employeeList.AddRange(employeeRetrieveObject.GetAllEmployeesHavingRemark(""));
           Assert.AreEqual(employeeList[0].RemarkText, "");
       }

 
    }
}
