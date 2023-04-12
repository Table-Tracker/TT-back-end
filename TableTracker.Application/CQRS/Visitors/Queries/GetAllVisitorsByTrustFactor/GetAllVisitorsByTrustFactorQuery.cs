using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Visitors.Queries.GetAllVisitorsByTrustFactor
{
    public class GetAllVisitorsByTrustFactorQuery : IRequest<VisitorDTO[]>
    {
        public GetAllVisitorsByTrustFactorQuery(float trustFactor)
        {
            TrustFactor = trustFactor;
        }

        public float TrustFactor { get; set; }
    }
}
