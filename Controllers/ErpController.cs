using ERP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.Controllers
{
    public class ErpController : Controller
    {
        private readonly DBContext _dBContext;
        public ErpController(DBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task<IActionResult> Inventory()
        {
            IEnumerable<Product> productsList = await _dBContext.Products.ToListAsync();
            return View(productsList);
        }
    }
}
