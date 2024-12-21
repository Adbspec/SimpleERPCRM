using System.ComponentModel.DataAnnotations;

namespace ERP.Dto
{
    public class AuthDto
    {
        [Required]
        public string LoginId { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool PersistantLogin { get; set; }
    }
}
