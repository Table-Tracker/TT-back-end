using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Visitors.Queries.GetAllVisitorsByTrustFactor
{
    public class GetAllVisitorsByTrustFactorQueryHandler : IRequestHandler<GetAllVisitorsByTrustFactorQuery, VisitorDTO[]>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllVisitorsByTrustFactorQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VisitorDTO[]> Handle(GetAllVisitorsByTrustFactorQuery request, CancellationToken cancellationToken)
        {
            var visitors = await _unitOfWork
                .GetRepository<IVisitorRepository>()
                .GetAllVisitorsByTrustFactor(request.TrustFactor);

            return visitors
                .Select(x => _mapper.Map<VisitorDTO>(x))
                .ToArray();
        }
    }
}
