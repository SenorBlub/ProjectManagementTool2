using System;
using System.Collections.Generic;
using MySqlConnector;
using ProjectManagementTool2.Logic.Interfaces.IModels;
using ProjectManagementTool2.Logic.Interfaces.IRepositories;
using ProjectManagementTool2.Models;

namespace ProjectManagementTool2.DAL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        readonly MySqlConnection _connection;

        public EmployeeRepository()
        {
            _connection = new MySqlConnection("Server=host.docker.internal;port=3312;Database=Project-Tool-Database;User=root;Password=password123;");
        }

        // GetEmployee to get a single employee by ID
        public IEmployee GetEmployee(Guid employeeId)
        {
            IEmployee employee = new Employee();
            _connection.Open();
            string query = "SELECT * FROM Employee WHERE guid = @employeeId";
            using MySqlCommand command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@employeeId", employeeId);

            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                employee.Email = reader["email"].ToString();
                employee.Name = reader["name"].ToString();
                employee.Guid = (Guid)reader["guid"];
            }
            _connection.Close();
            return employee;
        }

        // GetEmployees to get all employees
        public List<IEmployee> GetEmployees()
        {
            List<IEmployee> employees = new List<IEmployee>();
            _connection.Open();
            string query = "SELECT * FROM Employee";
            using MySqlCommand command = new MySqlCommand(query, _connection);

            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                employees.Add(new Employee
                {
                    Email = reader["email"].ToString(),
                    Name = reader["name"].ToString(),
                    Guid = (Guid)reader["guid"]
                });
            }
            _connection.Close();
            return employees;
        }

        // GetEmployees to get all employees
        public List<IEmployee> GetProjectEmployees(Guid projectId)
        {
            List<IEmployee> employees = new List<IEmployee>();
            _connection.Open();
            string query = "SELECT Employee.* FROM EmployeeProject RIGHT JOIN Employee ON EmployeeProject.employeeGuid = Employee.guid WHERE EmployeeProject.projectGuid = @projectId"; // !TODO Clarity of definition
            using MySqlCommand command = new MySqlCommand(query, _connection); // !TODO only fetch relevant data (performance)
            command.Parameters.AddWithValue("@projectId", projectId);

            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                employees.Add(new Employee
                {
                    Email = reader["email"].ToString(),
                    Name = reader["name"].ToString(),
                    Guid = (Guid)reader["employeeGuid"]
                });
            }
            _connection.Close();
            return employees;
        }

        // PostEmployee to add an employee
        public void PostEmployee(IEmployee employee)
        {
            _connection.Open();
            string query = "INSERT INTO Employee (guid, name, email) VALUES (@employeeId, @name, @email)";
            using MySqlCommand command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@employeeId", employee.Guid);
            command.Parameters.AddWithValue("@name", employee.Name);
            command.Parameters.AddWithValue("@email", employee.Email);

            command.ExecuteNonQuery();
            _connection.Close();
        }

        // UpdateEmployee to edit a single employee by ID
        public void UpdateEmployee(IEmployee employee, Guid employeeId)
        {
            _connection.Open();
            string query = "UPDATE Employee SET name = @name, email = @email WHERE guid = @employeeId";
            using MySqlCommand command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@employeeId", employeeId);
            command.Parameters.AddWithValue("@name", employee.Name);
            command.Parameters.AddWithValue("@email", employee.Email);

            command.ExecuteNonQuery();
            _connection.Close();
        }

        // DeleteEmployee to delete a single employee by ID
        public void DeleteEmployee(Guid employeeId)
        {
            _connection.Open();
            string query = "DELETE FROM Employee WHERE guid = @employeeId";
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@employeeId", employeeId);

            command.ExecuteNonQuery();
            _connection.Close();
        }
    }
}
