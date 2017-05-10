using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace nab.Models
{
    public class UserEmail
    {
        [Required (ErrorMessage = "Please enter your name")]
        [Display(Name = "Name")]
        public string SenderName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail Address")]
        public string SenderEmailAddress { get; set; }

        [Required (ErrorMessage = "Please enter your message subject")]
        [Display(Name = "E-mail Subject")]
        public string EmailSubject { get; set; }

        [Required(ErrorMessage = "Please enter your message")]
        [MaxLength(5000)]
        [Display(Name = "E-mail Message")]
        public string EmailMessage { get; set; }
    }
}
