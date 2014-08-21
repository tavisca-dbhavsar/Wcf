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
        [DeploymentItem(@"EmployeeXmlData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                   @"EmployeeXmlData.xml","Employee", DataAccessMethod.Sequential)]

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

        [DeploymentItem(@"EmployeeXmlData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                   @"EmployeeXmlData.xml","Employee", DataAccessMethod.Sequential)]
        public void AddEmployeeAgain()
        {

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
            try
            {
                newEmployeeList.AddRange(employeeCreateObject.CreateEmployee(employee));
            }
            catch (FaultException<FaultExceptionContract>e)
            {
                Assert.AreEqual(e.Detail.Message, "Employee with Id " + employee.Id + " already exists");
            }

        }

        [TestMethod]
        [DeploymentItem(@"EmployeeXmlData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                   @"EmployeeXmlData.xml","AddRemarkToEmployee", DataAccessMethod.Sequential)]
        public void AddRemarkToExistingEmployee()
        {
            employee.Id = Int32.Parse(testContextInstance.DataRow["EmployeeId"].ToString());
            employee.Name = testContextInstance.DataRow["EmployeeName"].ToString();
            employee.RemarkText = testContextInstance.DataRow["EmployeeRemark"].ToString();
            employee.RemarkDate = Convert.ToDateTime(testContextInstance.DataRow["EmployeeRemarkDate"].ToString());
            employeeCreateObject.CreateEmployee(employee);
            Employee employeeDetail = employeeCreateObject.AddRemarksById(2, "Good");
            Assert.AreEqual(employeeDetail.RemarkText, "Good");
        }

        [TestMethod]
        [DeploymentItem(@"EmployeeXmlData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                   @"EmployeeXmlData.xml",
                   "AddRemarkToNonExistingId", DataAccessMethod.Sequential)]
        [ExpectedException(typeof(FaultException<FaultExceptionContract>))]
        public void AddRemarkToNonExistingId()
        {

            employee.Id = Int32.Parse(testContextInstance.DataRow["EmployeeId"].ToString());
            employee.Name = testContextInstance.DataRow["EmployeeName"].ToString();
            employee.RemarkText = testContextInstance.DataRow["EmployeeRemark"].ToString();
            employee.RemarkDate = Convert.ToDateTime(testContextInstance.DataRow["EmployeeRemarkDate"].ToString());
            employeeCreateObject.CreateEmployee(employee);
            Employee employeeDetail = employeeCreateObject.AddRemarksById(50, "bye");

        }

        [TestMethod]
        [DeploymentItem(@"EmployeeXmlData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                    @"EmployeeXmlData.xml",
                    "RetrieveEmployees", DataAccessMethod.Sequential)]
        public void RetrieveEmployees()
        {
            employee.Id = Int32.Parse(testContextInstance.DataRow["EmployeeId"].ToString());
            employee.Name = testContextInstance.DataRow["EmployeeName"].ToString();
            employee.RemarkText = testContextInstance.DataRow["EmployeeRemark"].ToString();
            employee.RemarkDate = Convert.ToDateTime(testContextInstance.DataRow["EmployeeRemarkDate"].ToString());
            employeeCreateObject.CreateEmployee(employee);
            List<Employee> employeeList = new List<Employee>();
            employeeList.AddRange(employeeRetrieveObject.GetAllEmployees());
        }

        [TestMethod]
        [DeploymentItem(@"EmployeeXmlData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                    @"EmployeeXmlData.xml",
                    "RetrieveEmployeeById", DataAccessMethod.Sequential)]
        public void RetrieveEmployeeById()
        {

            employee.Id = Int32.Parse(testContextInstance.DataRow["EmployeeId"].ToString());
            employee.Name = testContextInstance.DataRow["EmployeeName"].ToString();
            employee.RemarkText = testContextInstance.DataRow["EmployeeRemark"].ToString();
            employee.RemarkDate = Convert.ToDateTime(testContextInstance.DataRow["EmployeeRemarkDate"].ToString());
            employeeCreateObject.CreateEmployee(employee);

            Employee emp = employeeRetrieveObject.SearchById(5);
            Assert.AreEqual(5, emp.Id);
        }

        [TestMethod]
        [DeploymentItem(@"EmployeeXmlData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                    @"EmployeeXmlData.xml",
                    "RetrieveNonExistingEmployeeId", DataAccessMethod.Sequential)]
        [ExpectedException(typeof(FaultException<FaultExceptionContract>))]
        public void RetrieveNonExistingEmployeeId()
        {

            employee.Id = Int32.Parse(testContextInstance.DataRow["EmployeeId"].ToString());
            employee.Name = testContextInstance.DataRow["EmployeeName"].ToString();
            employee.RemarkText = testContextInstance.DataRow["EmployeeRemark"].ToString();
            employee.RemarkDate = Convert.ToDateTime(testContextInstance.DataRow["EmployeeRemarkDate"].ToString());
            employeeCreateObject.CreateEmployee(employee);

            Employee emp = employeeRetrieveObject.SearchById(50);
            Assert.AreEqual(50, emp.Id);
        }


        [TestMethod]
        [DeploymentItem(@"EmployeeXmlData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                    @"EmployeeXmlData.xml",
                    "RetrieveEmployeeByName", DataAccessMethod.Sequential)]
        public void RetrieveEmployeeByName()
        {

            employee.Id = Int32.Parse(testContextInstance.DataRow["EmployeeId"].ToString());
            employee.Name = testContextInstance.DataRow["EmployeeName"].ToString();
            employee.RemarkText = testContextInstance.DataRow["EmployeeRemark"].ToString();
            employee.RemarkDate = Convert.ToDateTime(testContextInstance.DataRow["EmployeeRemarkDate"].ToString());
            employeeCreateObject.CreateEmployee(employee);

            List<Employee> employeeList = new List<Employee>();
            employeeList.AddRange(employeeRetrieveObject.SearchByName("Shruti"));
            Assert.AreEqual("Shruti", employeeList[0].Name);

        }

        [TestMethod]
        [DeploymentItem(@"EmployeeXmlData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                    @"EmployeeXmlData.xml",
                    "RetrieveNonExistingEmployeeByName", DataAccessMethod.Sequential)]
        [ExpectedException(typeof(FaultException<FaultExceptionContract>))]
        public void RetrieveNonExistingEmployeeByName()
        {

            employee.Id = Int32.Parse(testContextInstance.DataRow["EmployeeId"].ToString());
            employee.Name = testContextInstance.DataRow["EmployeeName"].ToString();
            employee.RemarkText = testContextInstance.DataRow["EmployeeRemark"].ToString();
            employee.RemarkDate = Convert.ToDateTime(testContextInstance.DataRow["EmployeeRemarkDate"].ToString());
            employeeCreateObject.CreateEmployee(employee);

            List<Employee> employeeList = new List<Employee>();
            employeeList.AddRange(employeeRetrieveObject.SearchByName("shri"));
            Assert.AreEqual("abc", employeeList[0].Name);
        }

        [TestMethod]
        [DeploymentItem(@"EmployeeXmlData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                    @"EmployeeXmlData.xml",
                    "EmployeesHavingRemark", DataAccessMethod.Sequential)]
        public void EmployeesHavingRemark()
        {
            employee.Id = Int32.Parse(testContextInstance.DataRow["EmployeeId"].ToString());
            employee.Name = testContextInstance.DataRow["EmployeeName"].ToString();
            employee.RemarkText = testContextInstance.DataRow["EmployeeRemark"].ToString();
            employee.RemarkDate = Convert.ToDateTime(testContextInstance.DataRow["EmployeeRemarkDate"].ToString());
            employeeCreateObject.CreateEmployee(employee);

            List<Employee> employeeList = new List<Employee>();
            employeeList.AddRange(employeeRetrieveObject.GetAllEmployeesHavingRemark("Good"));
            Assert.AreEqual(employeeList[0].RemarkText, "Good");
        }

       /* [TestMethod]
     
        public void RetrieveEmployeesByNameHavingNoRemarkTest()
        {

            employee.Id = 13;
            employee.Name = "krutika";
            employee.RemarkText = "";
            employee.RemarkDate = Convert.ToDateTime("2 / 2 / 14");
            employeeCreateObject.CreateEmployee(employee);

            List<Employee> employeeList = new List<Employee>();
            employeeList.AddRange(employeeRetrieveObject.GetAllEmployeesHavingRemark(""));
            Assert.AreEqual(employeeList[0].RemarkText, "");
        }*/

        [TestMethod]
        [DeploymentItem(@"EmployeeXmlData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                    @"EmployeeXmlData.xml",
                    "ValidateInvalidIdInAddRemarksById", DataAccessMethod.Sequential)]

        [ExpectedException(typeof(FaultException))]
        public void ValidateInvalidIdInAddRemarksById()
        {
            employee.Id = Int32.Parse(testContextInstance.DataRow["EmployeeId"].ToString());
            employee.Name = testContextInstance.DataRow["EmployeeName"].ToString();
            employee.RemarkText = testContextInstance.DataRow["EmployeeRemark"].ToString();
            employee.RemarkDate = Convert.ToDateTime(testContextInstance.DataRow["EmployeeRemarkDate"].ToString());
            employeeCreateObject.CreateEmployee(employee);
            Employee employeeDetail = employeeCreateObject.AddRemarksById(-1, "Good");
            Assert.AreEqual(employeeDetail.Id, -1);
        }

        [TestMethod]
        [DeploymentItem(@"EmployeeXmlData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                    @"EmployeeXmlData.xml",
                    "ValidateInvalidRemarkNameInAddRemarksById", DataAccessMethod.Sequential)]

        [ExpectedException(typeof(FaultException))]
        public void ValidateInvalidRemarkNameInAddRemarksById()
        {
            employee.Id = Int32.Parse(testContextInstance.DataRow["EmployeeId"].ToString());
            employee.Name = testContextInstance.DataRow["EmployeeName"].ToString();
            employee.RemarkText = testContextInstance.DataRow["EmployeeRemark"].ToString();
            employee.RemarkDate = Convert.ToDateTime(testContextInstance.DataRow["EmployeeRemarkDate"].ToString());
            employeeCreateObject.CreateEmployee(employee);
            Employee employeeDetail = employeeCreateObject.AddRemarksById(9, "Bad@&#");
            Assert.AreEqual(employeeDetail.RemarkText, "Bad");
        }

        [TestMethod]
        [DeploymentItem(@"EmployeeXmlData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                    @"EmployeeXmlData.xml",
                    "ValidateRetrieveNonExistingEmployeeName", DataAccessMethod.Sequential)]

        [ExpectedException(typeof(FaultException))]
        public void ValidateRetrieveNonExistingEmployeeName()
        {

            employee.Id = Int32.Parse(testContextInstance.DataRow["EmployeeId"].ToString());
            employee.Name = testContextInstance.DataRow["EmployeeName"].ToString();
            employee.RemarkText = testContextInstance.DataRow["EmployeeRemark"].ToString();
            employee.RemarkDate = Convert.ToDateTime(testContextInstance.DataRow["EmployeeRemarkDate"].ToString());
            employeeCreateObject.CreateEmployee(employee);
            List<Employee> empList = new List<Employee>();
            empList.AddRange(employeeRetrieveObject.SearchByName("Anita@#$"));

            Assert.AreEqual(empList[0].Name, "Anita");
        }

        [TestMethod]
        [DeploymentItem(@"EmployeeXmlData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                    @"EmployeeXmlData.xml",
                    "ValidateEmployeesHavingRemark", DataAccessMethod.Sequential)]


        [ExpectedException(typeof(FaultException))]
        public void ValidateEmployeesHavingRemark()
        {

            employee.Id = Int32.Parse(testContextInstance.DataRow["EmployeeId"].ToString());
            employee.Name = testContextInstance.DataRow["EmployeeName"].ToString();
            employee.RemarkText = testContextInstance.DataRow["EmployeeRemark"].ToString();
            employee.RemarkDate = Convert.ToDateTime(testContextInstance.DataRow["EmployeeRemarkDate"].ToString());
            employeeCreateObject.CreateEmployee(employee);

            List<Employee> employeeList = new List<Employee>();
            employeeList.AddRange(employeeRetrieveObject.GetAllEmployeesHavingRemark("Bad@#$%"));
            Assert.AreEqual(employeeList[0].RemarkText, "Bad");
        }
    }
}
