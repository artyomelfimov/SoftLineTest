using Domain.Entity;
using Domain.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestApplication.Models;

namespace TestApplication.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskViewModel model)
        {
            var response = await _taskService.Create(model);
            if (response.StatusCode == ResponseCode.OK)
            {
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tasks = await _taskService.GetTasks();
            return View(tasks.Data);
        }
        public async Task<IActionResult> Create()
		{
			return View();
		}
        public async Task<IActionResult> Edit(int Id)
        {
            var task = await _taskService.GetTask(Id);
            return View(task.Data);
        }
        [HttpPost]
        public async Task<IActionResult> AddTask(TaskViewModel task)
        {
            if (task == null)
            {
                return BadRequest("null");
            }
            await _taskService.Create(task);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Update(TaskViewModel task)
        {
            if (task == null)
            {
                return BadRequest("null");
            }
            await _taskService.Update(task);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int Id)
        {
            var task = await _taskService.GetTask(Id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task.Data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(TaskViewModel task)
        {
            await _taskService.Remove(task);
            return RedirectToAction("Index");
        }
    }
}
