using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Tables.Commands.DeleteTable
{
    public class DeleteTableCommandHandler : IRequestHandler<DeleteTableCommand, CommandResponse<TableDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteTableCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<TableDTO>> Handle(DeleteTableCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<ITableRepository>();
            var entity = await repository.FindById(request.Id);

            if (await repository.Contains(entity))
            {
                repository.Remove(entity);
                await _unitOfWork.Save();

                return new CommandResponse<TableDTO>(_mapper.Map<TableDTO>(entity));
            }

            return new CommandResponse<TableDTO>(
                _mapper.Map<TableDTO>(entity),
                Domain.Enums.CommandResult.NotFound,
                "Could not find the given table.");
        }
    }
}
