using DabaBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DabaBase.Controllers
{
    public class OrderDtlController : Controller
    {

        private readonly OnlineshopContext _context;

        public OrderDtlController(OnlineshopContext context)
        {
            _context = context;
        }

        //get home
        public async Task<IActionResult> Index()
        {
            return _context.OrderDtls != null ?
                View(await _context.OrderDtls.ToListAsync()) :
                Problem("Entity set 'OnlineshopDbContext.OD' is null ");
        }

        //get home details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderDtls == null)
            {
                return NotFound();
            }
            var ord = await _context.OrderDtls.FirstOrDefaultAsync(m => m.OrderdetailId == id);
            if (ord == null)
            {
                return NotFound();
            }
            return View(ord);
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

        public async Task<IActionResult> Create([Bind("OrderdetailId,ProductId,OrderId,PerPrice,Discount,Quantity")] OrderDtl orderDtl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDtl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderDtl);
        }


        //get home edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDtl = await _context.OrderDtls.FindAsync(id);

            if (orderDtl == null)
            {
                return NotFound();
            }

            return View(orderDtl);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderdetailId,ProductId,OrderId,PerPrice,Discount,Quantity")] OrderDtl orderDtl)
        {
            if (id != orderDtl.OrderdetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDtl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!orderDtlExists(orderDtl.OrderdetailId))
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

            return View(orderDtl);
        }
        private bool orderDtlExists(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderDtls == null)
            {
                return NotFound();
            }

            var orderDtl = await _context.OrderDtls.FirstOrDefaultAsync(m => m.OrderdetailId == id);
            if (orderDtl == null)
            {
                return NotFound();
            }

            return View(orderDtl);
        }


        //post home delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDtl = await _context.OrderDtls.FindAsync(id);
            if (orderDtl == null)
            {
                return NotFound();
            }

            _context.OrderDtls.Remove(orderDtl); // Move the removal inside the if statement
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> GetProductQuantities()
        {
            var queryResult = await _context.OrderDtls
                .Join(_context.OrderTbls,
                    orderDtl => orderDtl.OrderId,
                    order => order.OrderId,
                    (orderDtl, order) => new
                    {
                        orderDtl.ProductId,
                        orderDtl.Quantity,
                        order.OrderDate
                    })
                .Where(joinedData => joinedData.OrderDate >= new DateTime(2024, 1, 1) && joinedData.OrderDate <= new DateTime(2024, 6, 6))
                .GroupBy(joinedData => joinedData.ProductId)
                .Select(groupedData => new
                {
                    ProductId = groupedData.Key,
                    TotalQuantity = groupedData.Sum(x => x.Quantity)
                })
                .OrderByDescending(result => result.TotalQuantity)
                .ToListAsync();

            return View("Index", queryResult); 
        }
    }
}
