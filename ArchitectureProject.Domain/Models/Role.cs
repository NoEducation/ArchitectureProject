using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArchitectureProject.Domain.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        [MaxLength(25)]
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime AddedDate { get; set; }
        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
