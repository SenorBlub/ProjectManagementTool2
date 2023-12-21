using MySqlConnector;
using ProjectManagementTool2.Logic.Interfaces.IRepositories.ILinkRepositories;
using System;

namespace ProjectManagementTool2.DAL.Repositories.LinkRepos
{
    public class ProjectGoalRepository : IProjectGoalRepository
    {
        readonly MySqlConnection _connection;

        public ProjectGoalRepository()
        {
            _connection = new MySqlConnection("Server=host.docker.internal;port=3312;Database=Project-Tool-Database;User=root;Password=password123;");
        }

        public void PostProjectGoal(Guid projectGuid, Guid goalGuid)
        {
            _connection.Open();
            string query = "INSERT INTO ProjectGoal (projectGuid, goalGuid) VALUES (@projectGuid, @goalGuid)";
            using MySqlCommand command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@projectGuid", projectGuid);
            command.Parameters.AddWithValue("@goalGuid", goalGuid);

            command.ExecuteNonQuery();
            _connection.Close();
        }

        public void DeleteProjectGoal(Guid projectGuid, Guid goalGuid)
        {
            _connection.Open();
            string query = "DELETE FROM ProjectGoal WHERE projectGuid = @projectGuid AND goalGuid = @goalGuid";
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@projectGuid", projectGuid);
            command.Parameters.AddWithValue("@goalGuid", goalGuid);

            command.ExecuteNonQuery();
            _connection.Close();
        }
    }
}