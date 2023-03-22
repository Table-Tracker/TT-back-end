using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Users.Queries.FindUserById
{
    public class FindUserByIdQueryHandler : IRequestHandler<FindUserByIdQuery, UserDTO>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public FindUserByIdQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(FindUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork
                .GetRepository<IUserRepository>()
                .FindById(request.Id);

            return _mapper.Map<UserDTO>(user);
        }
    }
}
