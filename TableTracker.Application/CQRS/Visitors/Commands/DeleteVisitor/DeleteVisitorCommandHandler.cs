using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Visitors.Commands.DeleteVisitor
{
    public class DeleteVisitorCommandHandler : IRequestHandler<DeleteVisitorCommand, CommandResponse<VisitorDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteVisitorCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<VisitorDTO>> Handle(DeleteVisitorCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<IVisitorRepository>();
            var entity = await repository.FindById(request.Id);

            if (await repository.Contains(entity))
            {
                repository.Remove(entity);
                await _unitOfWork.Save();

                return new CommandResponse<VisitorDTO>(_mapper.Map<VisitorDTO>(entity));
            }

            return new CommandResponse<VisitorDTO>(
                _mapper.Map<VisitorDTO>(entity),
                CommandResult.NotFound,
                "Could not find the given visitor.");
        }
    }
}
