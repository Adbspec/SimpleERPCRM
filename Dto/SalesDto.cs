namespace ERP.Dto
{
    public class SalesDto
    {
    
        public int CustomerId { get; set; }
        public string ProductSku { get; set; }
        public int QuantitySold { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
