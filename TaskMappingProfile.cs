using AutoMapper;
using TasksWebApi.Domains;
using TasksWebApi.Models;

namespace TasksWebApi
{
    public class TaskMappingProfile: Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<Task, TaskDto>();
            CreateMap<CreateTaskDto, Task>();
            CreateMap<RegisterUserDto, User>();
        }    
    }
}
