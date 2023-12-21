using System;
using ProjectManagementTool2.Logic.Interfaces.IModels;

namespace ProjectManagementTool.Models
{
    public class Employee : IEmployee
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
    }
}