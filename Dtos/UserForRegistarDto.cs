using System;
using System.ComponentModel.DataAnnotations;

namespace AOA.API.Dtos
{
    public class UserForRegistarDto
    {
        [Required]
        public string Username { get; set; }

        [Required] 
        [StringLength(8, MinimumLength=4, ErrorMessage="You must specify password bettwen 4 and 8 chaeacters")]
        public string Password { get; set; }
    }

    








}