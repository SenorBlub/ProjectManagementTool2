using System;
using System.Collections.Generic;
using ProjectManagementTool2.Logic.Interfaces.IModels;
using ProjectManagementTool2.Logic.Interfaces.IRepositories;
using ProjectManagementTool2.Logic.Interfaces.IRepositories.ILinkRepositories;

namespace ProjectManagementTool2.Logic.Collections
{
    public class GoalCollection
    {
        private readonly IGoalRepository _goalRepository;
        private readonly IGoalTaskRepository _goalTaskRepository;
        private readonly IProjectGoalRepository _projectGoalRepository;

        public GoalCollection(
            IGoalRepository goalRepository,
            IGoalTaskRepository goalTaskRepository,
            IProjectGoalRepository projectGoalRepository)
        {
            _goalRepository = goalRepository;
            _goalTaskRepository = goalTaskRepository;
            _projectGoalRepository = projectGoalRepository;
        }

        public void AddGoal(IGoal goal)
        {
            _goalRepository.PostGoal(goal);
        }

        public void RemoveGoal(Guid goalId)
        {
            _goalRepository.DeleteGoal(goalId);
        }

        public void DisplayAllGoals()
        {
            var goals = _goalRepository.GetGoals();
            foreach (var goal in goals)
            {
                Console.WriteLine($"Goal ID: {goal.Guid}, Completion Percentage: {goal.CompletionPercentage}, Project ID: {goal.ProjectGuid}");
            }
        }

        public void DisplayGoalById(Guid goalId)
        {
            var goal = _goalRepository.GetGoal(goalId);
            if (goal != null)
            {
                Console.WriteLine($"Goal ID: {goal.Guid}, Completion Percentage: {goal.CompletionPercentage}, Project ID: {goal.ProjectGuid}");
            }
            else
            {
                Console.WriteLine($"Goal with ID {goalId} not found.");
            }
        }

        public void UpdateGoal(Guid goalId, int newCompletionPercentage, Guid newProjectGuid)
        {
            var existingGoal = _goalRepository.GetGoal(goalId);
            if (existingGoal != null)
            {
                existingGoal.CompletionPercentage = newCompletionPercentage;
                existingGoal.ProjectGuid = newProjectGuid;
                _goalRepository.UpdateGoal(existingGoal, goalId);
                Console.WriteLine($"Goal with ID {goalId} updated successfully.");
            }
            else
            {
                Console.WriteLine($"Goal with ID {goalId} not found.");
            }
        }

        public void AddTaskToGoal(Guid goalId, ITask task)
        {
            var existingGoal = _goalRepository.GetGoal(goalId);
            if (existingGoal != null)
            {
                _goalTaskRepository.PostGoalTask(goalId, task.Guid);
                Console.WriteLine($"Task with ID {task.Guid} added to the goal.");
            }
            else
            {
                Console.WriteLine($"Goal with ID {goalId} not found.");
            }
        }

        public void RemoveTaskFromGoal(Guid goalId, Guid taskId)
        {
            var existingGoal = _goalRepository.GetGoal(goalId);
            if (existingGoal != null)
            {
                _goalTaskRepository.DeleteGoalTask(goalId, taskId);
                Console.WriteLine($"Task with ID {taskId} removed from the goal.");
            }
            else
            {
                Console.WriteLine($"Goal with ID {goalId} not found.");
            }
        }

        public void AddGoalToProject(Guid goalId, Guid projectId)
        {
            var existingGoal = _goalRepository.GetGoal(goalId);
            if (existingGoal != null)
            {
                _projectGoalRepository.PostProjectGoal(projectId, goalId);
                Console.WriteLine($"Goal with ID {goalId} added to the project.");
            }
            else
            {
                Console.WriteLine($"Goal with ID {goalId} not found.");
            }
        }

        public void RemoveGoalFromProject(Guid goalId, Guid projectId)
        {
            var existingGoal = _goalRepository.GetGoal(goalId);
            if (existingGoal != null)
            {
                _projectGoalRepository.DeleteProjectGoal(projectId, goalId);
                Console.WriteLine($"Goal with ID {goalId} removed from the project.");
            }
            else
            {
                Console.WriteLine($"Goal with ID {goalId} not found.");
            }
        }
    }
}
