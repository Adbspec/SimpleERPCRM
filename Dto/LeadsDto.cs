using System.ComponentModel.DataAnnotations;

namespace ERP.Dto
{
    public class LeadDto
    {


        [Required(ErrorMessage = "Name is required.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 255 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Name can only contain letters and spaces.")]
        public string Name { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(15, MinimumLength = 7, ErrorMessage = "Phone number must be between 7 and 15 digits.")]
        public string? PhoneNumber { get; set; }

        public string? Status { get; set; }
    }
}
