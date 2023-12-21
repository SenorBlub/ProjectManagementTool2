namespace ProjectManagementTool2.Logic.Interfaces.IRepositories.ILinkRepositories
{
    public interface ITaskProjectRepository
    {
        void PostTaskProject(Guid taskGuid, Guid projectGuid);
        void DeleteTaskProject(Guid taskGuid, Guid projectGuid);
    }
}