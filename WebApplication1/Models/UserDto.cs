using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace WebApplication1.Models
{
    public class UserDto : IValidatableObject
    {

        [Required]
        [StringLength(100)]
        public string UserName { get; set; } 
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public bool IsActive { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (UserName.Equals("test",StringComparison.OrdinalIgnoreCase)) yield return new ValidationResult("نام کاربری نمیتواند test باشد");
            if (Password.Equals("123456")) yield return new ValidationResult("Shity Password");
            if (Gender == GenderType.Male && Age > 30) yield return new ValidationResult("Invalid Age for Mens.");







        }
    }
}
