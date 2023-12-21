using System;
using System.Collections.Generic;
using ProjectManagementTool2.Logic.Interfaces.IModels;

namespace ProjectManagementTool2.Models
{
    public class Goal : IGoal
    {
        public Guid Guid { get; set; }
        public List<ITask> Tasks { get; set; }
        public int CompletionPercentage { get; set; }
        public Guid ProjectGuid { get; set; }
        
        public Goal(Guid guid, List<ITask> tasks, int completionPercentage, Guid projectGuid)
        {
            Guid = guid;
            Tasks = tasks;
            CompletionPercentage = completionPercentage;
            ProjectGuid = projectGuid;
        }
    }
}