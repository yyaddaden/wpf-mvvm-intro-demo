using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace simple_task_manager_rest_api.Controllers
{
    [Produces("application/json")]
    [Route("api/Tasks")]
    public class TaskController : ControllerBase
    {
        private Models.SimpleTaskManagerDbContext _dbContext;

        public TaskController()
        {
            _dbContext = new Models.SimpleTaskManagerDbContext();
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetTasks()
        {
            try
            {
                List<Models.Task> tasks = _dbContext.Tasks.ToList();
                return StatusCode((int)HttpStatusCode.OK, tasks);
            }
            catch (Exception) { }

            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Models.Task), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult AddTask([FromBody] Models.Task model)
        {
            try
            {
                model.Status = "Inachevée";
                _dbContext.Tasks.Add(model);
                _dbContext.SaveChanges();
                return StatusCode((int)HttpStatusCode.Created, model);
            }
            catch (Exception) { }

            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpDelete("{taskId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult RemoveTask(int taskId)
        {
            try
            {
                Models.Task task = _dbContext.Tasks.Find(taskId);
                if (task != null)
                {
                    _dbContext.Tasks.Remove(task);
                    _dbContext.SaveChanges();
                    return StatusCode((int)HttpStatusCode.OK);
                }
            }
            catch (Exception) { }

            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpPut("{taskId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult CompleteTask(int taskId)
        {
            try
            {
                Models.Task task = _dbContext.Tasks.Find(taskId);
                if(task != null)
                {
                    task.Status = (task.Status == "Achevée") ? "Inachevée" : "Achevée";
                    _dbContext.SaveChanges();
                    return StatusCode((int)HttpStatusCode.OK);
                }
            }
            catch (Exception) { }

            return StatusCode((int)HttpStatusCode.BadRequest);
        }
    }
}
