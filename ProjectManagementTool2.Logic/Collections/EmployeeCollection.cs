using System;
using System.Collections.Generic;
using ProjectManagementTool2.Logic.Interfaces.IModels;
using ProjectManagementTool2.Logic.Interfaces.IRepositories;
using ProjectManagementTool2.Logic.Interfaces.IRepositories.ILinkRepositories;

namespace ProjectManagementTool2.Logic.Collections
{
    public class EmployeeCollection
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeProjectRepository _employeeProjectRepository;

        public EmployeeCollection(IEmployeeRepository employeeRepository, IEmployeeProjectRepository employeeProjectRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeProjectRepository = employeeProjectRepository;
        }

        public void AddEmployee(IEmployee employee)
        {
            _employeeRepository.PostEmployee(employee);
        }

        public void RemoveEmployee(Guid employeeId)
        {
            _employeeRepository.DeleteEmployee(employeeId);
        }

        public void DisplayAllEmployees()
        {
            var employees = _employeeRepository.GetEmployees();
            foreach (var employee in employees)
            {
                Console.WriteLine($"Employee ID: {employee.Guid}, Name: {employee.Name}, Email: {employee.Email}");
            }
        }

        public void DisplayEmployeeById(Guid employeeId)
        {
            var employee = _employeeRepository.GetEmployee(employeeId);
            if (employee != null)
            {
                Console.WriteLine($"Employee ID: {employee.Guid}, Name: {employee.Name}, Email: {employee.Email}");
            }
            else
            {
                Console.WriteLine($"Employee with ID {employeeId} not found.");
            }
        }

        public void UpdateEmployee(Guid employeeId, string newName, string newEmail)
        {
            var existingEmployee = _employeeRepository.GetEmployee(employeeId);
            if (existingEmployee != null)
            {
                existingEmployee.Name = newName;
                existingEmployee.Email = newEmail;
                _employeeRepository.UpdateEmployee(existingEmployee, employeeId);
                Console.WriteLine($"Employee with ID {employeeId} updated successfully.");
            }
            else
            {
                Console.WriteLine($"Employee with ID {employeeId} not found.");
            }
        }

        public void AddEmployeeToProject(Guid employeeId, Guid projectId)
        {
            _employeeProjectRepository.PostEmployeeProject(employeeId, projectId);
            Console.WriteLine($"Employee with ID {employeeId} added to the project with ID {projectId}.");
        }

        public void RemoveEmployeeFromProject(Guid employeeId, Guid projectId)
        {
            _employeeProjectRepository.DeleteEmployeeProject(employeeId, projectId);
            Console.WriteLine($"Employee with ID {employeeId} removed from the project with ID {projectId}.");
        }
    }
}
