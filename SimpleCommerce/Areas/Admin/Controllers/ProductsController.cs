using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCommerce.Data;
using SimpleCommerce.Data.Entities;
using SimpleCommerce.DataTables;

namespace SimpleCommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ApplicationContext _context;

        public ProductsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Admin/Products
        public IActionResult Index()
        {
            //return View(await _context.Product.ToListAsync());
            return View();
        }
        // POST: Products/Filtered
        [HttpPost]
        public async Task<JsonResult> Filter(DataTablesRequest dataTablesRequest)
        {
            var recordsTotal = _context.Product.Count();
            IQueryable<Product> productsQuery = _context.Product.AsQueryable();

            var searchText = dataTablesRequest?.Search?.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.ToUpper().Contains(searchText) ||
                    p.Description.ToUpper().Contains(searchText)
                );
            }

            var recordsFiltered = productsQuery.Count();

            var sortColumnName = dataTablesRequest?.Columns?.ElementAt(dataTablesRequest?.Order?.ElementAt(0)?.Column ?? 0)?.Name;
            var sortDirection = dataTablesRequest?.Order?.ElementAt(0).Dir.ToLower();

            productsQuery = productsQuery.OrderBy($"{sortColumnName} {sortDirection}");

            var skip = dataTablesRequest.Start;
            var take = dataTablesRequest.Length;
            var data = await productsQuery
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return new JsonResult(new
            {
                Draw = dataTablesRequest.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsFiltered,
                Data = data
            });
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,DateAdded")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid();
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Price,DateAdded")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(Guid id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
