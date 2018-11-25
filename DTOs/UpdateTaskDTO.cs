using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs
{
    public class UpdateTaskDTO
    {
        [Required]
        public string Name { get; set; }
        [Range (0,4)]
        public int Status { get; set; }

    }
}