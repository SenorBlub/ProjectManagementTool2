using MySqlConnector;
using ProjectManagementTool2.Logic.Interfaces.IRepositories.ILinkRepositories;

namespace ProjectManagementTool2.DAL.Repositories.LinkRepos;

public class TaskProjectRepository : ITaskProjectRepository
{
    readonly MySqlConnection _connection;

    public TaskProjectRepository()
    {
        _connection = new MySqlConnection("Server=host.docker.internal;port=3312;Database=Project-Tool-Database;User=root;Password=password123;");
    }

    public void PostTaskProject(Guid taskGuid, Guid projectGuid)
    {
        try
        {
            _connection.Open();
            string query = "INSERT INTO TaskProject (taskGuid, projectGuid) VALUES (@taskGuid, @projectGuid)";
            using MySqlCommand command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@taskGuid", taskGuid);
            command.Parameters.AddWithValue("@projectGuid", projectGuid);

            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in PostTaskProject: {ex.Message}");
            throw;
        }
        finally
        {
            _connection.Close();
        }
    }

    public void DeleteTaskProject(Guid taskGuid, Guid projectGuid)
    {
        try
        {
            _connection.Open();
            string query = "DELETE FROM TaskProject WHERE taskGuid = @taskGuid AND projectGuid = @projectGuid";
            using MySqlCommand command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@taskGuid", taskGuid);
            command.Parameters.AddWithValue("@projectGuid", projectGuid);

            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            // Handle the exception, log it, or throw a custom exception
            Console.WriteLine($"Error in DeleteTaskProject: {ex.Message}");
            // Optionally, throw the exception for higher-level handling
            throw;
        }
        finally
        {
            _connection.Close();
        }
    }
}