using ProductOrderAPI.Application.DTOs;
using ProductOrderAPI.Domain.Entities;
using ProductOrderAPI.Domain.Interfaces;

namespace ProductOrderAPI.Application.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepo;
    private readonly IRepository<Product> _productRepo;

    public OrderService(IOrderRepository orderRepo, IRepository<Product> productRepo)
    {
        _orderRepo = orderRepo;
        _productRepo = productRepo;
    }

    public async Task<PagedResultDto<OrderReadDto>> GetAllAsync(
        int pageNumber, int pageSize,
        DateTime? dateFrom, DateTime? dateTo)
    {
        var (orders, totalCount) = await _orderRepo.GetPagedAsync(
            pageNumber, pageSize, dateFrom, dateTo);

        var data = orders.Select(o =>
        {
            var totalPrice = OrderLogic.CalculateTotalPrice(o.Product.Price, o.Quantity); return new OrderReadDto
            {
                Id = o.Id,
                CustomerName = o.CustomerName,
                OrderDate = o.OrderDate,
                ProductName = o.Product?.Name ?? "",
                Quantity = o.Quantity,
                TotalPrice = totalPrice,
                IsExpensive = totalPrice > 100
            };
        });

        return new PagedResultDto<OrderReadDto>
        {
            Data = data,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
            CurrentPage = pageNumber
        };
    }

    public async Task<OrderReadDto?> GetByIdAsync(int id)
    {
        var order = await _orderRepo.GetByIdAsync(id);
        if (order == null) return null;

        var totalPrice = OrderLogic.CalculateTotalPrice(order.Product.Price, order.Quantity); return new OrderReadDto
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            OrderDate = order.OrderDate,
            ProductName = order.Product?.Name ?? "",
            Quantity = order.Quantity,
            TotalPrice = totalPrice,
            IsExpensive = totalPrice > 100
        };
    }

    public async Task<string?> CreateAsync(OrderCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.CustomerName))
            return "სახელი სავალდებულოა";
        if (!OrderLogic.CheckQuantityRange(dto.Quantity)) return "რაოდენობა უნდა იყოს მინიმუმ 1";

        var product = await _productRepo.GetByIdAsync(dto.ProductId);
        if (product == null)
            return "პროდუქტი არ არსებობს";

        var order = new Order
        {
            CustomerName = dto.CustomerName,
            OrderDate = DateTime.UtcNow,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity
        };

        await _orderRepo.AddAsync(order);
        return null;
    }

    public async Task<string?> UpdateAsync(int id, OrderCreateDto dto)
    {
        var order = await _orderRepo.GetByIdAsync(id);
        if (order == null) return "შეკვეთა არ მოიძებნა";

        if (string.IsNullOrWhiteSpace(dto.CustomerName))
            return "სახელი სავალდებულოა";
        if (!OrderLogic.CheckQuantityRange(dto.Quantity)) return "რაოდენობა უნდა იყოს მინიმუმ 1";

        var product = await _productRepo.GetByIdAsync(dto.ProductId);
        if (product == null)
            return "პროდუქტი არ არსებობს";

        order.CustomerName = dto.CustomerName;
        order.ProductId = dto.ProductId;
        order.Quantity = dto.Quantity;

        await _orderRepo.UpdateAsync(order);
        return null;
    }

    public async Task<string?> DeleteAsync(int id)
    {
        var order = await _orderRepo.GetByIdAsync(id);
        if (order == null) return "შეკვეთა არ მოიძებნა";

        await _orderRepo.DeleteAsync(order);
        return null;
    }
}