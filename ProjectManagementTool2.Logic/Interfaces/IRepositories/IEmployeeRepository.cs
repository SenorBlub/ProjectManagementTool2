﻿using ProjectManagementTool2.Logic.Interfaces.IModels;

namespace ProjectManagementTool2.Logic.Interfaces.IRepositories;

public interface IEmployeeRepository
{
    IEmployee GetEmployee(Guid id);
    List<IEmployee> GetEmployees();
    List<IEmployee> GetProjectEmployees(Guid id);
    void PostEmployee(IEmployee employee);
    void UpdateEmployee(IEmployee employee, Guid id);
    void DeleteEmployee(Guid id);
}