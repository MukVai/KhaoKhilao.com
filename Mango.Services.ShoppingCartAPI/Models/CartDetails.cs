using Mango.Services.ShoppingCartAPI.Models.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.ShoppingCartAPI.Models
{
    public class CartDetails    // for a particular cart header
    {
        [Key]
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }
        [ForeignKey("CartHeaderId")]
        public CartHeaderDTO CartHeader { get; set; }
        public int ProductId { get; set; }
        [NotMapped]
        public ProductDTO Product { get; set; }
        public int Count { get; set; }
    }
}
