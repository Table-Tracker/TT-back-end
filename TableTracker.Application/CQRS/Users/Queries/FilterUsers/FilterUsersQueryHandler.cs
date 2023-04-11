using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Users.Queries.FilterUsers
{
    public class FilterUsersQueryHandler : IRequestHandler<FilterUsersQuery, UserDTO[]>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public FilterUsersQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDTO[]> Handle(FilterUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork
                .GetRepository<IUserRepository>()
                .FilterUsers(request.Filter);

            return users
                .Select(x => _mapper.Map<UserDTO>(x))
                .ToArray();
        }
    }
}
