using ProjectManagementTool2.Logic.Interfaces.IModels;

namespace ProjectManagementTool2.Logic.Interfaces.IRepositories;

public interface ITaskRepository
{
    ITask GetTask(Guid id);
    IEnumerable<ITask> GetTasks();
    IEnumerable<ITask> GetEmployeeTasks(Guid id);
    IEnumerable<ITask> GetProjectTasks(Guid id);
    IEnumerable<ITask> GetGoalTasks(Guid id);
    void PostTask(ITask task);
    void UpdateTask(ITask task, Guid id);
    void DeleteTask(Guid id);
}