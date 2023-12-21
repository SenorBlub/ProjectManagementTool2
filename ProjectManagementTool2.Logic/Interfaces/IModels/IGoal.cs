using System;
using System.Collections.Generic;
using ProjectManagementTool.Logic.Interfaces.IModels;

namespace ProjectManagementTool2.Logic.Interfaces.IModels
{
    public interface IGoal
    {
        public Guid Guid { get; set; }
        public List<ITask> Tasks { get; set; }
        public int CompletionPercentage { get; set; }
        public Guid ProjectGuid { get; set; }
    }
}