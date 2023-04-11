using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Layout.Commands.DeleteLayout
{
    public class DeleteLayoutCommandHandler : IRequestHandler<DeleteLayoutCommand, CommandResponse<LayoutDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteLayoutCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<LayoutDTO>> Handle(DeleteLayoutCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<ILayoutRepository>();
            var entity = await repository.FindById(request.Id);

            if(await repository.Contains(entity))
            {
                repository.Remove(entity);
                await _unitOfWork.Save();

                return new CommandResponse<LayoutDTO>(_mapper.Map<LayoutDTO>(entity));
            }

            return new CommandResponse<LayoutDTO>(
                _mapper.Map<LayoutDTO>(entity),
                Domain.Enums.CommandResult.NotFound,
                "Could not find the given layout."
                );
        }
    }
}
