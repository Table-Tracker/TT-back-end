using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Visitors.Commands.AddVisitor
{
    internal class AddVisitorCommandHandler : IRequestHandler<AddVisitorCommand, CommandResponse<VisitorDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public AddVisitorCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<VisitorDTO>> Handle(AddVisitorCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Visitor>(request.Visitor);

            if (entity.Id != 0)
            {
                return new CommandResponse<VisitorDTO>(
                    request.Visitor,
                    CommandResult.Failure,
                    "The visitor is already in the database");
            }

            await _unitOfWork
                .GetRepository<IVisitorRepository>()
                .Insert(entity);

            await _unitOfWork.Save();

            return new CommandResponse<VisitorDTO>(_mapper.Map<VisitorDTO>(entity));
        }
    }
}
