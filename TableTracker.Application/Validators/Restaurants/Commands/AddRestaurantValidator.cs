using FluentValidation;

using TableTracker.Application.CQRS.Restaurants.Queries.FindRestaurantById;

namespace TableTracker.Application.Validators.Restaurants.Commands
{
    public class FindRestaurantByIdValidator : AbstractValidator<FindRestaurantByIdQuery>
    {
        public FindRestaurantByIdValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
