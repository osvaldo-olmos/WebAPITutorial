using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTO
{
    public class UpdateTaskDTO
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        [Range(0,4)]
        public int? Status { get; set; }
    }
}