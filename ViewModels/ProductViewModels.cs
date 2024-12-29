using ERP.Models;

namespace ERP.ViewModels
{
    public class ProductViewModels
    {
        // List of customers
        public List<Customer> Customers { get; set; } = new List<Customer>();

        // List of products
        public List<Product> Products { get; set; } = new List<Product>();

        // List of sales
        public List<Sale> Sales { get; set; } = new List<Sale>();
    }
}
