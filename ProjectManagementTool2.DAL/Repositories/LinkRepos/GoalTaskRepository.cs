using MySqlConnector;
using ProjectManagementTool2.Logic.Interfaces.IRepositories.ILinkRepositories;

namespace ProjectManagementTool2.DAL.Repositories.LinkRepos;

public class GoalTaskRepository : IGoalTaskRepository
{
    readonly MySqlConnection _connection;

    public GoalTaskRepository()
    {
        _connection = new MySqlConnection("Server=host.docker.internal;port=3312;Database=Project-Tool-Database;User=root;Password=password123;");
    }

    public void PostGoalTask(Guid goalGuid, Guid taskGuid)
    {
        try
        {
            _connection.Open();
            string query = "INSERT INTO GoalTask (goalGuid, taskGuid) VALUES (@goalGuid, @taskGuid)";
            using MySqlCommand command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@goalGuid", goalGuid);
            command.Parameters.AddWithValue("@taskGuid", taskGuid);

            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in PostGoalTask: {ex.Message}");
            throw;
        }
        finally
        {
            _connection.Close();
        }
    }

    public void DeleteGoalTask(Guid goalGuid, Guid taskGuid)
    {
        try
        {
            _connection.Open();
            string query = "DELETE FROM GoalTask WHERE goalGuid = @goalGuid AND taskGuid = @taskGuid";
            using MySqlCommand command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@goalGuid", goalGuid);
            command.Parameters.AddWithValue("@taskGuid", taskGuid);

            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in DeleteGoalTask: {ex.Message}");
            throw;
        }
        finally
        {
            _connection.Close();
        }
    }
}