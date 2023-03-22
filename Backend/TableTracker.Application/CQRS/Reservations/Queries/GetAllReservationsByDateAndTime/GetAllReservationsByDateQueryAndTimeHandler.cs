using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Reservations.Queries.GetAllReservationsByDateAndTime
{
    public class GetAllReservationsByDateQueryAndTimeHandler : IRequestHandler<GetAllReservationsByDateAndTimeQuery, ReservationDTO[]>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllReservationsByDateQueryAndTimeHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReservationDTO[]> Handle(GetAllReservationsByDateAndTimeQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
                .GetRepository<IReservationRepository>()
                .GetAllReservationsByDateAndTime(request.RestaurantId, request.Date);

            return result
                .Select(x => _mapper.Map<ReservationDTO>(x))
                .ToArray();
        }
    }
}
