using System.ComponentModel.DataAnnotations;


namespace ERP.Dto;
public class CustomerDTO
{
    [Required(ErrorMessage = "First Name is required.")]
    [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required.")]
    [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; }

    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
    public string Address { get; set; }
}
