using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand, CommandResponse<RestaurantDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteRestaurantCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<RestaurantDTO>> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<IRestaurantRepository>();
            var entity = await repository.FindById(request.RestaurantId);

            if (await repository.Contains(entity))
            {
                repository.Remove(entity);
                await _unitOfWork.Save();

                return new CommandResponse<RestaurantDTO>(_mapper.Map<RestaurantDTO>(entity));
            }

            return new CommandResponse<RestaurantDTO>(
                _mapper.Map<RestaurantDTO>(entity),
                CommandResult.NotFound,
                "Could not find the given restaurant.");
        }
    }
}
