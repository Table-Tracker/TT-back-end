using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.RestaurantVisitors.Commands.UpdateRestaurantVisitor
{
    public class UpdateRestaurantVisitorCommandHandler : IRequestHandler<UpdateRestaurantVisitorCommand, CommandResponse<RestaurantVisitorDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRestaurantVisitorCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<RestaurantVisitorDTO>> Handle(UpdateRestaurantVisitorCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<IRestaurantVisitorRepository>();
            var entity = _mapper.Map<RestaurantVisitor>(request.RestaurantVisitor);

            if (await repository.Contains(entity))
            {
                repository.Update(entity);
                await _unitOfWork.Save();

                return new CommandResponse<RestaurantVisitorDTO>(request.RestaurantVisitor);
            }

            return new CommandResponse<RestaurantVisitorDTO>(
                request.RestaurantVisitor,
                CommandResult.NotFound,
                "Could not find the given restaurant visitor.");
        }
    }
}
