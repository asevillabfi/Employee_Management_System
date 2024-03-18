using System;
using System.Collections.Generic;
using System.Linq;

namespace EMSApplication
{
    // Enum representing departments
    public enum Department
    {
        IT,
        HR,
        Finance,
        Marketing,
        Operations,
        Sales
    };

    // Base class Person
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    // Employee class derived from Person
    public class Employee : Person
    {
        // private static int totalEmployees = 0;

        public int EmployeeId { get; private set; }
        public double Salary { get; set; }
        public Department Department { get; set; }

        // Constructor
        public Employee(int employeeId, string firstName, string lastName, double salary, Department department)
        {
            EmployeeId = employeeId;
            FirstName = firstName;
            LastName = lastName;
            Salary = salary;
            Department = department;
        }

        // Destructor
        ~Employee()
        {
            Console.WriteLine($"Employee {FirstName} {LastName} is destroyed.");
        }
    }

    // Interface for managing employees
    interface IManager
    {
        void AssignEmployeeToDepartment(Employee employee, Department department);
    }

    // EmployeeManager class managing employees
    public class EmployeeManager : IManager
    {
        private List<Employee> employees = new List<Employee>();
        public List<Employee> Employees
        {
            get { return employees; }
        }
        private static int totalEmployees = 0;

        // Add Employee function
        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
            totalEmployees++;
        }

        // Remove Employee function
        public void RemoveEmployee(int employeeId)
        {
            var employeeToRemove = employees.FirstOrDefault(emp => emp.EmployeeId == employeeId);
            if (employeeToRemove != null)
            {
                employees.Remove(employeeToRemove);
                Console.WriteLine($"Employee with ID {employeeId} removed successfully.");
                totalEmployees--;
            }
            else
            {
                Console.WriteLine($"Employee with ID {employeeId} is not found.");
            }
        }

        // View Employees function
        public void ViewEmployees()
        {
            // Check if there's no employees to view
            if (employees.Count < 1)
            {
                Console.WriteLine("There are no employees inside.");
                return;
            }

            foreach (var employee in employees)
            {
                Console.WriteLine($"Employee ID# {employee.EmployeeId}, Name: {employee.FirstName} {employee.LastName}, Salary: Php {employee.Salary}, Department: {employee.Department}");
            }
        }

        // Calculate Total Salary function
        public void CalculateTotalSalary()
        {
            double totalSalary = employees.Sum(emp => emp.Salary);
            Console.WriteLine($"Total Salary of all employees: Php {totalSalary}");
        }

        // Total number of Employees
        public static int TotalEmployees
        {
            get { return totalEmployees; }
        }

