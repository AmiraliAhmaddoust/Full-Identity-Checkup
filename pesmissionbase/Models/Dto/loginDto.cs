using System.ComponentModel.DataAnnotations;

namespace pesmissionbase.Models.Dto
{
    public class loginDto
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsPersistent { get; set; } = false;
        public string ReturnUrl { get; set; }

    }
}
