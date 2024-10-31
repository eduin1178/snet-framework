using Moq;
using SNET.Framework.Domain.Entities;
using SNET.Framework.Domain.Repositories;
using SNET.Framework.Domain.Shared;
using SNET.Framework.Domain.UnitOfWork;
using SNET.Framework.Features.Users.Commands.AssignRole;

namespace SNET.Framework.Tests.Users.Commands.AssignRole;

public class AssignRoleToUserRequestHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly AssignRoleToUserRequestHandler _handler;

    public AssignRoleToUserRequestHandlerTests() 
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _handler = new AssignRoleToUserRequestHandler(
            _userRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldAssignRoleToUserAndCommit()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var roleId = Guid.NewGuid();
        var command = new AssignRoleToUserCommand()
        {
            UserId = userId,
            RoleId = roleId,
        };

        var userWithRole = User.Create(
            userId, "test", "test", "test@example.com", "3135030716", "123456");

        //userWithRole.AssignRole(roleId);

        _userRepositoryMock
            .Setup(repo => repo.GetByIdWithRoles(It.IsAny<Guid>()))
            .ReturnsAsync(userWithRole);

        // Act
        Result result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _userRepositoryMock
            .Verify(repo => repo.GetByIdWithRoles(It.IsAny<Guid>()), Times.Once);
        _unitOfWorkMock
            .Verify(uow => uow.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldNotCallCommitIfAddFails()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var roleId = Guid.NewGuid();
        var command = new AssignRoleToUserCommand()
        {
            UserId = userId,
            RoleId = roleId,
        };

        _userRepositoryMock
            .Setup(repo => repo.GetByIdWithRoles(It.IsAny<Guid>()))
            .Throws(new Exception("Add Role by user failed"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(CancellationToken.None), Times.Never);
    }
}
