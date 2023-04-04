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
using TableTracker.Application.CQRS.Tables.Queries.FindTableById;
using TableTracker.Domain.DataTransferObjects;
using TableTracker.Infrastructure.Repositories;
using TableTracker.Application.CQRS.Users.Queries.FindUserById;
using TableTracker.Domain.Entities;
using TableTracker.Application.CQRS.Tables.Queries.GetAllTables;
using TableTracker.Application.CQRS.Users.Queries.GetAllUsers;
using TableTracker.Application.CQRS.Users.Queries.GetAllUsersByFullName;
using TableTracker.Application.CQRS.Users.Queries.GetUserByEmail;

namespace TableTracker.Tests.UserTests
{
    public class UserQueries
    {
        private Mock<IUnitOfWork<long>> unitOfWorkMock;
        private Mock<IUserRepository> _userRepository;

        public UserQueries()
        {
            unitOfWorkMock = new Mock<IUnitOfWork<long>>();
            _userRepository = new Mock<IUserRepository>();

            unitOfWorkMock
                .Setup(uow => uow.GetRepository<IUserRepository>())
                .Returns(_userRepository.Object);
        }

        [Fact]
        public async void FindUserById()
        {
            var query = new FindUserByIdQuery(1);

            var user = new User { Id = 1 };
            var userDTO = new UserDTO { Id = 1 };

            _userRepository
                .Setup(repo => repo.FindById(1))
                .ReturnsAsync(user);

            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(mapper => mapper.Map<UserDTO>(user))
                .Returns(userDTO);

            var handler = new FindUserByIdQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(user.Id, result.Id);
        }

        [Fact]
        public async void GetAllUsers()
        {
            var users = new List<User>()
            {
                new User { Id = 1 },
                new User { Id = 2 },
                new User { Id = 3 },
                new User { Id = 4 },
                new User { Id = 5 },
            };

            var usersDTO = new List<UserDTO>()
            {
                new UserDTO { Id = 1 },
                new UserDTO { Id = 2 },
                new UserDTO { Id = 3 },
                new UserDTO { Id = 4 },
                new UserDTO { Id = 5 },
            };

            var query = new GetAllUsersQuery();

            _userRepository
                .Setup(repo => repo.GetAll())
                .ReturnsAsync(users);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<UserDTO>(It.IsAny<User>()))
                .Returns((User c) => new UserDTO { Id = c.Id });

            var handler = new GetAllUsersQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            var check = true;

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].Id != usersDTO[i].Id)
                {
                    check = false;
                    break;
                }
            }

            Assert.True(check);
        }

        [Fact]
        public async void GetAllUsersByFullName()
        {
            var users = new List<User>()
            {
                new User { Id = 1, FullName = "John Smith" },
                new User { Id = 2, FullName = "John Smith"},
                new User { Id = 3, FullName = "John Smith1"},
                new User { Id = 4, FullName = "John Smith2"},
                new User { Id = 5, FullName = "John Smith3"},
            };

            var usersDTO = new List<UserDTO>()
            {
                new UserDTO { Id = 1, FullName = "John Smith" },
                new UserDTO { Id = 2, FullName = "John Smith"},
            };

            var query = new GetAllUsersByFullNameQuery("John Smith");

            _userRepository
                .Setup(repo => repo.GetAllUsersByFullName("John Smith"))
                .ReturnsAsync(new List<User>(users.Take(2)));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<UserDTO>(It.IsAny<User>()))
                .Returns((User c) => new UserDTO { Id = c.Id, FullName = c.FullName });

            var handler = new GetAllUsersByFullNameQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            var check = true;

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].Id != usersDTO[i].Id || result[i].FullName != usersDTO[i].FullName)
                {
                    check = false;
                    break;
                }
            }

            Assert.True(check);
        }

        [Fact]
        public async void GetAllUsersByEmail()
        {
            var query = new GetUserByEmailQuery("JohnSmith@g.com");

            var user = new User { Id = 1, Email = "JohnSmith@g.com" };
            var userDTO = new UserDTO { Id = 1, Email = "JohnSmith@g.com" };

            _userRepository
                .Setup(repo => repo.GetUserByEmail("JohnSmith@g.com"))
                .ReturnsAsync(user);

            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(mapper => mapper.Map<UserDTO>(user))
                .Returns(userDTO);

            var handler = new GetUserByEmailQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(user.Id, result.Id);
        }
    }
}
