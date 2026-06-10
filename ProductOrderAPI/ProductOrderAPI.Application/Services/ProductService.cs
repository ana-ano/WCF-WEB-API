using ProductOrderAPI.Application.DTOs;
using ProductOrderAPI.Domain.Entities;
using ProductOrderAPI.Domain.Interfaces;

namespace ProductOrderAPI.Application.Services;

public class ProductService
{
    private readonly IRepository<Product> _productRepo;
    private readonly IRepository<Category> _categoryRepo;

    public ProductService(IRepository<Product> productRepo, IRepository<Category> categoryRepo)
    {
        _productRepo = productRepo;
        _categoryRepo = categoryRepo;
    }

    public async Task<IEnumerable<ProductReadDto>> GetAllAsync(
        decimal? minPrice, decimal? maxPrice,
        int? categoryId, string? sortBy, string? sortDirection)
    {
        var products = await _productRepo.GetAllAsync();

        if (minPrice.HasValue)
            products = products.Where(p => p.Price >= minPrice.Value);
        if (maxPrice.HasValue)
            products = products.Where(p => p.Price <= maxPrice.Value);
        if (categoryId.HasValue)
            products = products.Where(p => p.CategoryId == categoryId.Value);

        if (sortBy?.ToLower() == "price")
        {
            if (sortDirection?.ToLower() == "desc")
                products = products.OrderByDescending(p => p.Price);
            else
                products = products.OrderBy(p => p.Price);
        }
        else if (sortBy?.ToLower() == "name")
        {
            if (sortDirection?.ToLower() == "desc")
                products = products.OrderByDescending(p => p.Name);
            else
                products = products.OrderBy(p => p.Name);
        }

        return products.Select(p => new ProductReadDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            CategoryName = p.Category?.Name ?? ""
        });
    }

    public async Task<ProductReadDto?> GetByIdAsync(int id)
    {
        var product = await _productRepo.GetByIdAsync(id);
        if (product == null) return null;

        return new ProductReadDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            CategoryName = product.Category?.Name ?? ""
        };
    }

    public async Task<string?> CreateAsync(ProductCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return "სახელი სავალდებულოა";
        if (dto.Price <= 0)
            return "ფასი უნდა იყოს 0-ზე მეტი";

        var category = await _categoryRepo.GetByIdAsync(dto.CategoryId);
        if (category == null)
            return "კატეგორია არ არსებობს";

        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            CategoryId = dto.CategoryId
        };

        await _productRepo.AddAsync(product);
        return null;
    }

    public async Task<string?> UpdateAsync(int id, ProductCreateDto dto)
    {
        var product = await _productRepo.GetByIdAsync(id);
        if (product == null) return "პროდუქტი არ მოიძებნა";

        if (string.IsNullOrWhiteSpace(dto.Name))
            return "სახელი სავალდებულოა";
        if (dto.Price <= 0)
            return "ფასი უნდა იყოს 0-ზე მეტი";

        var category = await _categoryRepo.GetByIdAsync(dto.CategoryId);
        if (category == null)
            return "კატეგორია არ არსებობს";

        product.Name = dto.Name;
        product.Price = dto.Price;
        product.CategoryId = dto.CategoryId;

        await _productRepo.UpdateAsync(product);
        return null;
    }

    public async Task<string?> DeleteAsync(int id)
    {
        var product = await _productRepo.GetByIdAsync(id);
        if (product == null) return "პროდუქტი არ მოიძებნა";

        await _productRepo.DeleteAsync(product);
        return null;
    }
}