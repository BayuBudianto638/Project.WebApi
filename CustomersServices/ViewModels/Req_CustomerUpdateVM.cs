using System.ComponentModel.DataAnnotations;

namespace CustomersServices.ViewModels
{
    public class Req_CustomerUpdateVM
    {
        public int CustomerId { get; set; }
        [Required]
        [MaxLength(50)]
        public string CustomerCode { get; set; }
        [Required]
        [MaxLength(255)]
        public string CustomerName { get; set; }
        [Required]
        [MaxLength(1000)]
        public string CustomerAddress { get; set; } = string.Empty;
    }
}
