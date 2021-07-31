using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities.Common;

namespace Entities
{
    public class Users :  BaseEntity
    {
        public Users()
        {
            IsActive = true;
        }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; }
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }

        public ICollection<Post> Posts { get; set; }
    }

  

    public enum GenderType
    {
        [Display(Name = "مرد")]
        Male = 1,

        [Display(Name = "زن")]
        Female = 2
    }
}