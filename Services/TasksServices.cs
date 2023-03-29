using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TasksWebApi.Authorization;
using TasksWebApi.Domains;
using TasksWebApi.Exceptions;
using TasksWebApi.Interfaces;
using TasksWebApi.Models;

namespace TasksWebApi.Services
{
    public class TasksServices : ITasksServices
    {
        private readonly TasksDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<TasksServices> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public TasksServices(TasksDbContext context, IMapper mapper, ILogger<TasksServices> logger,IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }
        public TaskDto GetbyId( int id)
        {
            var userId = _userContextService.GetUserId;
            var task = _context.ToDos.SingleOrDefault(x => x.Id == id && x.CreatedById==userId);

            if (task is null)
                throw new NotFoundException("Nie znaleziono zadania");

            var result = _mapper.Map<TaskDto>(task);
            return result;
        }

        public IEnumerable<TaskDto> GetAll()
        {
            var userId = _userContextService.GetUserId;
               var tasks = _context.ToDos.Where(x=>x.CreatedById==userId).ToList();
            
            var result = _mapper.Map<List<TaskDto>>(tasks);
            return result;
        }


        public int Create(CreateTaskDto taskDto)
        {
          
            taskDto.CreatingDate = DateTime.Now;
            var task = _mapper.Map<Task>(taskDto);
            task.CreatedById = _userContextService.GetUserId;
          
            _context.ToDos.Add(task);
            _context.SaveChanges();

            return task.Id;
        }

        public void Delete(int id)
        {
           
            var taskToDelete = _context.ToDos.FirstOrDefault(x => x.Id == id);
            if (taskToDelete is null)
                throw new NotFoundException("Nie znaleziono zadania");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, taskToDelete,
               new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("Brak autoryzacji dla zalogowanego użytkownika");
            }

            _context.ToDos.Remove(taskToDelete);
            _context.SaveChanges();
          
        }

        public void Update(int id, CreateTaskDto taskDto)
        {
            var taskToUpdate = _context.ToDos.FirstOrDefault(x => x.Id == id);
            if (taskToUpdate is null)
                throw new NotFoundException("Nie znaleziono zadania");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, taskToUpdate,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if(!authorizationResult.Succeeded)
            {
                throw new ForbidException("Brak autoryzacji dla zalogowanego użytkownika");
            }

            var task = _mapper.Map<Task>(taskDto);

            taskToUpdate.Title = task.Title;
            if(!(task.Description is null))taskToUpdate.Description = task.Description;
            if(task.ExpiredDate.HasValue) taskToUpdate.ExpiredDate = task.ExpiredDate;
            taskToUpdate.IsDone = task.IsDone;
            _context.SaveChanges();
           
        }
    }
}