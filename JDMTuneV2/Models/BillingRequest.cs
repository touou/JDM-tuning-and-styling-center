using System;
using System.ComponentModel.DataAnnotations;

namespace JDMTuneV2.Models
{
    public class BillingRequest
    {
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostIndex { get; set; }
        [Required]
        public string FullAddress { get; set; }
    }
}