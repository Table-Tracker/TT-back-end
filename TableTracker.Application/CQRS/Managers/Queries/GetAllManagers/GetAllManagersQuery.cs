using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Managers.Queries.GetAllManagers
{
    public class GetAllManagersQuery : IRequest<ManagerDTO[]>
    {
    }
}
