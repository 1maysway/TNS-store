using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TNS_store2.Models;
using TNS_store2.Services;

namespace TNS_store2.Controllers
{
    public class ProductController : Controller
    {
        private readonly TnsDbContext _context;
        private readonly IHistoryService historyService;

        public ProductController(TnsDbContext context, IHistoryService historyService)
        {
            this.historyService = historyService;
            _context = context;
        }
        
        // GET: ProductController
        public ActionResult Product(int id)
        {
            var product = _context.Products.Find(id);
            Console.WriteLine("PPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPP");
            Console.WriteLine(product.Id);

            ViewBag.CategoryName =  _context.Categories.Find(product.CategoryId).Name;

            historyService.Add(HttpContext, id);

            return View(product);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
