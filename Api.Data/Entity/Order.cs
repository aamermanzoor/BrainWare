namespace Api.Data.Entity
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public ICollection<OrderProduct> Products { get; set; }
    }
}