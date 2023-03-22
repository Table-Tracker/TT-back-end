using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Infrastructure.Repositories
{
    public class ImageRepository : Repository<Image, long>, IImageRepository
    {
        public ImageRepository(TableDbContext context)
            : base(context)
        {
        }
    }
}
