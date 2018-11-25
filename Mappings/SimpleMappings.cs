using AutoMapper;
using TaskApi.DTOs;
using TaskApi.Models;

namespace TaskApi.Mappings
{
    public class SimpleMappings : Profile
    {
        public SimpleMappings()
        {
            CreateMap<TaskItem, CreateTaskDTO>().ReverseMap();
            CreateMap<TaskItem, TaskDTO>();
            CreateMap<UpdateTaskDTO, TaskItem>();
        }
    }
}