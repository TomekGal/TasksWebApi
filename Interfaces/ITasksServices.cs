using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TasksWebApi.Domains;
using TasksWebApi.Models;

namespace TasksWebApi.Interfaces
{
    public interface ITasksServices
    {
        TaskDto GetbyId( int id);
        IEnumerable<TaskDto> GetAll();
        int Create(CreateTaskDto taskDto);
        void Delete(int id);
        void Update(int id, CreateTaskDto taskDto);
      
    }
}
