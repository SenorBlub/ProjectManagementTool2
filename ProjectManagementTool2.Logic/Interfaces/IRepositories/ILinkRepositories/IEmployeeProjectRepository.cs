namespace ProjectManagementTool2.Logic.Interfaces.IRepositories.ILinkRepositories
{
    public interface IEmployeeProjectRepository
    {
        void PostEmployeeProject(Guid employeeGuid, Guid projectGuid);
        void DeleteEmployeeProject(Guid employeeGuid, Guid projectGuid);
    }
}