using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Cuisines.Queries.GetAllCuisines
{
    public class GetAllCuisinesQueryHandler : IRequestHandler<GetAllCuisinesQuery, CuisineDTO[]>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCuisinesQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CuisineDTO[]> Handle(GetAllCuisinesQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
               .GetRepository<ICuisineRepository>()
               .GetAll();

            return result
                .Select(x => _mapper.Map<CuisineDTO>(x))
                .ToArray();
        }
    }
}
