using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductOrderAPI.Application.DTOs
{
    public class OrderCreateDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderReadDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsExpensive { get; set; }
    }
}
