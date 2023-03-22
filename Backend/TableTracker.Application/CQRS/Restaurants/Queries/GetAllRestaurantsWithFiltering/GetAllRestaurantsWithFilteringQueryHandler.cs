using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Restaurants.Queries.GetAllRestaurantsWithFiltering
{
    public class GetAllRestaurantsWithFilteringQueryHandler : IRequestHandler<GetAllRestaurantsWithFilteringQuery, RestaurantDTO[]>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllRestaurantsWithFilteringQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RestaurantDTO[]> Handle(GetAllRestaurantsWithFilteringQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
                .GetRepository<IRestaurantRepository>()
                .GetAllRestaurantsWithFiltering(
                    rating: request.FilterModel.Rating,
                    cuisines: request.FilterModel.Cuisines.Select(x => _mapper.Map<Cuisine>(x)),
                    price: request.FilterModel.Price,
                    type: request.FilterModel.Type,
                    franchise: _mapper.Map<Franchise>(request.FilterModel.Franchise),
                    discount: request.FilterModel.Discount);

            return result
                .Select(x => _mapper.Map<RestaurantDTO>(x))
                .ToArray();
        }
    }
}
