using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.VisitorHistories.Commands.UpdateVisit
{
    public class UpdateVisitCommandHandler : IRequestHandler<UpdateVisitCommand, CommandResponse<VisitorHistoryDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateVisitCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<VisitorHistoryDTO>> Handle(UpdateVisitCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<IVisitorHistoryRepository>();
            var entity = _mapper.Map<VisitorHistory>(request.Visit);

            if (await repository.Contains(entity))
            {
                repository.Update(entity);
                await _unitOfWork.Save();

                return new CommandResponse<VisitorHistoryDTO>(request.Visit);
            }

            return new CommandResponse<VisitorHistoryDTO>(
                request.Visit,
                CommandResult.NotFound,
                "Could not find the given visit.");
        }
    }
}
