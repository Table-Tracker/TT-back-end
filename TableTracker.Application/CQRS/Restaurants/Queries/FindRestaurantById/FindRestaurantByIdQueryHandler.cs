using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Restaurants.Queries.FindRestaurantById
{
    public class FindRestaurantByIdQueryHandler : IRequestHandler<FindRestaurantByIdQuery, RestaurantDTO>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public FindRestaurantByIdQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RestaurantDTO> Handle(FindRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
                .GetRepository<IRestaurantRepository>()
                .FindById(request.Id);

            return _mapper.Map<RestaurantDTO>(result);
        }
    }
}
