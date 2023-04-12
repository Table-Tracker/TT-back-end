using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableTracker.Domain.Interfaces.Repositories;
using TableTracker.Domain.Interfaces;
using AutoMapper;
using System.Threading;
using TableTracker.Application.CQRS.Layout.Commands.AddLayout;
using TableTracker.Application.CQRS.Layout.Commands.DeleteLayout;
using TableTracker.Application.CQRS.Layout.Commands.UpdateLayout;
using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;
using TableTracker.Infrastructure.Repositories;
using Xunit;
using TableTracker.Application.CQRS.Managers.Commands.AddManager;
using TableTracker.Application.CQRS.Managers.Commands.DeleteManager;
using TableTracker.Application.CQRS.Managers.Commands.UpdateManager;

namespace TableTracker.Tests.ManagerTests
{
    public class ManagerCommands
    {
        private Mock<IUnitOfWork<long>> unitOfWorkMock;
        private Mock<IManagerRepository> _managerRepository;

        public ManagerCommands()
        {
            unitOfWorkMock = new Mock<IUnitOfWork<long>>();
            _managerRepository = new Mock<IManagerRepository>();

            unitOfWorkMock
                .Setup(uow => uow.GetRepository<IManagerRepository>())
                .Returns(_managerRepository.Object);
        }

        [Fact]
        public async void AddManager()
        {
            var manager = new Manager { FullName = "John Smith" };
            var managerDTO = new ManagerDTO { FullName = "John Smith" };

            _managerRepository
                .Setup(repo => repo.Insert(It.IsAny<Manager>()))
                .Returns(Task.CompletedTask);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Manager>(It.IsAny<ManagerDTO>())).Returns(manager);
            mapperMock
                .Setup(mapper => mapper.Map<ManagerDTO>(It.IsAny<Manager>()))
                .Returns((Manager c) => new ManagerDTO { FullName = c.FullName });

            var command = new AddManagerCommand(managerDTO);

            var handler = new AddManagerCommandHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(CommandResult.Success, result.Result);
            Assert.Equal(managerDTO.FullName, result.Object.FullName);
        }

        [Fact]
        public async void DeleteManager()
        {
            var manager = new Manager { FullName = "John Smith", Id = 1 };

            _managerRepository
                 .Setup(repo => repo.FindById(1))
                 .ReturnsAsync(manager);

            _managerRepository.Setup(r => r.Contains(manager))
                .ReturnsAsync(true);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<ManagerDTO>(manager))
                .Returns(new ManagerDTO { Id = manager.Id, FullName = manager.FullName });

            var command = new DeleteManagerCommand(1);

            var handler = new DeleteManagerCommandHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(CommandResult.Success, result.Result);
            Assert.Equal(manager.Id, result.Object.Id);
            Assert.Equal(manager.FullName, result.Object.FullName);
        }

        [Fact]
        public async void UpdateManager()
        {
            var manager = new Manager { FullName = "John Smith" };
            var managerDTO = new ManagerDTO { FullName = "John Smith" };

            _managerRepository.Setup(r => r.Contains(It.IsAny<Manager>()))
                .ReturnsAsync(true);

            _managerRepository.Setup(r => r.Update(It.IsAny<Manager>()));

            _managerRepository.Setup(r => r.FindById(1))
                .ReturnsAsync(manager);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Manager>(It.IsAny<ManagerDTO>()))
                .Returns(manager);
            mapperMock.Setup(mapper => mapper.Map<ManagerDTO>(It.IsAny<Manager>()))
            .Returns(managerDTO);

            var command = new UpdateManagerCommand(managerDTO);

            var handler = new UpdateManagerCommandHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            _managerRepository.Verify(repo => repo.Contains(manager), Times.Once);
            _managerRepository.Verify(repo => repo.Update(manager), Times.Once);
            Assert.Equal(CommandResult.Success, result.Result);
            Assert.Equal(managerDTO.FullName, result.Object.FullName);
        }
    }
}
