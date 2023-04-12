using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Cuisines.Commands.DeleteCuisine
{
    public class DeleteCuisineCommandHandler : IRequestHandler<DeleteCuisineCommand, CommandResponse<CuisineDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCuisineCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<CuisineDTO>> Handle(DeleteCuisineCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<ICuisineRepository>();
            var entity = await repository.FindById(request.Id);

            if (await repository.Contains(entity))
            {
                repository.Remove(entity);
                await _unitOfWork.Save();

                return new CommandResponse<CuisineDTO>(_mapper.Map<CuisineDTO>(entity));
            }

            return new CommandResponse<CuisineDTO>(
                _mapper.Map<CuisineDTO>(entity),
                Domain.Enums.CommandResult.NotFound,
                "Could not find the given cuisine.");
        }
    }
}
