﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mvcApp.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage="Please enter your name")]
        [MaxLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage ="Please enter a valid email address")]
        [MaxLength(200, ErrorMessage = "Email cannot exceed 200 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your message")]
        [MaxLength(4000, ErrorMessage = "Message cannot exceed 4000 characters")]
        public string Message { get; set; }

        public DateTime? CompletedAt { get; set; }

        public bool HasErrored => !string.IsNullOrWhiteSpace(ErrorMessage);
      
        public string ErrorMessage { get; set; }
    }
}
