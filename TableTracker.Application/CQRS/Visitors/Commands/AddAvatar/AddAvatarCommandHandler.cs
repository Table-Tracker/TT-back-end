using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Visitors.Commands.AddAvatar
{
    public class AddAvatarCommandHandler : IRequestHandler<AddAvatarCommand, CommandResponse<ImageDTO>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public AddAvatarCommandHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandResponse<ImageDTO>> Handle(AddAvatarCommand request, CancellationToken cancellationToken)
        {
            var visitor = await _unitOfWork
                .GetRepository<IVisitorRepository>()
                .FindById(request.VisitorId);

            var imageRepository = _unitOfWork.GetRepository<IImageRepository>();

            if (visitor.Avatar is not null)
            {
                imageRepository.Remove(visitor.Avatar);
            }

            var image = new Image { Name = request.FileName };
            await imageRepository.Insert(image);

            visitor.Avatar = image;
            await _unitOfWork.Save();

            return new CommandResponse<ImageDTO>(_mapper.Map<ImageDTO>(image));
        }
    }
}
