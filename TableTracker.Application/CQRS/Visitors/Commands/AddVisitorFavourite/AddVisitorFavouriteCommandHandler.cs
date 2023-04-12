using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Visitors.Commands.AddVisitorFavourite
{
    public class AddVisitorFavouriteCommandHandler : IRequestHandler<AddVisitorFavouriteCommand, CommandResponse>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public AddVisitorFavouriteCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse> Handle(
            AddVisitorFavouriteCommand request,
            CancellationToken cancellationToken)
        {
            var visitor = await _unitOfWork
                .GetRepository<IVisitorRepository>()
                .FindById(request.VisitorId);

            if (visitor is null)
            {
                return new CommandResponse(CommandResult.NotFound, "Visitor could not be found");
            }

            var restaurant = await _unitOfWork
                .GetRepository<IRestaurantRepository>()
                .FindById(request.RestaurantId);

            if (restaurant is null)
            {
                return new CommandResponse(CommandResult.NotFound, "Restaurant could not be found");
            }

            if (visitor.Favourites.Any(x => x.Id == request.RestaurantId))
            {
                return new CommandResponse(
                    CommandResult.Failure,
                    "The visitor favourite is already in the database");
            }

            visitor.Favourites.Add(restaurant);
            await _unitOfWork.Save();

            return new CommandResponse();
        }
    }
}
