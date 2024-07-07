using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using TodoList_CRUD_Practice.Models;

namespace TodoList_CRUD_Practice.Services
{
    public interface ITaskService
    {
        Task<List<Tasks>> GetAllTask();
        Task<Tasks> GetTaskById(int TaskId);
        Task<int> AddTask(Tasks Task);
        Task<int> UpdateTask(Tasks Task);
        Task<int> DeleteTask(int TaskId);
    }
    public class TaskService : ITaskService
    {
        private String _connectionString;

        public TaskService(string ConnectionString)
        {
            this._connectionString = ConnectionString;
        }

        public async Task<List<Tasks>> GetAllTask()
        {
            List<Tasks> tasks;

            using (IDbConnection conn = new SqlConnection(this._connectionString))
            {

                 string sqlQuery = "Select Id,Title,Description,DueDate from Tasks";
              //  string sqlQuery = "Select Id,Title,Description from Tasks";

                tasks = (await conn.QueryAsync<Tasks>(sqlQuery)).ToList();

            }

            return tasks;
        }

        public async  Task<Tasks> GetTaskById(int TaskId)
        {
            Tasks task = null;

            using (IDbConnection conn = new SqlConnection(this._connectionString))
            {
                string sqlQuery = "Select Id,Title,Description,DueDate from Tasks WHERE Id=@Id ";

                task = await conn.QueryFirstAsync<Tasks>(sqlQuery, new {Id=TaskId});
            }

            return task;
        }

        public async Task<int> AddTask(Tasks Task)
        {
            int GeneratedId;

            using (IDbConnection conn = new SqlConnection(this._connectionString))
            {
                string sqlQuery = "INSERT INTO Tasks (Title,Description,DueDate) OUTPUT INSERTED.Id VALUES " +
                    " (@Title, @Description,@DueDate)";

                GeneratedId = await conn.ExecuteScalarAsync<int>(sqlQuery,Task);

            }
            return GeneratedId;
        }

        public async Task<int> UpdateTask(Tasks Task)
        {
            int UpdatedRows=0;

            using (IDbConnection conn = new SqlConnection(this._connectionString))
            {

                string sqlQuery = "UPDATE Tasks  SET Title=@Title, Description= @Description, DueDate=@DueDate WHERE Id=@Id";

                UpdatedRows = (await conn.ExecuteAsync(sqlQuery, Task));

            }

            return UpdatedRows;
        }

        public async Task<int> DeleteTask(int TaskId)
        {
            int UpdatedRows = 0;

            using (IDbConnection conn = new SqlConnection(this._connectionString))
            {

                string sqlQuery = "DELETE FROM TaskS WHERE Id=@Id";

                UpdatedRows = (await conn.ExecuteAsync(sqlQuery, new {Id=TaskId}));

            }

            return UpdatedRows;
        }

    }
}
