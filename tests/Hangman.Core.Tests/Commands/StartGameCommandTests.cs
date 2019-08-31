using System;
using System.Threading.Tasks;
using Hangman.Core.Commands;
using Hangman.Core.Events;
using MediatR;
using Moq;
using Xunit;

namespace Hangman.Core.Tests.Commands
{
    public class StartGameCommandTests
    {
        [Fact]
        public void ConstructorShouldNotAcceptNullParameters()
        {
            Assert.Throws<ArgumentNullException>(() => new StartGameCommand(null));
        }

        [Fact]
        public async Task HandlerShouldPublishCreatedEvent()
        {
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<GenerateWordCommand>(), default)).ReturnsAsync("word");

            var parameters = new Mock<IParameters>();

            var handler = new StartGameHandler(mediator.Object);
            var command = new StartGameCommand(parameters.Object);

            var game = await handler.Handle(command, default);

            mediator.Verify(m => m.Publish(It.IsAny<GameStartedEvent>(), default), Times.Once);
        }
    }
}
