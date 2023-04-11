using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Visitors.Commands.DeleteAvatar
{
    public class DeleteAvatarCommandHandler : IRequestHandler<DeleteAvatarCommand, CommandResponse<string>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;

        public DeleteAvatarCommandHandler(IUnitOfWork<long> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResponse<string>> Handle(DeleteAvatarCommand request, CancellationToken cancellationToken)
        {
            var visitor = await _unitOfWork
                .GetRepository<IVisitorRepository>()
                .FindById(request.VisitorId);

            string fileName = "";

            if (visitor.Avatar is not null)
            {
                fileName = visitor.Avatar.Name;

                _unitOfWork.GetRepository<IImageRepository>().Remove(visitor.Avatar);

                await _unitOfWork.Save();
            }

            return new CommandResponse<string>(fileName);
        }
    }
}
