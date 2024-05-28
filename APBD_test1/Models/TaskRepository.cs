
using System.ComponentModel;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace Models;
public class TaskRepository : ITaskRepository
{
    private readonly IConfiguration _configuration;
    public TaskRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /*2. Design an endpoint that allows you to add a new team member. 
    Remember to return correct error codes with error messages. 
    Endpoint should return the id of the newly created row in database.*/

    public async Task<int> AddTeamMember(TeamMember teamMember)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        using SqlCommand command = new SqlCommand();
        
        command.Connection = connection;
        command.CommandText = "INSERT INTO TeamMember(FirstName, LastName, Email) OUTPUT INSERTED.IdTeamMember VALUES(@FirstName, @LastName, @Email)";
        command.Parameters.AddWithValue("@FirstName", teamMember.FirstName);
        command.Parameters.AddWithValue("@LastName", teamMember.LastName);
        command.Parameters.AddWithValue("@Email", teamMember.Email);

        await connection.OpenAsync();
        var newId = await command.ExecuteScalarAsync();
        return Convert.ToInt32(newId);
    }

    /*1. Design an endpoint that will allow you to return data about tasks to be performed. 
    An optional parameter is the project id through which we can get a list of tasks related solely to this project. 
    By default we should return all the tasks. Endpoint should return tasks in descending order of deadlines. 
    In the data returned from the endpoint we should include all the data from the Task table, the name of the project, 
    the last name of the person who created the task, the last name of the person to whom the task was assigned, 
    and the name of the task type.
    
    */
    public async Task<IEnumerable<TaskDTO>> GetTasks(int? projectId = null)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT t.IdTask, t.Name, tt.Name as TaskTypeName, p.Name as ProjectName, d.LastName as CreatorLastName, a.LastName as AssigneeLastName, t.Description, t.Deadline, t.IdTeam, t.IdTaskType, t.IdAssignedTo, t.IdCreator FROM Task t JOIN TaskType tt ON t.IdTaskType = tt.IdTaskType JOIN Project p ON t.IdProject = p.IdProject JOIN Doctor d ON t.IdCreator = d.IdDoctor JOIN Doctor a ON t.IdAssignedTo = a.IdDoctor";
        
        if (projectId.HasValue)
        {
            command.CommandText += " WHERE t.IdProject = @Id";
            command.Parameters.AddWithValue("@Id", projectId.Value);
        }
        
        command.CommandText += " ORDER BY t.Deadline DESC";

        await connection.OpenAsync();
        var reader = await command.ExecuteReaderAsync();
        var tasks = new List<TaskDTO>();
        
        DateOnlyConverter dateOnlyConverter = new DateOnlyConverter();
        while (await reader.ReadAsync())
        {
            var task = new TaskDTO()
            {
                IdTask = reader.GetInt32(reader.GetOrdinal("IdTask")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                TaskTypeName = reader.GetString(reader.GetOrdinal("TaskTypeName")),
                ProjectName = reader.GetString(reader.GetOrdinal("ProjectName")),
                CreatorLastName = reader.GetString(reader.GetOrdinal("CreatorLastName")),
                AssigneeLastName = reader.GetString(reader.GetOrdinal("AssigneeLastName")),
                Description = reader.GetString(reader.GetOrdinal("Description")),
                Deadline = (DateOnly)dateOnlyConverter.ConvertFrom(reader.GetDateTime(reader.GetOrdinal("Deadline")).ToString()),
                IdTeam = reader.GetInt32(reader.GetOrdinal("IdTeam")),
                IdTaskType = reader.GetInt32(reader.GetOrdinal("IdTaskType")),
                IdAssignedTo = reader.GetInt32(reader.GetOrdinal("IdAssignedTo")),
                IdCreator = reader.GetInt32(reader.GetOrdinal("IdCreator"))
            };
            
            
            tasks.Add(task);
        }
        
        return tasks;
    }
    
    /*3. Design an endpoint that allows you to edit one of the existing tasks. 
    Remember to return correct error codes with error messages. 
    Endpoint should return id of the edited row in database.*/

    public async Task<int> UpdateTask(int idTask, TaskModel task)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        using SqlCommand command = new SqlCommand();

        DateOnlyConverter dateOnlyConverter = new DateOnlyConverter();

        command.Connection = connection;
        command.CommandText = "Update Task set Name = @Name, Description = @Description, Deadline = @Deadline, IdTeam = @IdTeam, IdTaskType = @IdTaskType, IdAssignedTo = @IdAssignedTo, IdCreator = @IdCreator where IdTask = @IdTask";
        command.Parameters.AddWithValue("@IdTask", idTask);
        command.Parameters.AddWithValue("@Name", task.Name);
        command.Parameters.AddWithValue("@Description", task.Description);
        var deadline = (DateOnly)dateOnlyConverter.ConvertFrom(task.Deadline.ToString("yyyy-MM-dd"));
        command.Parameters.AddWithValue("@Deadline", deadline);
        command.Parameters.AddWithValue("@IdTeam", task.IdTeam);
        command.Parameters.AddWithValue("@IdTaskType", task.IdTaskType);
        command.Parameters.AddWithValue("@IdAssignedTo", task.IdAssignedTo);
        command.Parameters.AddWithValue("@IdCreator", task.IdCreator);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();

        return idTask;
    }
    public async Task<bool> FindProject(int idProject)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT 1 FROM Project WHERE IdProject = @IdProject";
        command.Parameters.AddWithValue("@IdProject", idProject);

        await connection.OpenAsync();

        var result = await command.ExecuteScalarAsync();
        return result is not null;
    }
}