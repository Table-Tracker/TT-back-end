using System.Reflection;
using System.Threading.Tasks;

namespace TableTracker.Domain.Interfaces
{
    public interface IUnitOfWork<TId>
    {
        TRepository GetRepository<TRepository>();
        void RegisterRepositories(Assembly interfaceAssembly, Assembly implementationAssembly);

        Task Save();
    }
}
