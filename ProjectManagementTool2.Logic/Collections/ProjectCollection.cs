using System;
using System.Collections.Generic;
using ProjectManagementTool2.Logic.Interfaces.IModels;
using ProjectManagementTool2.Logic.Interfaces.IRepositories;
using ProjectManagementTool2.Logic.Interfaces.IRepositories.ILinkRepositories;

namespace ProjectManagementTool2.Logic.Collections
{
    public class ProjectCollection
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskProjectRepository _taskProjectRepository;
        private readonly IProjectGoalRepository _projectGoalRepository;

        public ProjectCollection(
            IProjectRepository projectRepository,
            ITaskProjectRepository taskProjectRepository,
            IProjectGoalRepository projectGoalRepository)
        {
            _projectRepository = projectRepository;
            _taskProjectRepository = taskProjectRepository;
            _projectGoalRepository = projectGoalRepository;
        }

        public void AddProject(IProject project)
        {
            _projectRepository.PostProject(project);
        }

        public void RemoveProject(Guid projectId)
        {
            _projectRepository.DeleteProject(projectId);
        }

        public void DisplayAllProjects()
        {
            var projects = _projectRepository.GetProjects();
            foreach (var project in projects)
            {
                Console.WriteLine($"Project ID: {project.Guid}, Title: {project.Title}, Description: {project.Description}, isNew: {project.IsNew}");
            }
        }

        // Other methods for updating and displaying projects

        public void AddEmployeeToProject(Guid projectId, Guid employeeId)
        {
            _taskProjectRepository.PostTaskProject(employeeId, projectId);
            Console.WriteLine($"Employee with ID {employeeId} added to the project.");
        }

        public void RemoveEmployeeFromProject(Guid projectId, Guid employeeId)
        {
            _taskProjectRepository.DeleteTaskProject(employeeId, projectId);
            Console.WriteLine($"Employee with ID {employeeId} removed from the project.");
        }

        public void AddTaskToProject(Guid projectId, Guid taskId)
        {
            _taskProjectRepository.PostTaskProject(taskId, projectId);
            Console.WriteLine($"Task with ID {taskId} added to the project.");
        }

        public void RemoveTaskFromProject(Guid projectId, Guid taskId)
        {
            _taskProjectRepository.DeleteTaskProject(taskId, projectId);
            Console.WriteLine($"Task with ID {taskId} removed from the project.");
        }

        public void AddGoalToProject(Guid projectId, Guid goalId)
        {
            _projectGoalRepository.PostProjectGoal(projectId, goalId);
            Console.WriteLine($"Goal with ID {goalId} added to the project.");
        }

        public void RemoveGoalFromProject(Guid projectId, Guid goalId)
        {
            _projectGoalRepository.DeleteProjectGoal(projectId, goalId);
            Console.WriteLine($"Goal with ID {goalId} removed from the project.");
        }
    }
}
