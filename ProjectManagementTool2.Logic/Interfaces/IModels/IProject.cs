using System;
using System.Collections.Generic;
using ProjectManagementTool.Logic.Interfaces.IModels;

namespace ProjectManagementTool2.Logic.Interfaces.IModels
{
    public interface IProject
    {
        public Guid Guid { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<ITask> Tasks { get; set; }

        public IGoal Goal { get; set; }

        public List<IEmployee> Assignees { get; set; }

        public bool IsNew { get; set; }
    }
}