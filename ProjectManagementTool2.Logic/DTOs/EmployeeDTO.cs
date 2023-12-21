using System;

namespace ProjectManagementTool2.Logic.DTOs
{
    public class EmployeeDTO
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public EmployeeDTO(Guid guid, string name, string email)
        {
            Guid = guid;
            Name = name;
            Email = email;
        }
    }
}