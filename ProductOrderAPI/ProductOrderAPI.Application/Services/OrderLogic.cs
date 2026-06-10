namespace ProductOrderAPI.Application.Services;

public static class OrderLogic
{
    public static decimal CalculateTotalPrice(decimal price, int quantity)
    {
        return price * quantity;
    }

    public static bool CheckQuantityRange(int quantity)
    {
        return quantity >= 1;
    }

    public static bool ValidateCategory(int categoryId)
    {
        return categoryId > 0;
    }
}