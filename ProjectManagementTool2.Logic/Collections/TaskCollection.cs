using System;
using System.Collections.Generic;
using ProjectManagementTool2.Logic.Interfaces.IModels;
using ProjectManagementTool2.Logic.Interfaces.IRepositories;
using ProjectManagementTool2.Logic.Interfaces.IRepositories.ILinkRepositories;

namespace ProjectManagementTool2.Logic.Collections
{
    public class TaskCollection
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskProjectRepository _taskProjectRepository;
        private readonly IGoalRepository _goalRepository;
        private readonly IGoalTaskRepository _goalTaskRepository;

        public TaskCollection(
            ITaskRepository taskRepository,
            ITaskProjectRepository taskProjectRepository,
            IGoalRepository goalRepository,
            IGoalTaskRepository goalTaskRepository)
        {
            _taskRepository = taskRepository;
            _taskProjectRepository = taskProjectRepository;
            _goalRepository = goalRepository;
            _goalTaskRepository = goalTaskRepository;
        }

        // Existing methods...

        public void AddTaskToProject(Guid taskId, Guid projectId)
        {
            var task = _taskRepository.GetTask(taskId);

            if (task != null)
            {
                _taskProjectRepository.PostTaskProject(taskId, projectId);
                Console.WriteLine($"Task with ID {taskId} added to the project.");
            }
            else
            {
                Console.WriteLine($"Task with ID {taskId} not found.");
            }
        }

        public void RemoveTaskFromProject(Guid taskId, Guid projectId)
        {
            var task = _taskRepository.GetTask(taskId);

            if (task != null)
            {
                _taskProjectRepository.DeleteTaskProject(taskId, projectId);
                Console.WriteLine($"Task with ID {taskId} removed from the project.");
            }
            else
            {
                Console.WriteLine($"Task with ID {taskId} not found.");
            }
        }

        public void AddTaskToGoal(Guid taskId, Guid goalId)
        {
            var task = _taskRepository.GetTask(taskId);

            if (task != null)
            {
                _goalTaskRepository.PostGoalTask(goalId, taskId);
                Console.WriteLine($"Task with ID {taskId} added to the goal.");
            }
            else
            {
                Console.WriteLine($"Task with ID {taskId} not found.");
            }
        }

        public void RemoveTaskFromGoal(Guid taskId, Guid goalId)
        {
            var task = _taskRepository.GetTask(taskId);

            if (task != null)
            {
                _goalTaskRepository.DeleteGoalTask(goalId, taskId);
                Console.WriteLine($"Task with ID {taskId} removed from the goal.");
            }
            else
            {
                Console.WriteLine($"Task with ID {taskId} not found.");
            }
        }
    }
}
