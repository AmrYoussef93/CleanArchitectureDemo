using CleanArchitectureDemo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureDemo.Domain.Entities
{
    public class Shipper : BaseEntity
    {
        public Shipper()
        {
            Orders = new HashSet<Order>();
        }
        public string CompanyName { get; set; }
        public string Phone { get; set; } 
        public ICollection<Order> Orders { get; private set; }
    }
}
