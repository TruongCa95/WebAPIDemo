using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data
{
   public class Users
    {
        [Key]
        public string UserId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public int? PhoneNumber { get; set; }
        public int Gender { get; set; }
        public int? Age { get; set; }
        public string Token { get; set; }

    }
}
