using System;

namespace ProjectManagementTool2.Logic.Interfaces.IRepositories.ILinkRepositories
{
    public interface IProjectGoalRepository
    {
        void PostProjectGoal(Guid projectGuid, Guid goalGuid);
        void DeleteProjectGoal(Guid projectGuid, Guid goalGuid);
    }
}