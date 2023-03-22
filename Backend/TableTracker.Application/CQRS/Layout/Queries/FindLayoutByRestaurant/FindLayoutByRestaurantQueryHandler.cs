using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Layout.Queries.FindLayoutByRestaurant
{
    public class FindLayoutByRestaurantQueryHandler : IRequestHandler<FindLayoutByRestaurantQuery, LayoutDTO>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public FindLayoutByRestaurantQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LayoutDTO> Handle(FindLayoutByRestaurantQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
                .GetRepository<ILayoutRepository>()
                .FindLayoutByRestaurant(request.RestaurantDTO);

            return _mapper.Map<LayoutDTO>(result);
        }
    }
}
