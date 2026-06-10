using ProductOrderAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductOrderAPI.Domain.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<(IEnumerable<Order> Orders, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            DateTime? dateFrom,
            DateTime? dateTo
        );
    }
}
