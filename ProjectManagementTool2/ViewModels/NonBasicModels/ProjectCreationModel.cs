namespace ProjectManagementTool2.Models.NonBasicModels;

public class ProjectCreationModel
{
    public Project Project { get; set; }
    public List<Task> Tasks { get; set; } = new List<Task>();
    public List<Guid> Employees = new List<Guid>();
}