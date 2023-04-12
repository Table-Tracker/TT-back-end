using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Cuisines.Commands.AddCuisine
{
    public class AddCuisineCommandHandler : IRequestHandler<AddCuisineCommand, CommandResponse<CuisineDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public AddCuisineCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<CuisineDTO>> Handle(AddCuisineCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Cuisine>(request.Cuisine);

            if (entity.Id != 0)
            {
                return new CommandResponse<CuisineDTO>(
                    request.Cuisine,
                    CommandResult.Failure,
                    "The Cuisine is already in the database");
            }

            await _unitOfWork
                .GetRepository<ICuisineRepository>()
                .Insert(entity);

            await _unitOfWork.Save();

            return new CommandResponse<CuisineDTO>(_mapper.Map<CuisineDTO>(entity));
        }
    }
}
