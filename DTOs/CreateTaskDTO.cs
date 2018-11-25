using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs
{
    public class CreateTaskDTO : IValidatableObject
    {
        public string Name {get; set;}

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