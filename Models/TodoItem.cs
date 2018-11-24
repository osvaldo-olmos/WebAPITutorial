using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class TaskItem : IValidatableObject
    {
        public long Id { get; set; }
        public string Name { get; set; }
        [Range(0, 3)]
        public Status Status {get; set;}

        public TaskItem()
        {
            Status = Status.Todo;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results=new List<ValidationResult>();
            if (Name.Equals("sarasa"))
            {
                results.Add(new ValidationResult("Sea serio, y pongale un nombre como la gente a la tarea.", 
                                                new[] { "Name" }));
            }
            return results;
        }
    }
}