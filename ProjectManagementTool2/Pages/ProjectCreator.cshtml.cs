using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManagementTool2.Logic.Collections;
using ProjectManagementTool2.Logic.Interfaces.IModels;
using System;
using ProjectManagementTool2.DAL.Repositories;
using ProjectManagementTool2.DAL.Repositories.LinkRepos;
using ProjectManagementTool2.Models;
using ProjectManagementTool2.Models.NonBasicModels;
using Task = ProjectManagementTool2.Models.Task;

namespace ProjectManagementTool2.Pages
{
    public class ProjectCreatorModel : PageModel
    {
        [BindProperty]
        public ProjectCreationModel ProjectCreationModel { get; set; } = new ProjectCreationModel();

        private readonly TaskCollection _taskCollection;
        private readonly EmployeeCollection _employeeCollection;
        private readonly ProjectCollection _projectCollection;

        public ProjectCreatorModel(
            TaskCollection taskCollection,
            EmployeeCollection employeeCollection,
            ProjectCollection projectCollection)
        {
            _taskCollection = taskCollection;
            _employeeCollection = employeeCollection;
            _projectCollection = projectCollection;
        }

        public IActionResult OnPostCreateTask()
        {
            ProjectCreationModel = HttpContext.Session.GetObject<ProjectCreationModel>("ProjectCreationModel") ?? new ProjectCreationModel();

            // Retrieve form values
            var taskTitle = Request.Form["TaskTitle"];
            var taskDescription = Request.Form["TaskDescription"];
            var taskDeadline = Request.Form["TaskDeadline"];

            // Validate and create new task
            if (!string.IsNullOrWhiteSpace(taskTitle) && DateTime.TryParse(taskDeadline, out var deadline))
            {
                ITask newTask = new Task(Guid.NewGuid(), taskDescription, taskTitle, deadline, true);

                // Add the task to ProjectCreationModel.Tasks
                ProjectCreationModel.Tasks.Add(newTask);

                // Save ProjectCreationModel to session
                HttpContext.Session.SetObject("ProjectCreationModel", ProjectCreationModel);
            }
            else
            {
                // Handle invalid input, if needed
            }

            // Redirect back to the page
            return RedirectToPage();
        }

        public IActionResult OnPostAddAssignee()
        {
            ProjectCreationModel = HttpContext.Session.GetObject<ProjectCreationModel>("ProjectCreationModel") ?? new ProjectCreationModel();

            // Retrieve form values
            var employeeIdString = Request.Form["AssigneeID"];

            if (Guid.TryParse(employeeIdString, out Guid employeeId))
            {
                _employeeCollection.AddEmployeeToProject(employeeId, ProjectCreationModel.Project.Guid);

                // Add a debug statement to check the count after adding
                Console.WriteLine($"Number of employees after adding: {ProjectCreationModel.Employees.Count}");

                return RedirectToPage();
            }
            else
            {
                return BadRequest("Invalid employee ID");
            }
        }

        public IActionResult OnPostProjectOnly()
        {
            ProjectCreationModel = HttpContext.Session.GetObject<ProjectCreationModel>("ProjectCreationModel") ?? new ProjectCreationModel();

            // Retrieve form values
            string projectTitle = Request.Form["ProjectTitle"];
            string projectDescription = Request.Form["ProjectDescription"];

            // Validate and create project
            if (!string.IsNullOrWhiteSpace(projectTitle))
            {
                IProject newProject = new Project
                {
                    Title = projectTitle,
                    Description = projectDescription,
                    Guid = Guid.NewGuid(),
                    IsNew = true,
                    Goal = new Goal
                    {
                        CompletionPercentage = 0,
                        Guid = Guid.Empty,
                        ProjectGuid = Guid.Empty
                    }
                };

                foreach (ITask task in ProjectCreationModel.Tasks)
                {
                    _taskCollection.AddTaskToProject(task.Guid, ProjectCreationModel.Project.Guid);
                }

                _projectCollection.AddProject(ProjectCreationModel.Project);

                foreach (Guid employee in ProjectCreationModel.Employees)
                {
                    _employeeCollection.AddEmployeeToProject(employee, ProjectCreationModel.Project.Guid);
                }

                // Save ProjectCreationModel to session or database, depending on your needs
                HttpContext.Session.SetObject("ProjectCreationModel", ProjectCreationModel);

                // Redirect back to the page or another page as needed
                return RedirectToPage();
            }
            else
            {
                // Handle invalid input for the project if needed
                return BadRequest("Invalid project input");
            }
        }

        public void OnGet()
        {
            ProjectCreationModel = HttpContext.Session.GetObject<ProjectCreationModel>("ProjectCreationModel") ?? new ProjectCreationModel();

            Console.WriteLine($"Number of employees: {ProjectCreationModel.Employees.Count}");
        }
    }
}
