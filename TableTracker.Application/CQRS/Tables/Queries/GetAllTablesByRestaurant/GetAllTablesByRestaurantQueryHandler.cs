using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Tables.Queries.GetAllTablesByRestaurant
{
    public class GetAllTablesByRestaurantQueryHandler : IRequestHandler<GetAllTablesByRestaurantQuery, TableDTO[]>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllTablesByRestaurantQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TableDTO[]> Handle(GetAllTablesByRestaurantQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork
                .GetRepository<ITableRepository>()
                .GetAllTablesByRestaurant(request.RestaurantID);

            return result
                .Select(x => _mapper.Map<TableDTO>(x))
                .ToArray();
        }
    }
}
