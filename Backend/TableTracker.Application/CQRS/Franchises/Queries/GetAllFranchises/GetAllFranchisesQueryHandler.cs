using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Franchises.Queries.GetAllFranchises
{
    public class GetAllFranchisesQueryHandler : IRequestHandler<GetAllFranchisesQuery, FranchiseDTO[]>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllFranchisesQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FranchiseDTO[]> Handle(GetAllFranchisesQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
                .GetRepository<IFranchiseRepository>()
                .GetAll();

            return result
                .Select(x => _mapper.Map<FranchiseDTO>(x))
                .ToArray();
        }
    }
}
