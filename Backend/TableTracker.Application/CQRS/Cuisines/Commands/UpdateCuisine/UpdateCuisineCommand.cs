using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Cuisines.Commands.UpdateCuisine
{
    public class UpdateCuisineCommand : IRequest<CommandResponse<CuisineDTO>>
    {
        public UpdateCuisineCommand(CuisineDTO cuisine)
        {
            Cuisine = cuisine;
        }

        public CuisineDTO Cuisine { get; set; }
    }
}
