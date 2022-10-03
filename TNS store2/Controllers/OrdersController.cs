using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TNS_store2.Models;
using TNS_store2.Models;
using TNS_store2.Services;

namespace TNS_store2.Controllers
{
    public class OrdersController : Controller
    {
        private readonly TnsDbContext _context;
        private readonly ICartService cartService;
        private readonly IMailSender mailSender;

        public OrdersController(TnsDbContext context, ICartService cartService, IMailSender mailSender)
        {
            _context = context;
            this.cartService = cartService;
            this.mailSender = mailSender;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
              return View(await _context.Orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Address,City,State,ZipCode,Country,Phone,Email,OrderTotal,OrderPlaced")] Order order)
        {
            if (ModelState.IsValid)
            {
                var cartItem = cartService.GetProducts(HttpContext);

                order.OrderTotal = cartItem.Sum(x => x.Product.Price * x.Amount);

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                var orderProduct = cartItem.Select(x => new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = x.Product.Id
                });

                await _context.OrderProducts.AddRangeAsync(orderProduct);
                await _context.SaveChangesAsync();

                cartService.Clear(HttpContext);

                string str = "";
                foreach (var item in cartItem)
                {
                    str += item.Product.Name + " " + item.Amount + " " + item.Product.Price + " " + item.Product.Price * item.Amount;
                }

                await mailSender.sendAsync(order.Email, "Order confirmation", "Your order has been placed");

                return RedirectToAction("Index", "Home");
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Address,City,State,ZipCode,Country,Phone,Email,OrderTotal,OrderPlaced")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'TnsDbContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return _context.Orders.Any(e => e.Id == id);
        }
    }
}
