using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Visitors.Queries.FilterVisitors
{
    public class FilterVisitorsQueryHandler : IRequestHandler<FilterVisitorsQuery, VisitorDTO[]>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public FilterVisitorsQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VisitorDTO[]> Handle(FilterVisitorsQuery request, CancellationToken cancellationToken)
        {
            var visitors = await _unitOfWork
                .GetRepository<IVisitorRepository>()
                .FilterVisitors(request.Filter);

            return visitors
                .Select(x => _mapper.Map<VisitorDTO>(x))
                .ToArray();
        }
    }
}
