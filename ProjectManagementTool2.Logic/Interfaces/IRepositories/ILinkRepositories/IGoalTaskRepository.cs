namespace ProjectManagementTool2.Logic.Interfaces.IRepositories.ILinkRepositories
{
    public interface IGoalTaskRepository
    {
        void PostGoalTask(Guid goalGuid, Guid taskGuid);
        void DeleteGoalTask(Guid goalGuid, Guid taskGuid);
    }
}