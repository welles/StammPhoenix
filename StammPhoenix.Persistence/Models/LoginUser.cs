﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StammPhoenix.Persistence.Constants;

namespace StammPhoenix.Persistence.Models
{
    public class LoginUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; internal set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public bool IsLocked { get; set; }

        public bool NeedPasswordChange { get; set; }

        public Role Role { get; set; }
    }
}
