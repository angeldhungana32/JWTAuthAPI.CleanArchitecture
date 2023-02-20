using JWTAuthAPI.Application.Core.Interfaces;
using JWTAuthAPI.Application.Infrastructure.Data;

namespace JWTAuthAPI.Application.Infrastructure.Repositories
{
    public class RepositoryActivator : IRepositoryActivator
    {
        private readonly ApplicationDbContext _dbContext;

        private Dictionary<dynamic, dynamic> _repositories;
        public RepositoryActivator(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Dictionary<dynamic, dynamic>();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public IRepository<T> Repository<T>() where T : class
        {
            _repositories ??= new Dictionary<dynamic, dynamic>();

            var type = typeof(T).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repoType = typeof(Repository<>);
                var repoInstance = Activator.CreateInstance(repoType.MakeGenericType(typeof(T)), _dbContext);
                _repositories.Add(type, repoInstance);
            }

            return (IRepository<T>)_repositories[type];
        }
    }
}
