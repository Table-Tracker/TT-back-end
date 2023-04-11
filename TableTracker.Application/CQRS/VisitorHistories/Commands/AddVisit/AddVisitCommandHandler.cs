using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.VisitorHistories.Commands.AddVisit
{
    public class AddVisitCommandHandler : IRequestHandler<AddVisitCommand, CommandResponse<VisitorHistoryDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public AddVisitCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<VisitorHistoryDTO>> Handle(AddVisitCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<VisitorHistory>(request.Visit);

            if (entity.Id != 0)
            {
                return new CommandResponse<VisitorHistoryDTO>(
                    request.Visit,
                    CommandResult.Failure,
                    "The visit is already in the database.");
            }

            await _unitOfWork.GetRepository<IVisitorHistoryRepository>().Insert(entity);
            await _unitOfWork.Save();

            return new CommandResponse<VisitorHistoryDTO>(request.Visit);
        }
    }
}
