using System;
using System.Threading.Tasks;
using Hangman.Core.Commands;
using Hangman.Core.Events;
using Hangman.Core.Queries;
using MediatR;
using Moq;
using Xunit;

namespace Hangman.Core.Tests.Commands
{
    public class ApplyMoveCommandTests
    {
        [Fact]
        public void ConstructorShouldNotAcceptNullMove()
        {
            Assert.Throws<ArgumentNullException>(() => new ApplyMoveCommand(Guid.Empty, null));
        }

        [Fact]
        public async Task HandlerShoulThrowIfGameDoesNotExists()
        {
            var move = new Mock<Move>();
            var mediator = new Mock<IMediator>();

            var command = new ApplyMoveCommand(Guid.Empty, move.Object);
            var handler = new ApplyMoveHandler(mediator.Object);
            await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(command, default));
        }

        [Fact]
        public async Task HandlerShouldPublishAppliedEvent()
        {
            var gameId = Guid.NewGuid();
            var move = new Mock<Move>();
            var parameters = new Mock<IParameters>();
            parameters
                .Setup(p => p.MaxWrongGuesses)
                .Returns(3);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(m => m.Send(It.IsAny<GameByIdQuery>(), default))
                .ReturnsAsync(new Game(gameId, parameters.Object, "word"));

            var command = new ApplyMoveCommand(gameId, move.Object);
            var handler = new ApplyMoveHandler(mediator.Object);
            var game = await handler.Handle(command, default);

            mediator.Verify(m => m.Publish(It.IsAny<MoveAppliedEvent>(), default), Times.Once);
        }
    }
}
