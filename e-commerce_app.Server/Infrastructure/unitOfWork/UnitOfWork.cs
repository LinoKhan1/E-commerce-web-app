using e_commerce_app.Server.Infrastructure.Data;
using e_commerce_app.Server.Infrastructure.Repositories;
using e_commerce_app.Server.Infrastructure.Repositories.Interfaces;

namespace e_commerce_app.Server.Infrastructure.unitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;

        private readonly ILogger<ProductRepository> _logger;
        private readonly ILogger<CategoryRepository> _loggerCategory;

        public UnitOfWork(ApplicationDbContext context, ILogger<ProductRepository> logger, ILogger<CategoryRepository> loggerCategory)
        {
            _context = context;
            _logger = logger;
            _loggerCategory = loggerCategory;
        }

        public IProductRepository ProductRepository
        {
            get
            {

                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_context, _logger);
                }
                return _productRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_context, _loggerCategory);
                }
                return _categoryRepository;
            }
        }


        public Task<int> CompleteAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
