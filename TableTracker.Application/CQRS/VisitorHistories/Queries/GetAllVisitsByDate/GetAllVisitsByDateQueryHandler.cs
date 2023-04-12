using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.VisitorHistories.Queries.GetAllVisitsByDate
{
    public class GetAllVisitsByDateQueryHandler : IRequestHandler<GetAllVisitsByDateQuery, VisitorHistoryDTO[]>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllVisitsByDateQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VisitorHistoryDTO[]> Handle(GetAllVisitsByDateQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
                .GetRepository<IVisitorHistoryRepository>()
                .GetAllVisitsByDate(request.Date);

            return result
                .Select(x => _mapper.Map<VisitorHistoryDTO>(x))
                .ToArray();
        }
    }
}
