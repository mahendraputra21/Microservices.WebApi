using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Product : BaseEntity
    {
        [Column(Order = 2, TypeName = "VARCHAR(50)")]
        public string? ProductGUID { get; set; }

        [Column(Order = 3, TypeName = "VARCHAR(100)")]
        public string? ProductName { get; set; }

        [Column(Order = 4, TypeName = "VARCHAR(50)")]
        public string? Manufacture { get; set; }

        [Column(Order = 5)]
        public int Stock { get; set; }

        [Column(Order = 6)]
        public decimal Price { get; set; }

        [Column(Order = 7, TypeName = "VARCHAR(200)")]
        public string? Description { get; set; }

        [Column(Order = 8, TypeName ="DateTime")]
        public DateTime CreatedDate { get; set; }

        [Column(Order = 9, TypeName = "VARCHAR(50)")]
        public string? CreatedBy { get; set; }

        [Column(Order = 10, TypeName = "DateTime")]
        public DateTime UpdatedDate { get; set; }

        [Column(Order = 11, TypeName = "VARCHAR(50)")]
        public string? UpdatedBy { get; set; }
    }
}
