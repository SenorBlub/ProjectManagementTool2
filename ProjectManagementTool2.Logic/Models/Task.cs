using System;
using ProjectManagementTool2.Logic.Interfaces.IModels;

namespace ProjectManagementTool2.Models
{
    public class Task : ITask
    {
        public Guid Guid { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsNew { get; set; }

        public Task(Guid guid, string description, string title, DateTime deadline, bool isNew)
        {
            Guid = guid;
            Description = description;
            Title = title;
            Deadline = deadline;
            IsNew = isNew;
        }
    }
}