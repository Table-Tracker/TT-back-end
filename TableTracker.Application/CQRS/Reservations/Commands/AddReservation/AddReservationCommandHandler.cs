using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Reservations.Commands.AddReservation
{
    public class AddReservationCommandHandler : IRequestHandler<AddReservationCommand, CommandResponse<ReservationDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public AddReservationCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<ReservationDTO>> Handle(AddReservationCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Reservation>(request.Reservation);

            if (entity.Id != 0)
            {
                return new CommandResponse<ReservationDTO>(
                    request.Reservation,
                    CommandResult.Failure,
                    "The reservation is already in the database.");
            }

            entity.Table = null;
            entity.Visitor = null;

            await _unitOfWork.GetRepository<IReservationRepository>().Insert(entity);
            await _unitOfWork.Save();

            return new CommandResponse<ReservationDTO>(request.Reservation);
        }
    }
}
