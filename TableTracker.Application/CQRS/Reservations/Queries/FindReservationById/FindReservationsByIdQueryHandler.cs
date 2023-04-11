using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Reservations.Queries.FindReservationById
{
    public class FindReservationsByIdQueryHandler : IRequestHandler<FindReservationByIdQuery, ReservationDTO>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public FindReservationsByIdQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReservationDTO> Handle(FindReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
                .GetRepository<IReservationRepository>()
                .FindById(request.Id);

            return _mapper.Map<ReservationDTO>(result);
        }
    }
}
