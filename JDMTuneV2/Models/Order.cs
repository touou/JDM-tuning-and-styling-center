using System;
using System.Linq;
using LinqToDB.Mapping;

namespace JDMTuneV2.Models
{
    [Table(Name = "Order")]
    public class Order
    {
        [Column(Name = "OrderId")]
        public Guid OrderId { get; set; }
        [Column(Name = "UserId")]
        public Guid UserId { get; set; }
        [Column(Name = "Name")]
        public string Name { get; set; }
        [Column(Name = "Surname")]
        public string Surname { get; set; }
        [Column(Name = "StreetAddress")]
        public string StreetAddress { get; set; }
        [Column(Name = "Country")]
        public string Country { get; set; }
        [Column(Name = "City")]
        public string City { get; set; }
        [Column(Name = "PostCode")]
        public string PostCode { get; set; }
        [Column(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }
        
        
    }
}