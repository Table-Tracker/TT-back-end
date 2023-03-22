using System.Threading;
using System.Threading.Tasks;

using MediatR;

using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Visitors.Commands.DeleteVisitorFavourite
{
    public class DeleteVisitorFavouriteCommandHandler : IRequestHandler<DeleteVisitorFavouriteCommand, CommandResponse>
    {
        private readonly IUnitOfWork<long> _unitOfWork;

        public DeleteVisitorFavouriteCommandHandler(IUnitOfWork<long> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResponse> Handle(DeleteVisitorFavouriteCommand request, CancellationToken cancellationToken)
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

            if (visitor.Favourites.Remove(restaurant))
            {
                await _unitOfWork.Save();

                return new CommandResponse();
            }

            return new CommandResponse(CommandResult.Failure, "Could not delete favourite");
        }
    }
}
