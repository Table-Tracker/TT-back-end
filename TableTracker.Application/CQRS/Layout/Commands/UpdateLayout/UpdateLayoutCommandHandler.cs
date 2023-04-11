using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Layout.Commands.UpdateLayout
{
    public class UpdateLayoutCommandHandler : IRequestHandler<UpdateLayoutCommand, CommandResponse<LayoutDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLayoutCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<LayoutDTO>> Handle(UpdateLayoutCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<ILayoutRepository>();
            var entity = _mapper.Map<Domain.Entities.Layout>(request.LayoutDTO);

            if(await repository.Contains(entity))
            {
                entity.Restaurant = null;
                repository.Update(entity);
                await _unitOfWork.Save();

                return new CommandResponse<LayoutDTO>(request.LayoutDTO);
            }

            return new CommandResponse<LayoutDTO>(
                request.LayoutDTO,
                Domain.Enums.CommandResult.NotFound,
                "Could not find the given layout."
                );
        }
    }
}
