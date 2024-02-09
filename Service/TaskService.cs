using DBALayer.Interfaces;
using Domain.Entity;
using Domain.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class TaskService : ITaskService
    {
        private readonly IBaseRepo<TaskEntity> _taskRepository;

        public TaskService(IBaseRepo<TaskEntity> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IBaseResponse<TaskEntity>> Create(TaskViewModel model)
        {
            try
            {

                var task = await _taskRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Name == model.TaskName);
                if (task != null)
                {
                    return new Response<TaskEntity>()
                    {
                        Description = "Задача с таким названием уже есть",
                        StatusCode = ResponseCode.TaskIsAlreadyExists
                    };
                }

                task = new TaskEntity()
                {
                    Name = model.TaskName,
                    Description = model.TaskDescription,
                    StatusId = (int)model.StatusName,
                };
                await _taskRepository.Create(task);

                return new Response<TaskEntity>()
                {
                    Description = "Задача создалась",
                    StatusCode = ResponseCode.OK
                };

            }
            catch (Exception ex)
            {
                return new Response<TaskEntity>()
                {
                    StatusCode = ResponseCode.InternalServerError
                };
            }
        }
		public async Task<IBaseResponse<IEnumerable<TaskViewModel>>> GetTasks()
		{
			try
			{
				var tasks = await _taskRepository.GetAll()
					.Select(x => new TaskViewModel()
					{
						Id = x.Id,
						TaskName = x.Name,
						TaskDescription = x.Description,
						StatusName = (Domain.Enums.StatusName)x.StatusId,
					})
					.ToListAsync();

				return new Response<IEnumerable<TaskViewModel>>()
				{
					Data = tasks,
					StatusCode = ResponseCode.OK
				};
			}
			catch (Exception ex)
			{
				return new Response<IEnumerable<TaskViewModel>>()
				{
					Description = $"{ex.Message}",
					StatusCode = ResponseCode.InternalServerError
				};
			}
		}

		public async Task<IBaseResponse<TaskViewModel>> GetTask(int Id)
		{
			try
			{
				var task = await _taskRepository.GetAll().FirstOrDefaultAsync(x => x.Id == Id);
				if (task == null)
				{
					return new Response<TaskViewModel>()
					{
						Description = "Задача не найдена",
						StatusCode = ResponseCode.InternalServerError
						
					};
				}

				var taskModel = new TaskViewModel()
				{
					Id = task.Id,
					TaskName = task.Name,
					TaskDescription = task.Description,
					StatusName = (Domain.Enums.StatusName)task.StatusId,
				};

				return new Response<TaskViewModel>()
				{
					Data = taskModel,
					StatusCode = ResponseCode.OK
				};
			}
			catch (Exception ex)
			{
				return new Response<TaskViewModel>()
				{
					Description = $"{ex.Message}",
					StatusCode = ResponseCode.InternalServerError
				};
			}
		}
        public async Task<IBaseResponse<TaskEntity>> Update(TaskViewModel model)
        {
            try
            {

                var task = await _taskRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == model.Id);
                if (task == null)
                {
                    return new Response<TaskEntity>()
                    {
                        Description = "Не удалось изменить",
                        StatusCode = ResponseCode.InternalServerError
                    };
                }
                task.Name = model.TaskName;
                task.Description = model.TaskDescription;
                task.StatusId = (int)model.StatusName;

                
                await _taskRepository.Update(task);

                return new Response<TaskEntity>()
                {
                    Description = "Задача обновлена",
                    StatusCode = ResponseCode.OK
                };

            }
            catch (Exception ex)
            {
                return new Response<TaskEntity>()
                {
                    StatusCode = ResponseCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<TaskEntity>> Remove(TaskViewModel model)
        {
            try
            {

                var task = await _taskRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == model.Id);
                if (task == null)
                {
                    return new Response<TaskEntity>()
                    {
                        Description = "Не удалось удалить",
                        StatusCode = ResponseCode.InternalServerError
                    };
                }
                await _taskRepository.Delete(task);

                return new Response<TaskEntity>()
                {
                    Description = "Задача удалена",
                    StatusCode = ResponseCode.OK
                };

            }
            catch (Exception ex)
            {
                return new Response<TaskEntity>()
                {
                    StatusCode = ResponseCode.InternalServerError
                };
            }
        }
    }
}
