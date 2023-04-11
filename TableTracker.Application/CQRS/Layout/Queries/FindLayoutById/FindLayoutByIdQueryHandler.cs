using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Layout.Queries.FindLayoutById
{
    public class FindLayoutByIdQueryHandler : IRequestHandler<FindLayoutByIdQuery, LayoutDTO>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public FindLayoutByIdQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LayoutDTO> Handle(FindLayoutByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
                .GetRepository<ILayoutRepository>()
                .FindById(request.Id);

            return _mapper.Map<LayoutDTO>(result);
        }
    }
}
