using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Shape.Requests
{
    public class SignupRequest
    {
        [Required]
        [StringLength(12)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(16)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
