using e_commerce_app.Server.Infrastructure.Repositories.Interfaces;

namespace e_commerce_app.Server.Infrastructure.unitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        Task<int> CompleteAsync();
    }
}
