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
            // Check if the employee ID is unique
            if (employees.Any(emp => emp.EmployeeId == employee.EmployeeId))
            {
                Console.WriteLine("Employee ID already exists. Please use a unique ID");
                return;
            }

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
                Console.WriteLine($"Employee ID: {employee.EmployeeId}, Name: {employee.FirstName} {employee.LastName}, Salary: {employee.Salary}, Department: {employee.Department}");
            }
        }

        // Calculate Total Salary function
        public void CalculateTotalSalary()
        {
            double totalSalary = employees.Sum(emp => emp.Salary);
            Console.WriteLine($"Total Salary of all employees: {totalSalary}");
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
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                Console.WriteLine("---------------------------\n");

                switch (choice)
                {
                    // Add Employee
                    case 1:
                        Console.Write("Enter Employee ID: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.Write("Enter First Name: ");
                        string firstName = Console.ReadLine();

                        Console.Write("Enter Last Name: ");
                        string lastName = Console.ReadLine();

                        Console.Write("Enter Salary: ");
                        double salary = double.Parse(Console.ReadLine());

                        // Display departments starting from 1
                        Console.WriteLine("Select Department:");
                        int departmentIndex = 1;
                        foreach (Department department in Enum.GetValues(typeof(Department)))
                        {
                            Console.WriteLine($"{departmentIndex}. {department}");
                            departmentIndex++;
                        }

                        // Input Department
                        Console.Write("Enter Department (by number): ");
                        int departmentNumber = int.Parse(Console.ReadLine());
                        Department departmentSelected = (Department)(departmentNumber - 1);

                        manager.AddEmployee(new Employee(id, firstName, lastName, salary, departmentSelected));
                        break;

                    // Remove Employee
                    case 2:
                        Console.Write("Enter Employee ID to remove: ");
                        int removeId = int.Parse(Console.ReadLine());
                        manager.RemoveEmployee(removeId);
                        break;

                    // View Employees
                    case 3:
                        manager.ViewEmployees();
                        break;

                    // Calculate Total Salary
                    case 4:
                        manager.CalculateTotalSalary();
                        break;

                    // Total Number of Employees
                    case 5:
                        Console.WriteLine($"Total number of employees: {EmployeeManager.TotalEmployees}");
                        break;

                    // Assign Employee to a Department
                    case 6:
                        Console.Write("Enter Employee ID to assign department: ");
                        int empId = int.Parse(Console.ReadLine());
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
                            Console.Write("Enter Department (by number): ");
                            int depIndex = int.Parse(Console.ReadLine());
                            Department depSelected = (Department)(depIndex - 1);
                            manager.AssignEmployeeToDepartment(employeeToAssign, depSelected);
                        }
                        else
                        {
                            Console.WriteLine($"Employee with ID {empId} is not found.");
                        }
                        break;

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