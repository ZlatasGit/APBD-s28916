namespace Models;
public class TaskDTO
{
    public int IdTask { get; set; }
    public string Name { get; set; }
    public string TaskTypeName { get; set; }
    public string ProjectName { get; set; }
    public string CreatorLastName { get; set; }
    public string AssigneeLastName { get; set; }
    public string Description { get; set; }
    public DateOnly Deadline { get; set; }
    public int IdTeam { get; set; }
    public int IdTaskType { get; set; }
    public int IdAssignedTo { get; set; }
    public int IdCreator { get; set; }
}