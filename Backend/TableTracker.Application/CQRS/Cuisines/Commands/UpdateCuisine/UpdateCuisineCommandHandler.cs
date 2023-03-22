using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Cuisines.Commands.UpdateCuisine
{
    public class UpdateCuisineCommandHandler : IRequestHandler<UpdateCuisineCommand, CommandResponse<CuisineDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCuisineCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<CuisineDTO>> Handle(UpdateCuisineCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<ICuisineRepository>();
            var entity = _mapper.Map<Cuisine>(request.Cuisine);

            if (await repository.Contains(entity))
            {
                repository.Update(entity);
                await _unitOfWork.Save();

                return new CommandResponse<CuisineDTO>(request.Cuisine);
            }

            return new CommandResponse<CuisineDTO>(
                request.Cuisine,
                Domain.Enums.CommandResult.NotFound,
                "Could not find the given cuisine.");
        }
    }
}
