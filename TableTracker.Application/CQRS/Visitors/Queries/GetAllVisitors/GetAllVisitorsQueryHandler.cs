using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Visitors.Queries.GetAllVisitors
{
    public class GetAllVisitorsQueryHandler : IRequestHandler<GetAllVisitorsQuery, VisitorDTO[]>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllVisitorsQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VisitorDTO[]> Handle(GetAllVisitorsQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
                .GetRepository<IVisitorRepository>()
                .GetAll();

            return result
                .Select(x => _mapper.Map<VisitorDTO>(x))
                .ToArray();
        }
    }
}
