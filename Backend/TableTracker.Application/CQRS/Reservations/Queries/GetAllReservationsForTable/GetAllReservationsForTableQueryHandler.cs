using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Reservations.Queries.GetAllReservationsForTable
{
    public class GetAllReservationsForTableQueryHandler : IRequestHandler<GetAllReservationsForTableQuery, ReservationDTO[]>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllReservationsForTableQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReservationDTO[]> Handle(GetAllReservationsForTableQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
                .GetRepository<IReservationRepository>()
                .GetAllReservationsForTable(tableId: request.Id, date: request.Date);

            return result
                .Select(x => _mapper.Map<ReservationDTO>(x))
                .ToArray();
        }
    }
}
