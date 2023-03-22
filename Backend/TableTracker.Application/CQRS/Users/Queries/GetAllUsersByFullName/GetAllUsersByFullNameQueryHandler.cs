using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Users.Queries.GetAllUsersByFullName
{
    public class GetAllUsersByFullNameQueryHandler : IRequestHandler<GetAllUsersByFullNameQuery, UserDTO[]>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllUsersByFullNameQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDTO[]> Handle(GetAllUsersByFullNameQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork
                .GetRepository<IUserRepository>()
                .GetAllUsersByFullName(request.FullName);

            return users
                .Select(x => _mapper.Map<UserDTO>(x))
                .ToArray();
        }
    }
}
