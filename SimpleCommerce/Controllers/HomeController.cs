using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;
using SimpleCommerce.Data;
using SimpleCommerce.Data.Entities;
using SimpleCommerce.DataTables;
using SimpleCommerce.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SimpleCommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;

        public HomeController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Home
        public IActionResult Index()
        {
            return View();
        }

        // POST: Home/Filter
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
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
