using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
                Console.WriteLine($"Employee ID# {employeeId} removed successfully.");
                totalEmployees--;
            }
            else
            {
                Console.WriteLine($"Employee ID# {employeeId} is not found.");
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
                DisplayMenu();

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Line();
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    // Case 1
                    // Add Employee
                    case 1:
                        Line();
                        AddEmployee(manager);
                        break;

                    // Case 2
                    // Remove Employee
                    case 2:
                        Line();
                        RemoveEmployee(manager);                        
                        break;

                    // Case 3
                    // View Employees
                    case 3:
                        Line();
                        manager.ViewEmployees();
                        break;

                    // Case 4
                    // Calculate Total Salary
                    case 4:
                        Line();
                        manager.CalculateTotalSalary();
                        break;

                    // Case 5
                    // Total Number of Employees
                    case 5:
                        Line();
                        TotalEmployees(manager);
                        break;

                    // Case 6
                    // Assign Employee to a Department
                    case 6:
                        Line();
                        AssignEmployeeToDepartment(manager);              
                        break;

                    // Case 7
                    // Exit Program
                    case 7:
                        exit = true;
                        break;

                    default:
                        Line();
                        Console.WriteLine("Invalid choice. Please enter a number (1-7).");
                        break;
                }
            }
        }

        // Display the Menu
        static void DisplayMenu()
        {
            Line();
            Console.WriteLine("Employee Management System\n");
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. Remove Employee");
            Console.WriteLine("3. View All Employees");
            Console.WriteLine("4. Calculate Total Salary");
            Console.WriteLine("5. Total Number of Employees");
            Console.WriteLine("6. Assign Employee to Department");
            Console.WriteLine("7. Exit");
            Console.Write("\nEnter your choice: ");
        }

        // Add Employee
        static void AddEmployee(EmployeeManager manager)
        {
            // Employee Variables
            int id = 0;
            int departmentNumber = 0;
            double salary = 0;
            string firstName = "", lastName = "";

            // Employee Validation
            bool isValidIdInput = false;
            bool isValidFirstNameInput = false;
            bool isValidLastNameInput = false;
            bool isValidSalaryInput = false;
            bool isValidDepInput = false;

            // Employee ID                        
            while (!isValidIdInput)
            {
                Console.Write("Enter Employee ID: ");
                if (int.TryParse(Console.ReadLine(), out id))
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

                // Trim to remove spaces
                firstName = firstName.Trim();

                if (!string.IsNullOrEmpty(firstName) && Regex.IsMatch(firstName, @"^[a-zA-Z]+$"))
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

                // Trim to remove spaces
                lastName = lastName.Trim();

                if (!string.IsNullOrEmpty(lastName) && Regex.IsMatch(lastName, @"^[a-zA-Z]+$"))
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
                if (double.TryParse(Console.ReadLine(), out salary))
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
                if (int.TryParse(Console.ReadLine(), out departmentNumber))
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
        }

        // Remove Employee
        static void RemoveEmployee(EmployeeManager manager)
        {
            // Check if there are no employees to remove
            if (manager.Employees.Count < 1)
            {
                Console.WriteLine("There are no employees to remove.");
            }
            else
            {
                int removeId = 0;
                bool isValid = false;

                while (!isValid)
                {
                    Console.WriteLine("Enter Employee ID to remove");
                    Console.Write("ID# ");
                    //int removeId = int.Parse(Console.ReadLine());
                    if (int.TryParse(Console.ReadLine(), out removeId))
                    {
                        isValid = true;
                        manager.RemoveEmployee(removeId);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid ID.");
                    }
                }
            }
        }

        // Total Number of Employees
        static void TotalEmployees(EmployeeManager manager)
        {
            Console.WriteLine($"Total number of employees: {EmployeeManager.TotalEmployees}");
        }

        // Assign Employee to Department
        static void AssignEmployeeToDepartment(EmployeeManager manager)
        {
            // Department Variables
            int empId = 0;
            int depIndex = 0;

            // Department Validation
            bool isValidInput = false;
            bool isValidNumberInput = false;

            // Check if there are no employees to assign/change department
            if (manager.Employees.Count < 1)
            {
                Console.WriteLine("There are no employees to assign department.");
                return;
            }

            // Assigning department to employee
            while (!isValidInput)
            {
                Console.WriteLine("Enter Employee ID to assign department");
                Console.Write("ID# ");
                if (int.TryParse(Console.ReadLine(), out empId))
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
                            if (int.TryParse(Console.ReadLine(), out depIndex))
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
                        Console.WriteLine($"\nEmployee ID# {empId} is not found.\n");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please enter a valid ID.\n");
                    continue;
                }
            }
        }
        static void Line()
        {
            Console.WriteLine("\n---------------------------\n");
        }
    }
}