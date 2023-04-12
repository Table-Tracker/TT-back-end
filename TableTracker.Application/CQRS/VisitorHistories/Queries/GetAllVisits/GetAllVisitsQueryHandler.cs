using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.VisitorHistories.Queries.GetAllVisits
{
    public class GetAllVisitsQueryHandler : IRequestHandler<GetAllVisitsQuery, VisitorHistoryDTO[]>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllVisitsQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<VisitorHistoryDTO[]> Handle(GetAllVisitsQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
                   .GetRepository<IVisitorHistoryRepository>()
                   .GetAll();

            return result
                .Select(x => _mapper.Map<VisitorHistoryDTO>(x))
                .ToArray();
        }
    }
}
