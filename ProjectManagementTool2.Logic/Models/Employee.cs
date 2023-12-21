using System;
using ProjectManagementTool2.Logic.Interfaces.IModels;

namespace ProjectManagementTool2.Models
{
    public class Employee : IEmployee
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public Employee(Guid guid, string name, string email)
        {
            Guid = guid;
            Name = name;
            Email = email;
        }
    }
}