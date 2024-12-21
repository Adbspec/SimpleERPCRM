using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace ERP.Dto
{
    public class CustomerInteractionsDto
    {
        [Required(ErrorMessage = "Interaction ID is required.")]
        public int InteractionId { get; set; }

        [Required(ErrorMessage = "Customer ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Customer ID must be a positive number.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Interaction type is required.")]
        [StringLength(50, ErrorMessage = "Interaction type cannot exceed 50 characters.")]
        public string InteractionType { get; set; } = null!;

        [Required(ErrorMessage = "Details are required.")]
        [StringLength(1000, ErrorMessage = "Details cannot exceed 1000 characters.")]
        public string Details { get; set; } = null!;

        [Required(ErrorMessage = "Timestamp is required.")]
        public DateTime Timestamp { get; set; }
    }
}
