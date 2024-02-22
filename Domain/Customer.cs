using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Customer : BaseEntity
    {
        [Column(Order = 2, TypeName = "VARCHAR(100)")]
        public string Name { get; set; }

        [Column(Order = 3, TypeName = "VARCHAR(100)")]
        public string Contact { get; set; }

        [Column(Order = 4, TypeName = "VARCHAR(100)")]
        public string City { get; set; }

        [Column(Order = 5, TypeName = "VARCHAR(100)")]
        public string Email { get; set; }
    }
}