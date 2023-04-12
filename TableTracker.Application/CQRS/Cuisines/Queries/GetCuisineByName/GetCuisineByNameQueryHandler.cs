using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Cuisines.Queries.GetCuisineByName
{
    public class GetCuisineByNameQueryHandler : IRequestHandler<GetCuisineByNameQuery, CuisineDTO>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetCuisineByNameQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CuisineDTO> Handle(GetCuisineByNameQuery request, CancellationToken cancellationToken)
        {
            var cuisine = await _unitOfWork
                .GetRepository<ICuisineRepository>()
                .GetCuisineByName(request.Name);

            return _mapper.Map<CuisineDTO>(cuisine);
        }
    }
}
