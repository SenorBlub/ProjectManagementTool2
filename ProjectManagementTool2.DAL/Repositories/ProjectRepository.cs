using MySqlConnector;
using ProjectManagementTool2.Models;
using System;
using System.Collections.Generic;
using ProjectManagementTool2.Logic.Interfaces.IModels;
using ProjectManagementTool2.Logic.Interfaces.IRepositories;

namespace ProjectManagementTool2.DAL.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly MySqlConnection _connection;

        public ProjectRepository()
        {
            _connection = new MySqlConnection("Server=host.docker.internal;port=3312;Database=Project-Tool-Database;User=root;Password=password123;");
        }

        // GetProject to display project by ID
        public IProject GetProject(Guid projectId)
        {
            IProject project = new Project();
            _connection.Open();
            string query = "SELECT * FROM Project WHERE ID = @projectId";
            using MySqlCommand command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@projectId", projectId);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                project.Guid = (Guid)reader["guid"];
                project.Description = reader["description"].ToString();
                project.Title = reader["title"].ToString();
                project.IsNew = (bool)reader["isNew"];
            }

            _connection.Close();
            return project;
        }

        // GetProjects to display all projects
        public IEnumerable<IProject> GetProjects()
        {
            List<IProject> projects = new List<IProject>();
            _connection.Open();
            string query = "SELECT * FROM Project";
            using MySqlCommand command = new MySqlCommand(query, _connection);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                projects.Add(
                    new Project
                    {
                        Guid = (Guid)reader["guid"],
                        Description = reader["description"].ToString(),
                        Title = reader["title"].ToString(),
                        IsNew = (bool)reader["isNew"]
                    });
            }

            _connection.Close();
            return projects;
        }

        // GetEmployeeProjects to get all projects associated with a single employee
        public IEnumerable<IProject> GetEmployeeProjects(Guid projectId)
        {
            List<IProject> projects = new List<IProject>();
            _connection.Open();
            string query = "SELECT Project.* FROM EmployeeProject RIGHT JOIN Project ON EmployeeProject.projectGuid = Project.guid WHERE employeeGuid = @projectId";
            using MySqlCommand command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@projectId", projectId);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                projects.Add(
                    new Project
                    {
                        Guid = (Guid)reader["guid"],
                        Description = reader["description"].ToString(),
                        Title = reader["title"].ToString(),
                        IsNew = (bool)reader["isNew"]
                    });
            }

            _connection.Close();
            return projects;
        }

        // PostProject to create a project
        public void PostProject(IProject project)
        {
            _connection.Open();

            string query = "INSERT INTO Project (guid, title, description, isNew) VALUES (@projectId, @title, @description, @isNew)";
            MySqlCommand command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@projectId", project.Guid);
            command.Parameters.AddWithValue("@title", project.Title);
            command.Parameters.AddWithValue("@description", project.Description);
            command.Parameters.AddWithValue("@isNew", project.IsNew);

            command.ExecuteNonQuery();

            _connection.Close();
        }

        // UpdateProject to change any values associated with a project
        public void UpdateProject(IProject project, Guid projectId)
        {
            _connection.Open();

            string query = "UPDATE Project SET title = @title, description = @description, isNew = @isNew WHERE guid = @projectId";
            MySqlCommand command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@projectId", projectId);
            command.Parameters.AddWithValue("@title", project.Title);
            command.Parameters.AddWithValue("@description", project.Description);
            command.Parameters.AddWithValue("@isNew", project.IsNew);

            command.ExecuteNonQuery();

            _connection.Close();
        }

        // DeleteProject to remove a project by ID
        public void DeleteProject(Guid projectId)
        {
            _connection.Open();
            string query = "DELETE FROM Project WHERE guid = @projectId";
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@projectId", projectId);

            command.ExecuteNonQuery();

            _connection.Close();
        }
    }
}
