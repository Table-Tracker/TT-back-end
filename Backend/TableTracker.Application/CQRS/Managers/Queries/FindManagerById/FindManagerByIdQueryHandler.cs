using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Managers.Queries.FindManagerById
{
    public class FindManagerByIdQueryHandler : IRequestHandler<FindManagerByIdQuery, ManagerDTO>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public FindManagerByIdQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ManagerDTO> Handle(FindManagerByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
                .GetRepository<IManagerRepository>()
                .FindById(request.Id);

            return _mapper.Map<ManagerDTO>(result);
        }
    }
}
