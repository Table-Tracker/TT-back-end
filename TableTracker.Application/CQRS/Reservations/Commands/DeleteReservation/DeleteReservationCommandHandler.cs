using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Reservations.Commands.DeleteReservation
{
    public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand, CommandResponse<ReservationDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteReservationCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<ReservationDTO>> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<IReservationRepository>();
            var entity = await repository.FindById(request.Id);

            if (await repository.Contains(entity))
            {
                repository.Remove(entity);
                await _unitOfWork.Save();

                return new CommandResponse<ReservationDTO>(_mapper.Map<ReservationDTO>(entity));
            }

            return new CommandResponse<ReservationDTO>(
                _mapper.Map<ReservationDTO>(entity),
                Domain.Enums.CommandResult.NotFound,
                "Could not find the given reservation.");
        }
    }
}
