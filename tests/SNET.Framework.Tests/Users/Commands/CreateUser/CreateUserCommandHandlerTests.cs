using FluentValidation;
using FluentValidation.Results;
using Moq;
using SNET.Framework.Domain.Entities;
using SNET.Framework.Domain.Repositories;
using SNET.Framework.Domain.Shared;
using SNET.Framework.Domain.UnitOfWork;
using SNET.Framework.Features.Users.Commands;

namespace SNET.Framework.Tests.Users.Commands.CreateUser;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IValidator<CreateUserCommand>> _validatorMock;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _validatorMock = new Mock<IValidator<CreateUserCommand>>();

        _handler = new CreateUserCommandHandler(
            _userRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _validatorMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldAddUserAndCommit()
    {
        // Arrange
        var validResult = new ValidationResult();
        
        var command = new CreateUserCommand()
        {
            Id = Guid.NewGuid(),
            Email = "alex@example.com",
            FirstName = "Test",
            LastName = "Test",
            Password = "123456",
            PhoneNumber = "3135241205"
        };

        _validatorMock
            .Setup(x => x.Validate(It.IsAny<CreateUserCommand>()))
            .Returns(validResult);

        // Act
        Result result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _userRepositoryMock
            .Verify(repo => repo.Add(It.IsAny<User>()), Times.Once);
        _unitOfWorkMock
            .Verify(uow => uow.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidCommand_ShouldValidation()
    {
        // Arrange
        var command = new CreateUserCommand { Email = "" };
        var validationFailures = new List<ValidationFailure>
        {
            new ValidationFailure("Email", "Email cannot be empty")
        };
        _validatorMock.Setup(v => v.Validate(command)).Returns(new ValidationResult(validationFailures));

        // Act
        Result result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Error);
        Assert.Equal("Datos no válidos", result.Error!.Message);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldNotCallCommitIfAddFails()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Id = Guid.NewGuid(),
            Email = "alex@example.com",
            FirstName = "Test",
            LastName = "Test",
            Password = "123456",
            PhoneNumber = "3135241205"
        };
        _validatorMock.Setup(v => v.Validate(command)).Returns(new ValidationResult());
        _userRepositoryMock.Setup(repo => repo.Add(It.IsAny<User>())).Throws(new Exception("Add failed"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(CancellationToken.None), Times.Never);
    }
}
