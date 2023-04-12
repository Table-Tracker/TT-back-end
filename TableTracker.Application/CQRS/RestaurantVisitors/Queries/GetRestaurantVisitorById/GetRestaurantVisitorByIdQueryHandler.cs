using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.RestaurantVisitors.Queries.GetRestaurantVisitorById
{
    public class GetRestaurantVisitorByIdQueryHandler : IRequestHandler<GetRestaurantVisitorByIdQuery, RestaurantVisitorDTO>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetRestaurantVisitorByIdQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<RestaurantVisitorDTO> Handle(
            GetRestaurantVisitorByIdQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
                .GetRepository<IRestaurantVisitorRepository>()
                .FindById(request.Id);

            return _mapper.Map<RestaurantVisitorDTO>(result);
        }
    }
}
