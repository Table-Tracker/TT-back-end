using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.RestaurantVisitors.Commands.AddRestaurantVisitor
{
    public class AddRestaurantVisitorCommandHandler : IRequestHandler<AddRestaurantVisitorCommand, CommandResponse<RestaurantVisitorDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public AddRestaurantVisitorCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResponse<RestaurantVisitorDTO>> Handle(
            AddRestaurantVisitorCommand request,
            CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<RestaurantVisitor>(request.RestaurantVisitor);

            if (entity.Id != 0)
            {
                return new CommandResponse<RestaurantVisitorDTO>(
                    request.RestaurantVisitor,
                    CommandResult.Failure,
                    "The restaurant visitor is already in the database.");
            }

            await _unitOfWork.GetRepository<IRestaurantVisitorRepository>().Insert(entity);
            await _unitOfWork.Save();

            return new CommandResponse<RestaurantVisitorDTO>(request.RestaurantVisitor);
        }
    }
}
