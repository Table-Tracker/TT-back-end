using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.RestaurantVisitors.Queries.GetAllVisitorsByAverageMoneySpent
{
    public class GetAllVisitorsByAverageMoneySpentQueryHandler : IRequestHandler<GetAllVisitorsByAverageMoneySpentQuery, RestaurantVisitorDTO[]>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllVisitorsByAverageMoneySpentQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<RestaurantVisitorDTO[]> Handle(GetAllVisitorsByAverageMoneySpentQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
                .GetRepository<IRestaurantVisitorRepository>()
                .GetAllVisitorsByAverageMoneySpent(
                    request.AverageMoneySpent,
                    _mapper.Map<Restaurant>(request.Restaurant));

            return result
                .Select(x => _mapper.Map<RestaurantVisitorDTO>(x))
                .ToArray();
        }
    }
}
