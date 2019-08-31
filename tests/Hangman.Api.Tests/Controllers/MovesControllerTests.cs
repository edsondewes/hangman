using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Hangman.Api.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Hangman.Api.Tests.Controllers
{
    public class MovesControllerTests : IAsyncLifetime, IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        private Guid _gameId;

        public MovesControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        //Start a new game to use in the tests
        public async Task InitializeAsync()
        {
            var model = new
            {
                kind = (int)NewGameModel.GameModeKind.Easy
            };

            var response = await _client.PostAsJsonAsync($"/api/games", model);
            var content = await response.Content.ReadAsAsync<GameStateModel>();

            _gameId = content.Id;
        }

        [Fact]
        public async Task PostGuessLetterShouldReturn404IfTheGameDoesNotExists()
        {
            var randomGuid = Guid.NewGuid();
            var response = await _client.PostAsJsonAsync($"/api/games/{randomGuid}/moves/guess", new { letter = 'a' });
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData('A', HttpStatusCode.BadRequest)]
        [InlineData('$', HttpStatusCode.BadRequest)]
        [InlineData('1', HttpStatusCode.BadRequest)]
        [InlineData('a', HttpStatusCode.OK)]
        public async Task PostGuessLetterSholdAllowOnlyLowercaseLetters(char letter, HttpStatusCode expectedStatusCode)
        {
            var response = await _client.PostAsJsonAsync($"/api/games/{_gameId}/moves/guess", new { letter });
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact]
        public async Task PostGuessLetterShouldReturnBadRequestWhenNewMovesAreNotAllowed()
        {
            // Guess a wrong letter 3 times to end the game
            await _client.PostAsJsonAsync($"/api/games/{_gameId}/moves/guess", new { letter = 'x' });
            await _client.PostAsJsonAsync($"/api/games/{_gameId}/moves/guess", new { letter = 'y' });
            await _client.PostAsJsonAsync($"/api/games/{_gameId}/moves/guess", new { letter = 'z' });

            var response = await _client.PostAsJsonAsync($"/api/games/{_gameId}/moves/guess", new { letter = 'a' });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostRevealLetterShouldReturn404IfTheGameDoesNotExists()
        {
            var randomGuid = Guid.NewGuid();
            var response = await _client.PostAsJsonAsync($"/api/games/{randomGuid}/moves/reveal", new { });
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PostRevealShouldReturnBadRequestWhenHelpsAreNotAllowed()
        {
            // Use reveal twice so we dont have helps anymore
            await _client.PostAsJsonAsync($"/api/games/{_gameId}/moves/reveal", new { });
            await _client.PostAsJsonAsync($"/api/games/{_gameId}/moves/reveal", new { });

            var response = await _client.PostAsJsonAsync($"/api/games/{_gameId}/moves/reveal", new { });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
