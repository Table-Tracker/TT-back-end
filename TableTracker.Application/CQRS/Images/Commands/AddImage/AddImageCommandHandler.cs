using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Images.Commands.AddImage
{
    public class AddImageCommandHandler : IRequestHandler<AddImageCommand, CommandResponse<ImageDTO>>
    {
        public Task<CommandResponse<ImageDTO>> Handle(AddImageCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
