using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Visitors.Commands.UpdateVisitor
{
    public class UpdateVisitorCommandHandler : IRequestHandler<UpdateVisitorCommand, CommandResponse<VisitorDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateVisitorCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<VisitorDTO>> Handle(UpdateVisitorCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<IVisitorRepository>();
            var entity = _mapper.Map<Visitor>(request.Visitor);

            if (await repository.Contains(entity))
            {
                repository.Update(entity);
                await _unitOfWork.Save();

                return new CommandResponse<VisitorDTO>(request.Visitor);
            }

            return new CommandResponse<VisitorDTO>(
                request.Visitor,
                CommandResult.NotFound,
                "Could not find the given visitor.");
        }
    }
}
