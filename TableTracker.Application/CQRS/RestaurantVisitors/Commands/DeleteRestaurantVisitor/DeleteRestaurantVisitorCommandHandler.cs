using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.RestaurantVisitors.Commands.DeleteRestaurantVisitor
{
    public class DeleteRestaurantVisitorCommandHandler : IRequestHandler<DeleteRestaurantVisitorCommand, CommandResponse<RestaurantVisitorDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteRestaurantVisitorCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResponse<RestaurantVisitorDTO>> Handle(DeleteRestaurantVisitorCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<IRestaurantVisitorRepository>();
            var entity = await repository.FindById(request.Id);

            if (await repository.Contains(entity))
            {
                repository.Remove(entity);
                await _unitOfWork.Save();

                return new CommandResponse<RestaurantVisitorDTO>(_mapper.Map<RestaurantVisitorDTO>(entity));
            }

            return new CommandResponse<RestaurantVisitorDTO>(
                _mapper.Map<RestaurantVisitorDTO>(entity),
                Domain.Enums.CommandResult.NotFound,
                "Could not find the given restaurant visitor.");
        }
    }
}
