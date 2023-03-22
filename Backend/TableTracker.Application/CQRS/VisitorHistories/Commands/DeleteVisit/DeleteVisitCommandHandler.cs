using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.VisitorHistories.Commands.DeleteVisit
{
    public class DeleteVisitCommandHandler : IRequestHandler<DeleteVisitCommand, CommandResponse<VisitorHistoryDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteVisitCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<VisitorHistoryDTO>> Handle(DeleteVisitCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<IVisitorHistoryRepository>();
            var entity = await repository.FindById(request.Id);

            if (await repository.Contains(entity))
            {
                repository.Remove(entity);
                await _unitOfWork.Save();

                return new CommandResponse<VisitorHistoryDTO>(_mapper.Map<VisitorHistoryDTO>(entity));
            }

            return new CommandResponse<VisitorHistoryDTO>(
                _mapper.Map<VisitorHistoryDTO>(entity),
                CommandResult.NotFound,
                "Could not find the given visit.");
        }
    }
}
