using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Visitors.Queries.FindVisitorById
{
    public class FindVisitorByIdQueryHandler : IRequestHandler<FindVisitorByIdQuery, VisitorDTO>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public FindVisitorByIdQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VisitorDTO> Handle(FindVisitorByIdQuery request, CancellationToken cancellationToken)
        {
            var visitor = await _unitOfWork
                .GetRepository<IVisitorRepository>()
                .FindById(request.Id);

            return _mapper.Map<VisitorDTO>(visitor);
        }
    }
}
