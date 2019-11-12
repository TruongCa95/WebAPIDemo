using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data
{
   public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Name is not empty")]
        [Column(TypeName = "nvarchar(200)")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is not empty")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required (ErrorMessage = "Password is not empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone number is not empty")]
        [DisplayName("Mobile Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public int? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Gender is not empty")]
        public int Gender { get; set; }

        [DisplayName("Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Column(TypeName = "varchar(200)")]
        [DisplayName("Email Opt-In")]
        public string EmailOptIn { get; set; }

        public string Token { get; set; }

    }
}
