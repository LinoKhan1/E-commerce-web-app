using e_commerce_app.Server.Core.Entities;

namespace e_commerce_app.Server.Infrastructure.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(int orderId);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order); 
        Task SaveChangesAsync();
    }
}
