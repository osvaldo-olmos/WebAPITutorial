using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class TaskItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public TaskStatus Status { get; set; }

        public TaskItem()
        {
            Status =TaskStatus.Todo;
        }
    }
}