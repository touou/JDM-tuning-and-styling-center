using System;
using System.Linq;
using LinqToDB.Mapping;


namespace JDMTuneV2.Models
{
    [Table(Name = "Clothes")]
    public class Сlothes
    {
        [Column("ClotheId")]
        public Guid ClotheId { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Picture")]
        public string Picture { get; set; }
        [Column("Price")]
        public string Price { get; set; }
        [Column("ShortDesc")]
        public string ShortDescription { get; set; }
    }
}