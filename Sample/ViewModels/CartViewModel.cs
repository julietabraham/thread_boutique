using MessagePack;
using Sample.Models;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace Sample.ViewModels
{
    public class CartViewModel
    {
        [Key]
        public int Id { get; set; }
        public List<Cart> CartItems { get; set; }
        public decimal? CartTotal { get; set; }
    }
}
