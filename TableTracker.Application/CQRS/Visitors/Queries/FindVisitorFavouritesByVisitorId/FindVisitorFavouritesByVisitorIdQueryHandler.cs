using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Visitors.Queries.FindVisitorFavouritesByVisitorId
{
    public class FindVisitorFavouritesByVisitorIdQueryHandler :
        IRequestHandler<FindVisitorFavouritesByVisitorIdQuery, ICollection<RestaurantDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public FindVisitorFavouritesByVisitorIdQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<RestaurantDTO>> Handle(
            FindVisitorFavouritesByVisitorIdQuery request,
            CancellationToken cancellationToken)
        {
            var visitorFavourite = await _unitOfWork
                .GetRepository<IVisitorRepository>()
                .FindVisitorFavouritesByVisitorId(request.VisitorId);

            return _mapper.Map<ICollection<RestaurantDTO>>(visitorFavourite);
        }
    }
}
