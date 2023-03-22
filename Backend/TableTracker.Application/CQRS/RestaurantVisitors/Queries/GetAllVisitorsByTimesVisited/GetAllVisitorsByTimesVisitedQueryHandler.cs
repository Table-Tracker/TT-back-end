using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.RestaurantVisitors.Queries.GetAllVisitorsByTimesVisited
{
    public class GetAllVisitorsByTimesVisitedQueryHandler : IRequestHandler<GetAllVisitorsByTimesVisitedQuery, RestaurantVisitorDTO[]>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllVisitorsByTimesVisitedQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<RestaurantVisitorDTO[]> Handle(GetAllVisitorsByTimesVisitedQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
                .GetRepository<IRestaurantVisitorRepository>()
                .GetAllVisitorsByTimesVisited(
                    request.TimesVisited,
                    _mapper.Map<Restaurant>(request.Restaurant));
        
            return result
                .Select(x => _mapper.Map<RestaurantVisitorDTO>(x))
                .ToArray();
        }
    }
}
