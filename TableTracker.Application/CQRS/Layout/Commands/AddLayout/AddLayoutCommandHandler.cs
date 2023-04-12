using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Layout.Commands.AddLayout
{
    public class AddLayoutCommandHandler : IRequestHandler<AddLayoutCommand, CommandResponse<LayoutDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public AddLayoutCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<LayoutDTO>> Handle(AddLayoutCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Layout>(request.LayoutDTO);

            if(entity.Id != 0)
            {
                return new CommandResponse<LayoutDTO>(
                    request.LayoutDTO,
                    Domain.Enums.CommandResult.Failure,
                    "The Layout is already in the database");
            }

            entity.Restaurant = null;

            await _unitOfWork.GetRepository<ILayoutRepository>().Insert(entity);
            await _unitOfWork.Save();

            return new CommandResponse<LayoutDTO>(request.LayoutDTO);
        }
    }
}
