using Market_Management_System.Data.Common;
using Market_Management_System.Data.Enums;

namespace Market_Management_System.Data.Entities
{
    public class Product : BaseEntity
    {
        private static int _count = 0;
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Code { get; set; }
        
        public int Quantity { get; set; }
        public ProductCategory Category { get; set; }

        public Product()
        {
            _count++;
            No = _count;
        }

    }
}