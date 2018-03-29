using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MyApiProject.Models
{
    public class Operation
    {
        public string OpName { get; set; }
    }
    public class Numbers : IValidatableObject
    {
        public int N1 { get; set; }
        public int N2 { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (this.N1 == 0 && this.N2 == 0)
            {
                errors.Add(new ValidationResult("Both N1 and N2 cannot be 0" ));
            }
            return errors;
        }
    }
}