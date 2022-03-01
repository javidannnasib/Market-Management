using Market_Management_System.Data.Common;

namespace Market_Management_System.Data.Entities
{
    public class SaleProduct : BaseEntity 
    {
        private static int _count = 0;
        public Product ProductCode { get; set; }
        public int Quantity { get; set; }
        public Sale Sale { get; set; }
        public decimal Price { get; set; }

         public SaleProduct()
        {
            _count++;
            No = _count;
        }
        
    }
}