using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.VisitorHistories.Queries.FindVisitById
{
    public class FindVisitByIdQueryHandler : IRequestHandler<FindVisitByIdQuery, VisitorHistoryDTO>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public FindVisitByIdQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VisitorHistoryDTO> Handle(FindVisitByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
                 .GetRepository<IVisitorHistoryRepository>()
                 .FindById(request.Id);

            return _mapper.Map<VisitorHistoryDTO>(result);
        }
    }
}
