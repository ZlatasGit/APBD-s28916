namespace Models;
public interface ITaskRepository
{
    Task<IEnumerable<TaskDTO>> GetTasks(int? projectId = null);
    Task<int> AddTeamMember(TeamMember teamMember);
    Task<int> UpdateTask(int id, TaskModel task);
    Task<bool> FindProject(int id);
}