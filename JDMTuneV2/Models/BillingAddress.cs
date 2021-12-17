using System;
using System.Linq;
using LinqToDB.Mapping;

namespace JDMTuneV2.Models
{
    [Table(Name = "BillingAddress")]
    public class BillingAddress
    {
        [Column(Name = "UserId")]
        public Guid UserId { get; set; }
        [Column(Name = "Country")]
        public string Country { get; set; }
        [Column(Name = "City")]
        public string City { get; set; }
        [Column(Name = "PostIndex")]
        public string PostIndex { get; set; }
        [Column(Name = "FullAddress")]
        public string FullAddress { get; set; }
    }
}