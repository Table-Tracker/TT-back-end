using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Franchises.Queries.GetFranchiseByName
{
    public class GetFranchiseByNameQueryHandler : IRequestHandler<GetFranchiseByNameQuery, FranchiseDTO>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetFranchiseByNameQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FranchiseDTO> Handle(GetFranchiseByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
                .GetRepository<IFranchiseRepository>()
                .GetFranchiseByName(request.Name);

            return _mapper.Map<FranchiseDTO>(result);
        }
    }
}
