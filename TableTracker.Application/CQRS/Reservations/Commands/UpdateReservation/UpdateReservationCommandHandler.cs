using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Reservations.Commands.UpdateReservation
{
    public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, CommandResponse<ReservationDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateReservationCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<ReservationDTO>> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<IReservationRepository>();
            var entity = _mapper.Map<Reservation>(request.Reservation);

            if (await repository.Contains(entity))
            {
                entity.Table = null;
                entity.Visitor = null;

                repository.Update(entity);
                await _unitOfWork.Save();

                return new CommandResponse<ReservationDTO>(request.Reservation);
            }

            return new CommandResponse<ReservationDTO>(
                request.Reservation,
                Domain.Enums.CommandResult.NotFound,
                "Could not find the given reservation.");
        }
    }
}
