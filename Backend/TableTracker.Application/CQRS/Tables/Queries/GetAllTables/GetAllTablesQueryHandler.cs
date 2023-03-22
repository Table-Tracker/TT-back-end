using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Tables.Queries.GetAllTables
{
    public class GetAllTablesQueryHandler : IRequestHandler<GetAllTablesQuery, TableDTO[]>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllTablesQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<TableDTO[]> Handle(GetAllTablesQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
                .GetRepository<ITableRepository>()
                .GetAll();

            return result
                .Select(x => _mapper.Map<TableDTO>(x))
                .ToArray();
        }
    }
}