        // Interface implementation
        public void AssignEmployeeToDepartment(Employee employee, Department department)
        {
            employee.Department = department;
            Console.WriteLine($"Employee {employee.FirstName} {employee.LastName} is assigned to {department} department.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            EmployeeManager manager = new EmployeeManager();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n---------------------------");
                Console.WriteLine("Employee Management System\n");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Remove Employee");
                Console.WriteLine("3. View All Employees");
                Console.WriteLine("4. Calculate Total Salary");
                Console.WriteLine("5. Total Number of Employees");
                Console.WriteLine("6. Assign Employee to Department");
                Console.WriteLine("7. Exit");
                Console.Write("\nEnter your choice: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("---------------------------\n");
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                Console.WriteLine("---------------------------\n");

                switch (choice)
                {
                    // Case 1
                    // Add Employee
                    case 1:
                        // employee variables
                        int id = 0;
                        int departmentNumber = 0;
                        double salary = 0;
                        string firstName = "", lastName = "";
                        string input;

                        // employee validation
                        bool isValidIdInput = false;
                        bool isValidFirstNameInput = false;
                        bool isValidLastNameInput = false;
                        bool isValidSalaryInput = false;
                        bool isValidDepInput = false;

                        // Employee ID                        
                        while (!isValidIdInput)
                        {
                            Console.Write("Enter Employee ID: ");
                            input = Console.ReadLine();

                            if (int.TryParse(input, out id))
                            {
                                // Check if the employee ID is unique
                                if (manager.Employees.Any(emp => emp.EmployeeId == id))
                                {
                                    Console.WriteLine("Employee ID already exists. Please use a unique ID");
                                }
                                else
                                {
                                    isValidIdInput = true;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a valid ID.");
                            }
                        }

                        // First Name
                        while (!isValidFirstNameInput)
                        {
                            Console.Write("Enter First Name: ");
                            firstName = Console.ReadLine();

                            if (!string.IsNullOrEmpty(firstName))
                            {
                                isValidFirstNameInput = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a valid First name.");
                            }
                        }

                        // Last Name
                        while (!isValidLastNameInput)
                        {
                            Console.Write("Enter Last Name: ");
                            lastName = Console.ReadLine();

                            if (!string.IsNullOrEmpty(lastName))
                            {
                                isValidLastNameInput = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a valid Last name.");
                            }
                        }

                        // Salary
                        while (!isValidSalaryInput)
                        {
                            Console.Write("Enter Salary: ");
                            input = Console.ReadLine();

                            if (double.TryParse(input, out salary))
                            {
                                if (salary <= 0)
                                {
                                    Console.WriteLine("Invalid amount. Please enter a valid Salary.");
                                }
                                else
                                {
                                    isValidSalaryInput = true;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a valid Salary.");
                            }
                        }

                        // Display departments starting from 1
                        Console.WriteLine("Select Department:");
                        int departmentIndex = 1;
                        foreach (Department department in Enum.GetValues(typeof(Department)))
                        {
                            Console.WriteLine($"{departmentIndex}. {department}");
                            departmentIndex++;
                        }

                        // Input Department
                        while (!isValidDepInput)
                        {
                            Console.Write("Enter Department (by number): ");
                            input = Console.ReadLine();

                            if (int.TryParse(input, out departmentNumber))
                            {
                                if (Enum.IsDefined(typeof(Department), departmentNumber - 1))
                                {
                                    isValidDepInput = true;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid number. Please enter a number from above.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a valid Department number.");
                            }
                        }

                        Department departmentSelected = (Department)(departmentNumber - 1);

                        Employee newEmployee = new Employee(id, firstName, lastName, salary, departmentSelected);
                        manager.AddEmployee(newEmployee);
                        break;

                    // Case 2
                    // Remove Employee
                    case 2:
                        // Check if there are no employees to remove
                        if (manager.Employees.Count < 1)
                        {
                            Console.WriteLine("There are no employees to remove.");
                            break;
                        }
                        Console.Write("Enter Employee ID to remove: ");
                        int removeId = int.Parse(Console.ReadLine());
                        manager.RemoveEmployee(removeId);
                        break;

                    // Case 3
                    // View Employees
                    case 3:
                        manager.ViewEmployees();
                        break;

                    // Case 4
                    // Calculate Total Salary
                    case 4:
                        manager.CalculateTotalSalary();
                        break;

                    // Case 5
                    // Total Number of Employees
                    case 5:
                        Console.WriteLine($"Total number of employees: {EmployeeManager.TotalEmployees}");
                        break;

                    // Case 6
                    // Assign Employee to a Department
                    case 6:
                        int empId = 0;
                        int depIndex = 0;
                        bool isValidInput = false;
                        bool isValidNumberInput = false;

                        // Check if there are no employees to assign/change department
                        if (manager.Employees.Count < 1)
                        {
                            Console.WriteLine("There are no employees to assign department.");
                            break;
                        }

                        // Assigning department to employee
                        while (!isValidInput)
                        {
                            Console.WriteLine("Enter Employee ID to assign department");
                            Console.Write("ID# ");
                            input = Console.ReadLine();

                            if (int.TryParse(input, out empId))
                            {
                                Employee employeeToAssign = manager.Employees.FirstOrDefault(emp => emp.EmployeeId == empId);

                                if (employeeToAssign != null)
                                {
                                    Console.WriteLine("Select Department:");
                                    int departIndex = 1;
                                    foreach (Department department in Enum.GetValues(typeof(Department)))
                                    {
                                        Console.WriteLine($"{departIndex}. {department}");
                                        departIndex++;
                                    }

                                    while (!isValidNumberInput)
                                    {
                                        Console.Write("Enter Department (by number): ");
                                        input = Console.ReadLine();
                                        if (int.TryParse(input, out depIndex))
                                        {
                                            if (Enum.IsDefined(typeof(Department), depIndex - 1))
                                            {
                                                Department depSelected = (Department)(depIndex - 1);
                                                manager.AssignEmployeeToDepartment(employeeToAssign, depSelected);

                                                isValidInput = true;
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid number. Please enter a number from above.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid input. Please enter a valid number.");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"\nEmployee ID#{empId} is not found.\n");
                                    continue;
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nInvalid input. Please enter a valid ID.\n");
                                continue;
                            }
                        }                      
                        break;

                    // Case 7
                    // Exit Program
                    case 7:
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}