using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace ArchitectureProject.Domain.Models
{
    public class User
    {
        public int UserId { get; set; }
        [MaxLength(150)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string Email { get; set; }
        public DateTime AddedDate { get; set; }
        [ForeignKey("Role")]
        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public string Password { get; set; }
        [JsonIgnore]
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
    }
}
