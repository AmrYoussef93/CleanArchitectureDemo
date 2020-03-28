using CleanArchitectureDemo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureDemo.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
