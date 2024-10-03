using e_commerce_app.Server.Infrastructure.Repositories.Interfaces;

namespace e_commerce_app.Server.Infrastructure.unitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        ICartRepository CartRepository { get; }
        IOrderRepository OrderRepository { get; }

        Task<int> CompleteAsync();
    }
}
