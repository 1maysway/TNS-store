using TNS_store2.Models;
using TNS_store2.Services;

namespace TNS_store2.Models
{
    public class CartItemViewModel
    {
        public Product Product { get; set; }
        public int Amount { get; set; }
    }
}
