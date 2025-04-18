using System.ComponentModel.DataAnnotations;

namespace CustomersServices.ViewModels
{
    public class Res_CustomerVM
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
