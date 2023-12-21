﻿using System;
using System.Collections.Generic;
using MySqlConnector;
using ProjectManagementTool2.Logic.Interfaces.IRepositories;
using ProjectManagementTool2.Logic.Interfaces.IModels;
using Task = ProjectManagementTool2.Models.Task;

namespace ProjectManagementTool2.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        readonly MySqlConnection _connection;

        public TaskRepository()
        {
            _connection = new MySqlConnection("Server=host.docker.internal;port=3312;Database=Project-Tool-Database;User=root;Password=password123;");
        }

        // GetTask to get a task by ID
        public ITask GetTask(Guid taskId)
        {
            ITask task = new Task();
            _connection.Open();
            string query = "SELECT * FROM Task WHERE guid = @taskId";
            MySqlCommand command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@taskId", taskId);

            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                task = new Task
                {
                    Guid = (Guid)reader["guid"],
                    Description = reader["description"].ToString(),
                    Title = reader["title"].ToString(),
                    Deadline = (DateTime)reader["deadline"],
                    IsNew = (bool)reader["isNew"]
                };
            }
            _connection.Close();
            return task;
        }

        // GetAllTasks to get all tasks
        public IEnumerable<ITask> GetTasks()
        {
            _connection.Open();
            string query = "SELECT * FROM Task";
            MySqlCommand command = new MySqlCommand(query, _connection);

            using MySqlDataReader reader = command.ExecuteReader();

            List<ITask> tasks = new List<ITask>();

            while (reader.Read())
            {
                tasks.Add(new Task
                {
                    Guid = (Guid)reader["guid"],
                    Description = reader["description"].ToString(),
                    Title = reader["title"].ToString(),
                    Deadline = (DateTime)reader["deadline"],
                    IsNew = (bool)reader["isNew"]
                });
            }

            _connection.Close();
            return tasks;
        }

        // GetEmployeeTasks to get all tasks that are associated with a single employee by ID
        public IEnumerable<ITask> GetEmployeeTasks(Guid taskId)
        {
            _connection.Open();
            string query = "SELECT Task.* FROM Task RIGHT JOIN TaskProject ON Task.guid = TaskProject.taskGuid RIGHT JOIN Project ON TaskProject.projectGuid = Project.guid JOIN EmployeeProject ON Project.guid = EmployeeProject.projectGuid RIGHT JOIN Employee ON EmployeeProject.employeeGuid = Employee.guid WHERE Employee.guid = @taskId;";
            MySqlCommand command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@taskId", taskId);
            using MySqlDataReader reader = command.ExecuteReader();

            List<ITask> tasks = new List<ITask>();

            while (reader.Read())
            {
                tasks.Add(new Task
                {
                    Guid = (Guid)reader["guid"],
                    Description = reader["description"].ToString(),
                    Title = reader["title"].ToString(),
                    Deadline = (DateTime)reader["deadline"],
                    IsNew = (bool)reader["isNew"]
                });
            }

            _connection.Close();
            return tasks;
        }

        // GetProjectTasks to get all tasks that are associated with a single project by ID
        public IEnumerable<ITask> GetProjectTasks(Guid taskId)
        {
            _connection.Open();
            string query = "SELECT Task.* FROM TaskProject RIGHT JOIN Task ON TaskProject.taskGuid = Task.Guid WHERE projectGuid = @taskId";
            MySqlCommand command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@taskId", taskId);
            using MySqlDataReader reader = command.ExecuteReader();

            List<ITask> tasks = new List<ITask>();

            while (reader.Read())
            {
                tasks.Add(new Task
                {
                    Guid = (Guid)reader["taskGuid"],
                    Description = reader["description"].ToString(),
                    Title = reader["title"].ToString(),
                    Deadline = (DateTime)reader["deadline"],
                    IsNew = (bool)reader["isNew"]
                });
            }
            _connection.Close();
            return tasks; // Return null if the task with the specified ID is not found
        }

        // get all tasks associated with a certain goal
        public IEnumerable<ITask> GetGoalTasks(Guid taskId)
        {
            _connection.Open();
            string query = "SELECT Task.* FROM GoalTask INNER JOIN Task ON GoalTask.taskGuid = Task.Guid WHERE goalGuid = @taskId";
            MySqlCommand command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@taskId", taskId);
            using MySqlDataReader reader = command.ExecuteReader();

            List<ITask> tasks = new List<ITask>();

            while (reader.Read())
            {
                tasks.Add(new Task
                {
                    Guid = (Guid)reader["taskGuid"],
                    Description = reader["description"].ToString(),
                    Title = reader["title"].ToString(),
                    Deadline = (DateTime)reader["deadline"],
                    IsNew = (bool)reader["isNew"]
                });
            }
            _connection.Close();
            return tasks;
        }

        // PostTask to add a task
        public void PostTask(ITask task)
        {
            _connection.Open();
            string query = "INSERT INTO Task (guid, description, title, deadline, isNew) VALUES (@taskId, @description, @title, @deadline, @isNew)";
            using MySqlCommand command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@taskId", task.Guid);
            command.Parameters.AddWithValue("@description", task.Description);
            command.Parameters.AddWithValue("@title", task.Title);
            command.Parameters.AddWithValue("@deadline", task.Deadline);
            command.Parameters.AddWithValue("@isNew", task.IsNew);
            command.ExecuteNonQuery();

            _connection.Close();
        }
        // UpdateTask to edit a single task by ID
        public void UpdateTask(ITask task, Guid taskId)
        {
            _connection.Open();
            string query =
                "UPDATE Task SET description = @description, title = @title, deadline = @deadline, isNew = @isNew WHERE guid = @taskId";
            using MySqlCommand command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@description", task.Description);
            command.Parameters.AddWithValue("@title", task.Title);
            command.Parameters.AddWithValue("@deadline", task.Deadline);
            command.Parameters.AddWithValue("@isNew", task.IsNew);
            command.Parameters.AddWithValue("@taskId", taskId);

            command.ExecuteNonQuery();
            _connection.Close();
        }

        // DeleteTask to delete a task by ID
        public void DeleteTask(Guid taskId)
        {
            _connection.Open();
            string query = "DELETE FROM Task WHERE guid = @taskId";
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@taskId", taskId);

            command.ExecuteNonQuery();
            _connection.Close();
        }
    }
}
