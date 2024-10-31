using DabaBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DabaBase.Controllers
{
    public class CustomerController : Controller
    {

        private readonly OnlineshopContext _context;

        public CustomerController(OnlineshopContext context)
        {
            _context = context;
        }

        //get home
        public async Task<IActionResult> Index()
        {
            return _context.Customers != null ?
                View(await _context.Customers.ToListAsync()) :
                Problem("Entity set 'OnlineshopDbContext.Customers' is null ");
        }

        //get home details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }
            var customers = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customers == null)
            {
                return NotFound();
            }
            return View(customers);
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

        public async Task<IActionResult> Create([Bind("CustomerId,CustomerFName,CustomerLName,Address,Phone,Postalcode,Password,CityId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }


        //get home edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,CustomerFName,CustomerLName,Address,Phone,Postalcode,Password,CityId")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(customer.CustomerId))
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

            return View(customer);
        }
        private bool ProductsExists(int customerId)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }


        //post home delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer); // Move the removal inside the if statement
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
