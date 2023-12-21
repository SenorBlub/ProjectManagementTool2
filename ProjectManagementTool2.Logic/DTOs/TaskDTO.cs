using System;

namespace ProjectManagementTool2.Logic.DTOs
{
    public class TaskDTO
    {
        public Guid Guid { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsNew { get; set; }

        public TaskDTO(Guid guid, string description, string title, DateTime deadline, bool isNew)
        {
            Guid = guid;
            Description = description;
            Title = title;
            Deadline = deadline;
            IsNew = isNew;
        }
    }
}