using DabaBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DabaBase.Controllers
{
    public class OrderTblController : Controller
    {

        private readonly OnlineshopContext _context;

        public OrderTblController(OnlineshopContext context)
        {
            _context = context;
        }

        //get home
        public async Task<IActionResult> Index()
        {
            return _context.OrderTbls != null ?
                View(await _context.OrderTbls.ToListAsync()) :
                Problem("Entity set 'OnlineshopDbContext.PrderTbl' is null ");
        }

        //get home details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderTbls == null)
            {
                return NotFound();
            }
            var order = await _context.OrderTbls.FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        //get home create

        public IActionResult Create()
        {
            return View();
        }
        //post : home/create
        //to protect from overpostiong attacks , enable the specific properties you want to bind to
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("OrderId,OrderDate,IsDelivered,CustomerId,Discount")] OrderTbl order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }


        //get home edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.OrderTbls.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderDate,IsDelivered,CustomerId,Discount")] OrderTbl order)
        {
            if (id != order.OrderId)
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
                    if (!OrdersExists(order.OrderId))
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
        private bool OrdersExists(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderTbls == null)
            {
                return NotFound();
            }

            var order = await _context.OrderTbls.FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }


        //post home delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.OrderTbls.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.OrderTbls.Remove(order); // Move the removal inside the if statement
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
