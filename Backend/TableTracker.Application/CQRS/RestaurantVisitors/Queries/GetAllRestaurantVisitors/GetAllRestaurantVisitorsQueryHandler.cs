using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.RestaurantVisitors.Queries.GetAllRestaurantVisitors
{
    public class GetAllRestaurantVisitorsQueryHandler : IRequestHandler<GetAllRestaurantVisitorsQuery, RestaurantVisitorDTO[]>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllRestaurantVisitorsQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<RestaurantVisitorDTO[]> Handle(GetAllRestaurantVisitorsQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
                .GetRepository<IRestaurantVisitorRepository>()
                .GetAll();

            return result
                .Select(x => _mapper.Map<RestaurantVisitorDTO>(x))
                .ToArray();
        }
    }
}
