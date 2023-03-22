using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Tables.Commands.AddTable
{
    public class AddTableCommandHandler : IRequestHandler<AddTableCommand, CommandResponse<TableDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public AddTableCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<TableDTO>> Handle(AddTableCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Table>(request.Table);

            if (entity.Id != 0)
            {
                return new CommandResponse<TableDTO>(
                    request.Table,
                    CommandResult.Failure,
                    "The table is already in the database.");
            }

            await _unitOfWork.GetRepository<ITableRepository>().Insert(entity);
            await _unitOfWork.Save();

            return new CommandResponse<TableDTO>(request.Table);
        }
    }
}
