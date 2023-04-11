using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Restaurants.Commands.AddRestaurant
{
    public class AddRestaurantCommandHandler : IRequestHandler<AddRestaurantCommand, CommandResponse<RestaurantDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public AddRestaurantCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<RestaurantDTO>> Handle(AddRestaurantCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Restaurant>(request.Restaurant);

            if (entity.Id != 0)
            {
                return new CommandResponse<RestaurantDTO>(
                    request.Restaurant,
                    CommandResult.Failure,
                    "The restaurant is already in the database.");
            }

            entity.Manager = null;
            entity.Franchise = null;
            entity.Layout = null;

            await _unitOfWork.GetRepository<IRestaurantRepository>().Insert(entity);
            await _unitOfWork.Save();

            return new CommandResponse<RestaurantDTO>(request.Restaurant);
        }
    }
}
