using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Managers.Queries.GetAllManagers
{
    public class GetAllManagersQueryHandler : IRequestHandler<GetAllManagersQuery, ManagerDTO[]>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllManagersQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ManagerDTO[]> Handle(GetAllManagersQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
                .GetRepository<IManagerRepository>()
                .GetAll();

            return result
                .Select(x => _mapper.Map<ManagerDTO>(x))
                .ToArray();
        }
    }
}
