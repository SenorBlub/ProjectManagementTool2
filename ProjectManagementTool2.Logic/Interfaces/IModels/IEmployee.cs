using System;

namespace ProjectManagementTool2.Logic.Interfaces.IModels
{
    public interface IEmployee
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}