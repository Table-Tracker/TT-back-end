using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableTracker.Domain.Interfaces.Repositories;
using TableTracker.Domain.Interfaces;
using Xunit;
using AutoMapper;
using System.Threading;
using TableTracker.Application.CQRS.Franchises.Commands.AddFranchise;
using TableTracker.Application.CQRS.Franchises.Commands.DeleteFranchise;
using TableTracker.Application.CQRS.Franchises.Commands.UpdateFranchise;
using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;
using TableTracker.Infrastructure.Repositories;
using TableTracker.Application.CQRS.Layout.Commands.AddLayout;
using TableTracker.Application.CQRS.Layout.Commands.DeleteLayout;
using TableTracker.Application.CQRS.Layout.Commands.UpdateLayout;

namespace TableTracker.Tests.LayoutTests
{
    public class LayoutCommands
    {
        private Mock<IUnitOfWork<long>> unitOfWorkMock;
        private Mock<ILayoutRepository> _layoutRepository;

        public LayoutCommands()
        {
            unitOfWorkMock = new Mock<IUnitOfWork<long>>();
            _layoutRepository = new Mock<ILayoutRepository>();

            unitOfWorkMock
                .Setup(uow => uow.GetRepository<ILayoutRepository>())
                .Returns(_layoutRepository.Object);
        }

        [Fact]
        public async void AddLayout()
        {
            var layout = new Layout { LayoutData = 1 };
            var layoutDTO = new LayoutDTO { LayoutData = 1 };

            _layoutRepository
                .Setup(repo => repo.Insert(It.IsAny<Layout>()))
                .Returns(Task.CompletedTask);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Layout>(It.IsAny<LayoutDTO>())).Returns(layout);
            mapperMock
                .Setup(mapper => mapper.Map<LayoutDTO>(It.IsAny<Layout>()))
                .Returns((Layout c) => new LayoutDTO { LayoutData = c.LayoutData });

            var command = new AddLayoutCommand(layoutDTO);

            var handler = new AddLayoutCommandHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(CommandResult.Success, result.Result);
            Assert.Equal(layoutDTO.LayoutData, result.Object.LayoutData);
        }

        [Fact]
        public async void DeleteLayout()
        {
            var layout = new Layout { LayoutData = 1, Id = 1 };

            _layoutRepository
                 .Setup(repo => repo.FindById(1))
                 .ReturnsAsync(layout);

            _layoutRepository.Setup(r => r.Contains(layout))
                .ReturnsAsync(true);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<LayoutDTO>(layout))
                .Returns(new LayoutDTO { Id = layout.Id, LayoutData = layout.LayoutData });

            var command = new DeleteLayoutCommand(1);

            var handler = new DeleteLayoutCommandHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(CommandResult.Success, result.Result);
            Assert.Equal(layout.Id, result.Object.Id);
            Assert.Equal(layout.LayoutData, result.Object.LayoutData);
        }

        [Fact]
        public async void UpdateLayout()
        {
            var layout = new Layout { LayoutData = 1 };
            var layoutDTO = new LayoutDTO { LayoutData = 1 };

            _layoutRepository.Setup(r => r.Contains(It.IsAny<Layout>()))
                .ReturnsAsync(true);

            _layoutRepository.Setup(r => r.Update(It.IsAny<Layout>()));

            _layoutRepository.Setup(r => r.FindById(1))
                .ReturnsAsync(layout);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Layout>(It.IsAny<LayoutDTO>()))
                .Returns(layout);
            mapperMock.Setup(mapper => mapper.Map<LayoutDTO>(It.IsAny<Layout>()))
            .Returns(layoutDTO);

            var command = new UpdateLayoutCommand(layoutDTO);

            var handler = new UpdateLayoutCommandHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            _layoutRepository.Verify(repo => repo.Contains(layout), Times.Once);
            _layoutRepository.Verify(repo => repo.Update(layout), Times.Once);
            Assert.Equal(CommandResult.Success, result.Result);
            Assert.Equal(layoutDTO.LayoutData, result.Object.LayoutData);
        }
    }
}
