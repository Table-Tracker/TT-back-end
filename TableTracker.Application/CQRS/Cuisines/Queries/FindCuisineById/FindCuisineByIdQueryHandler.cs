using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Cuisines.Queries.FindCuisineById
{
    public class FindCuisineByIdQueryHandler : IRequestHandler<FindCuisineByIdQuery, CuisineDTO>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public FindCuisineByIdQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CuisineDTO> Handle(FindCuisineByIdQuery request, CancellationToken cancellationToken)
        {
            var cuisine = await _unitOfWork
                .GetRepository<ICuisineRepository>()
                .FindById(request.Id);

            return _mapper.Map<CuisineDTO>(cuisine);
        }
    }
}
