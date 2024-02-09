using Domain.Entity;
using Domain.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public interface ITaskService
    {
        Task<IBaseResponse<TaskEntity>> Create(TaskViewModel model);
        Task<IBaseResponse<TaskEntity>> Update(TaskViewModel model);
        Task<IBaseResponse<TaskEntity>> Remove(TaskViewModel model);
        Task<IBaseResponse<IEnumerable<TaskViewModel>>> GetTasks();
		Task<IBaseResponse<TaskViewModel>> GetTask(int Id);

	}
}
