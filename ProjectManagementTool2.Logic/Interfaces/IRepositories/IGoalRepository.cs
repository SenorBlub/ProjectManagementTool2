using ProjectManagementTool2.Logic.Interfaces.IModels;

namespace ProjectManagementTool2.Logic.Interfaces.IRepositories;

public interface IGoalRepository
{
    IGoal GetGoal(Guid id);
    IEnumerable<IGoal> GetGoals();
    void PostGoal(IGoal goal);
    void UpdateGoal(IGoal goal, Guid id);
    void DeleteGoal(Guid id);
}