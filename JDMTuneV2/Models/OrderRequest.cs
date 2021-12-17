using System.ComponentModel.DataAnnotations;

namespace JDMTuneV2.Models
{
    public class OrderRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}