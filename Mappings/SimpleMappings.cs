using AutoMapper;
using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.Mappings
{
    public class SimpleMappings : Profile
    {
        public SimpleMappings()
        {
            CreateMap<TaskItem, CreateTaskDTO>().ReverseMap();
        }
    }
}