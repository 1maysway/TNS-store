using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TNS_store2.Models;

namespace TNS_store2.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly TnsDbContext context;

        public CartViewComponent(TnsDbContext context)
        {
            this.context = context;
        }
        

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Product> products = context.Products;

            Dictionary<int, int> cart;
            if (HttpContext.Session.Keys.Contains("cart"))
            {
                cart = JsonSerializer.Deserialize<Dictionary<int, int>>(HttpContext.Session.GetString("cart"));
                products = context.Products.ToList().Where(p => cart.ContainsKey(p.Id));
                ViewBag.Count = cart.Sum(x => x.Value);
                ViewBag.Cart = cart;
                ViewBag.TotalPrice = products.Sum(x => x.Price * cart[x.Id]);

            }
            else
            {
                ViewBag.Count = 0;
                //ViewBag.Cart = new Dictionary<int, int>();
                ViewBag.TotalPrice = 0;
            }




            return View(products);
        }
    }
}
