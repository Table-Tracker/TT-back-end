using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Tables.Queries.GetAllTablesWithFiltering
{
    public class GetAllTablesWithFilteringQueryHandler : IRequestHandler<GetAllTablesWithFilteringQuery, TableDTO[]>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllTablesWithFilteringQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TableDTO[]> Handle(GetAllTablesWithFilteringQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
                .GetRepository<ITableRepository>()
                .GetAllTablesWithFiltering(
                    restaurant: _mapper.Map<Restaurant>(request.FilterModel.Restaurant),
                    numberOfSeats: request.FilterModel.NumberOfSeats,
                    tableSize: request.FilterModel.TableSize,
                    floor: request.FilterModel.Floor,
                    reserveDate: request.FilterModel.ReserveDate,
                    state: request.FilterModel.State
                );

            return result
                .Select(x => _mapper.Map<TableDTO>(x))
                .ToArray();
        }
    }
}
