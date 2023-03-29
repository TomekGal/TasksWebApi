using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TasksWebApi.Interfaces;
using TasksWebApi.Models;

namespace TasksWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
      
        private readonly ITasksServices _taskServices;

        public ToDoController( ITasksServices taskServices)
        {
            
            _taskServices = taskServices;
        }
        [HttpGet]
        public ActionResult<IEnumerable<TaskDto>> GetAll()
        {
            var tasksDto = _taskServices.GetAll();

            return Ok(tasksDto);
        }
      

        [HttpGet("{id}")]
        public ActionResult<TaskDto> GetbyId([FromRoute] int id)
        {
            var task = _taskServices.GetbyId(id);
                             
            return Ok(task);
        }
        [HttpPost]
       
        public ActionResult CreateTask([FromBody]CreateTaskDto taskDto)
        {
           
            var id= _taskServices.Create(taskDto);
            return Created($"/api/ToDo/{id}",null);
        }

        [HttpDelete("{id}")]

        public ActionResult Delete([FromRoute]int id)
        {
            _taskServices.Delete(id);

           return NoContent();
                      
        }

        [HttpPut("{id}")]
        public  ActionResult Update([FromRoute]int id,[FromBody] CreateTaskDto taskDto)
        {

            _taskServices.Update(id,taskDto);
            return Ok();

           
        }
    }
}
