using System.Text.Json;
using TNS_store2.Models;

namespace TNS_store2.Services
{
    public class CartService : ICartService
    {
        private readonly TnsDbContext context;
        public CartService(TnsDbContext context)
        {
            this.context = context;
        }
        public void Add(HttpContext httpContext, int id)
        {
            Dictionary<int, int> cart;
            if (!httpContext.Session.Keys.Contains("cart"))
            {
                cart = new Dictionary<int, int>();
                cart.Add(id, 1);
                httpContext.Session.SetString("cart", JsonSerializer.Serialize(cart));
            }
            else
            {
                cart = JsonSerializer.Deserialize<Dictionary<int, int>>(httpContext.Session.GetString("cart"));

                if (cart.TryGetValue(id, out int value))
                {
                    cart[id] = value + 1;
                }
                else
                {
                    cart.Add(id, 1);
                }
            }

            httpContext.Session.SetString("cart", JsonSerializer.Serialize(cart));
        }

        public void CLear(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CartItemViewModel> GetProducts(HttpContext httpContext)
        {
            IEnumerable<Product> products = null;
            if (httpContext.Session.Keys.Contains("cart"))
            {
                var cart = JsonSerializer.Deserialize<Dictionary<int, int>>(httpContext.Session.GetString("cart"));
                products = context.Products.ToList().Where(p => cart.ContainsKey(p.Id));

                return products.Select(p => new CartItemViewModel
                {
                    Product = p,
                    Amount = cart[p.Id]
                });
            }
            return new List<CartItemViewModel>();
        }

        public void Remove(HttpContext httpContext, int id)
        {
            Dictionary<int, int> cart;
            cart = JsonSerializer.Deserialize<Dictionary<int, int>>(httpContext.Session.GetString("cart"));
            cart.Remove(id);

            httpContext.Session.SetString("cart", JsonSerializer.Serialize(cart));
        }
        public void Decrement(HttpContext httpContext, int id)
        {
            Dictionary<int, int> cart;
            cart = JsonSerializer.Deserialize<Dictionary<int, int>>(httpContext.Session.GetString("cart"));
            if (cart.TryGetValue(id, out int value))
            {
                if (value > 1)
                {
                    cart[id] = value - 1;
                }
                else
                {
                    cart.Remove(id);
                }
            }
            httpContext.Session.SetString("cart", JsonSerializer.Serialize(cart));
        }
        public void Clear(HttpContext httpContext)
        {
            httpContext.Session.Remove("cart");
        }
    }
}
