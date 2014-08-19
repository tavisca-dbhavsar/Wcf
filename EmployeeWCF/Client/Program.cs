using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ClientService;
using System.ServiceModel;


namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var employeeCreateObject = new AddandCreateClient("BasicHttpBinding_IAddandCreate");
            var employeeRetrieveObject = new RetrieveClient("WSHttpBinding_IRetrieve");
            int option=0;
            try
            {
                do
                {
                    Console.WriteLine("1.Create Employee");
                    Console.WriteLine("2.Retrive All Employees");
                    Console.WriteLine("3.Retrive Employees by Id");
                    Console.WriteLine("4.Retrive Employees by Name");
                    Console.WriteLine("5.Update Employee's Remark");
                    Console.WriteLine("6.Retrive Employees by Remark");

                    Console.WriteLine("Enter your option");
                    option = Convert.ToInt32(Console.ReadLine());


                    switch (option)
                    {
                        case 1:
                            string ans = "";
                            do
                            {
                                Console.WriteLine("Enter Employee Id ");
                                int id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Enter Employee Name ");
                                string name = Console.ReadLine();
                                Console.WriteLine("Enter Employee Remark Date");
                                DateTime date = Convert.ToDateTime(Console.ReadLine());
                                Console.WriteLine("Enter Employee Remark Text");
                                string text = Console.ReadLine();

                                var employee = new Employee();
                                employee.Id = id;
                                employee.Name = name;
                                employee.RemarkDate = date;
                                employee.RemarkText = text;
                                employeeCreateObject.CreateEmployee(employee);
                                Console.WriteLine("Want to continue(y/n)");
                                ans = Console.ReadLine();

                            } while (ans != "n");
                            break;

                        case 2: List<Employee> empList = new List<Employee>();
                            empList.AddRange(employeeRetrieveObject.GetAllEmployees());
                            foreach (Employee employee in empList)
                            {
                                Console.WriteLine("Employee Id : " + employee.Id);
                                Console.WriteLine("Employee Name : " + employee.Name);
                                Console.WriteLine("Employee Date : " + employee.RemarkDate);
                                Console.WriteLine("Employee Remark : " + employee.RemarkText);
                                Console.WriteLine("\n");
                            }
                            break;

                        case 3: Console.WriteLine("Enter the Search Id");
                            int searchId = Convert.ToInt32(Console.ReadLine());
                            var idDetails = employeeRetrieveObject.SearchById(searchId);
                            Console.WriteLine("Employee Id : " + idDetails.Id);
                            Console.WriteLine("Employee Name : " + idDetails.Name);
                            Console.WriteLine("Employee Date : " + idDetails.RemarkDate);
                            Console.WriteLine("Employee Remark : " + idDetails.RemarkText);
                            Console.WriteLine("\n");
                            break;

                        case 4: Console.WriteLine("Enter the Search Name");
                            string empName = Console.ReadLine();
                            List<Employee> empListByName = new List<Employee>();
                            empListByName.AddRange(employeeRetrieveObject.SearchByName(empName));
                            foreach (Employee employee in empListByName)
                            {
                                Console.WriteLine("Employee Id : " + employee.Id);
                                Console.WriteLine("Employee Name : " + employee.Name);
                                Console.WriteLine("Employee Date : " + employee.RemarkDate);
                                Console.WriteLine("Employee Remark : " + employee.RemarkText);
                                Console.WriteLine("\n");
                            }
                            break;

                        case 5: Console.WriteLine("Enter Id whose remark you want update");
                            int empId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Add New Remark");
                            string remark = Console.ReadLine();
                            employeeCreateObject.AddRemarksById(empId, remark);
                            break;

                        case 6: Console.WriteLine("Enter the Search Remark");
                            string searchRemark = Console.ReadLine();
                            List<Employee> empListByRemarkName = new List<Employee>();
                            empListByRemarkName.AddRange(employeeRetrieveObject.GetAllEmployeesHavingRemark(searchRemark));

                            foreach (Employee employee in empListByRemarkName)
                            {
                                Console.WriteLine("Employee Id : " + employee.Id);
                                Console.WriteLine("Employee Name : " + employee.Name);
                                Console.WriteLine("Employee Date : " + employee.RemarkDate);
                                Console.WriteLine("Employee Remark : " + employee.RemarkText);
                                Console.WriteLine("\n");
                            }
                            break;
                    }
                } while (option != 7);
            }
            catch (FaultException<FaultExceptionContract> ex)
            {
                string message = "StatusCode: " + ex.Detail.StatusCode;
                message += "\nMessage: " + ex.Detail.Message;
                Console.WriteLine(message);

            }
        }

    
    }
}
