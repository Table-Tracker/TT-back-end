using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Cuisines.Commands.DeleteCuisine
{
    public class DeleteCuisineCommand : IRequest<CommandResponse<CuisineDTO>>
    {
        public DeleteCuisineCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
