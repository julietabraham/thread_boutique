using System.ComponentModel.DataAnnotations;

namespace Sample.Models
{
    public class Cart
    {
        [Key]
        public int RecId { get; set; }
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public DateTime Created { get; set; }
        public virtual Product Product { get; set; }

    }
}
