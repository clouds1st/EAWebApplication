using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EA_API;

namespace EAWebApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApiContext _context;
        private readonly ILogger<ProductController> _logger;
        public ProductController(ApiContext context, ILogger<ProductController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
              return _context.Products != null ? 
                          View(await _context.Products.ToListAsync()) :
                          Problem("Entity set 'ApiContext.Products'  is null.");
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                _logger.Log(LogLevel.Trace, "No data for Id:" + id.ToString());
                return NotFound();
            }

            var eA_ProductsInMemory = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eA_ProductsInMemory == null)
            {
                _logger.Log(LogLevel.Error, "No data forfound");
                return NotFound();
            }
            _logger.Log(LogLevel.Information, "data available for id ;"+id.ToString());
            return View(eA_ProductsInMemory);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Productdesc,CreatedDate")] EA_ProductsInMemory eA_ProductsInMemory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eA_ProductsInMemory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            LogInvalid();
            return View(eA_ProductsInMemory);
        }

        private void LogInvalid()
        {
            _logger.Log(LogLevel.Error, "Invalid data");
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                LogNotfoundError();
                return NotFound();
            }

            var eA_ProductsInMemory = await _context.Products.FindAsync(id);
            if (eA_ProductsInMemory == null)
            {
                LogNotfoundError();
                return NotFound();
            }
            return View(eA_ProductsInMemory);
        }

        private void LogNotfoundError()
        {
            _logger.Log(LogLevel.Error, "data not available for id ;");
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Productdesc,CreatedDate")] EA_ProductsInMemory eA_ProductsInMemory)
        {
            if (id != eA_ProductsInMemory.Id)
            {
                LogNotfoundError();
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eA_ProductsInMemory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EA_ProductsInMemoryExists(eA_ProductsInMemory.Id))
                    {
                        LogNotfoundError();
                        return NotFound();
                    }
                    else
                    {
                        LogDbUpdateConcurrencyException();
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(eA_ProductsInMemory);
        }

        private void LogDbUpdateConcurrencyException()
        {
            _logger.Log(LogLevel.Error, "data DbUpdateConcurrencyException ;");
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                LogNotfoundError();
                return NotFound();
            }

            var eA_ProductsInMemory = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eA_ProductsInMemory == null)
            {
                LogNotfoundError();
                return NotFound();
            }

            return View(eA_ProductsInMemory);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApiContext.Products'  is null.");
            }
            var eA_ProductsInMemory = await _context.Products.FindAsync(id);
            if (eA_ProductsInMemory != null)
            {
                _context.Products.Remove(eA_ProductsInMemory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EA_ProductsInMemoryExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
