using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, CommandResponse<RestaurantDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRestaurantCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<RestaurantDTO>> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<IRestaurantRepository>();
            var entity = _mapper.Map<Restaurant>(request.Restaurant);

            if (await repository.Contains(entity))
            {
                entity.Manager = null;
                entity.Franchise = null;
                entity.Layout = null;

                repository.Update(entity);
                await _unitOfWork.Save();

                return new CommandResponse<RestaurantDTO>(request.Restaurant);
            }

            return new CommandResponse<RestaurantDTO>(
                request.Restaurant,
                Domain.Enums.CommandResult.NotFound,
                "Could not find the given restaurant.");
        }
    }
}
