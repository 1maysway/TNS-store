using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Diagnostics;
using System.Text.Json;
using TNS_store2.Models;
using TNS_store2.Services;
//using Newtonsoft.Json;

namespace TNS_store2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TnsDbContext tnsDbContext;
        private readonly ICartService cartService;
        private readonly IHistoryService historyService;
        

        public HomeController(ILogger<HomeController> logger, TnsDbContext tnsDbContext, ICartService cartService, IHistoryService historyService)
        {
            _logger = logger;
            this.tnsDbContext = tnsDbContext;
            this.cartService = cartService;
            this.historyService = historyService;
        }

        public IActionResult Index()
        {
            var products = tnsDbContext.Products;
            //Console.WriteLine("PPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPP" + products.ToList()[0].Id + products.ToList()[0].Name);
            var ProductsHistoryIds = new List<int>();
            //for (int i = 0; i < 4; i++)
            //{
            //    ProductsHistoryIds.Add(products.ToList()[i].Id);
            //}

            ViewBag.ProductsHistory = historyService.GetProducts(HttpContext);

            return View(products.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
        public async Task<IActionResult> Search(string search, int priceMin, int priceMax)
        {
            Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            if (search == null && ViewBag.Search != null)
            {
                search = ViewBag.search;
            }
            

            if (search != null)
            {
                if (tnsDbContext.Categories.Where(c => c.Name.Contains(search)).ToList().Count>0 || tnsDbContext.Products.Where(c => c.Name.Contains(search)).ToList().Count > 0)
                {
                    var products = tnsDbContext.Products.Where(p => p.Name.Contains(search));
                    if (products.ToList().Count == 0)
                    {
                        var categories = tnsDbContext.Categories.Where(c => c.Name.Contains(search));
                        products = tnsDbContext.Products.Where(p => p.CategoryId == categories.FirstOrDefault().Id);
                    }

                    ViewData["placeholdrePriceMin"] = Decimal.ToInt32(products.Min(p => p.Price));
                    ViewData["placeholdrePriceMax"] = Decimal.ToInt32(products.Max(p => p.Price));

                    if (priceMin > 0)
                    {
                        ViewData["priceMin"] = priceMin;
                        products = products.Where(x => x.Price >= priceMin);
                    }

                    if (priceMax > 0)
                    {
                        ViewData["priceMax"] = priceMax;
                        products = products.Where(x => x.Price <= priceMax);
                    }

                    ViewData["search"] = search;
                    return View(products.ToList());
                }
                else
                {
                    return RedirectToAction("Index","Home");
                }
            }
            else
            {
                return NotFound();
            }
        }
        public void AddToCart(int id,string returnUrl, string? search, int? priceMin, int? priceMax)
        {
            Dictionary<int, int> cart;
            if (!HttpContext.Session.Keys.Contains("cart"))
            {
                cart = new Dictionary<int, int>();
                cart.Add(id, 1);
                HttpContext.Session.SetString("cart", JsonSerializer.Serialize(cart));
            }
            else
            {
                cart = JsonSerializer.Deserialize<Dictionary<int, int>>(HttpContext.Session.GetString("cart"));

                if (cart.TryGetValue(id, out int value))
                {
                    cart[id] = value + 1;
                }
                else
                {
                    cart.Add(id, 1);
                }
            }


            HttpContext.Session.SetString("cart", JsonSerializer.Serialize(cart));




            //cartService.Add(HttpContext, id);

            //Console.WriteLine(returnUrl);
            //if (returnUrl == null)
            //{
            //    return RedirectToAction("Index");
            //}
            //else if (returnUrl.Contains("Search"))
            //{
            //    return RedirectToAction("Search", new { search = search, priceMin = priceMin, priceMax = priceMax });
            //}
            //else
            //{
            //    return Redirect(returnUrl);
            //}
        }
        public IActionResult Cart()
        {
            IEnumerable<Product> products = tnsDbContext.Products;

            Dictionary<int, int> cart;
            if (HttpContext.Session.Keys.Contains("cart"))
            {
                cart = JsonSerializer.Deserialize<Dictionary<int, int>>(HttpContext.Session.GetString("cart"));
                products = tnsDbContext.Products.ToList().Where(p => cart.ContainsKey(p.Id));
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
        //[HttpPost]
        //[Route("/RemoveHistoryItem")]
        public void RemoveHistoryItem(/*object data*/int id)
        {
            //var id = (int)data;
            Console.WriteLine("DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD  " + id);
            historyService.Remove(HttpContext, id);
            //return Task.CompletedTask;
        }

        public void RemoveFromCart(int id)
        {
            cartService.Remove(HttpContext, id);
        }
        public void ClearCart()
        {
            cartService.Clear(HttpContext);
        }
        public void DecrementCartItem(int id)
        {
            cartService.Decrement(HttpContext, id);
        }
    }
}