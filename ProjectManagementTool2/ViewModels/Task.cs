using System;
using ProjectManagementTool2.Logic.Interfaces.IModels;

namespace ProjectManagementTool.Models
{
    public class Task : ITask
    {
        public Guid Guid { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public DateTime Deadline { get; set; }

        public bool IsNew { get; set; }
    }
}