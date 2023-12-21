using System;
using System.Collections.Generic;
using ProjectManagementTool2.Logic.Interfaces.IModels;

namespace ProjectManagementTool2.Logic.DTOs
{
    public class ProjectDTO
    {
        public Guid Guid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ITask> Tasks { get; set; }
        public IGoal Goal { get; set; }
        public List<IEmployee> Assignees { get; set; }
        public bool IsNew { get; set; }

        public ProjectDTO(Guid guid, string title, string description, List<ITask> tasks, IGoal goal, List<IEmployee> assignees, bool isNew)
        {
            Guid = guid;
            Title = title;
            Description = description;
            Tasks = tasks;
            Goal = goal;
            Assignees = assignees;
            IsNew = isNew;
        }
    }
}