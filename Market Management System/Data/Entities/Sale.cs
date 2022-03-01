using System;
using System.Collections;
using System.Collections.Generic;
using Market_Management_System.Data.Common;

namespace Market_Management_System.Data.Entities
{
    public class Sale : BaseEntity
    {
        private static int _count = 0;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public ICollection<SaleProduct> Products { get; set; }

        public Sale()
        {
            _count++;
            No = _count;
            Date = DateTime.Now;
        }
        
    }
}