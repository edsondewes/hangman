using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Hangman.Api.ViewModels;
using Hangman.Core;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Hangman.Api.Tests.Controllers
{
    public class GamesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public GamesControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetShouldReturn404IfTheGameDoesNotExists()
        {
            var randomGuid = Guid.NewGuid();
            var response = await _client.GetAsync($"/api/games/{randomGuid}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetShouldReturnTheSameDataOfThePost()
        {
            var client = _factory.CreateClient();

            var model = new
            {
                kind = (int)NewGameModel.GameModeKind.Easy
            };

            var postResponse = await client.PostAsJsonAsync($"/api/games", model);
            var newGame = await postResponse.Content.ReadAsAsync<GameStateModel>();

            var getResponse = await client.GetAsync($"/api/games/{newGame.Id}");
            var getModel = await getResponse.Content.ReadAsAsync<GameStateModel>();

            Assert.True(getResponse.IsSuccessStatusCode);
            Assert.Equal(newGame.Id, getModel.Id);
            Assert.Equal(newGame.GuessedLetters, getModel.GuessedLetters);
            Assert.Equal(newGame.Status, getModel.Status);
            Assert.Equal(newGame.Word, getModel.Word);
        }

        [Fact]
        public async Task PostShouldStartANewGame()
        {
            var model = new
            {
                kind = (int)NewGameModel.GameModeKind.Easy
            };

            var response = await _client.PostAsJsonAsync($"/api/games", model);
            var content = await response.Content.ReadAsAsync<GameStateModel>();

            Assert.NotEqual(Guid.Empty, content.Id);
            Assert.Empty(content.GuessedLetters);
            Assert.True(content.Word.All(letter => letter == default));
            Assert.Equal(Status.Open, content.Status);
        }

        [Fact]
        public async Task PostShouldNotAllowNotMappedKinds()
        {
            var model = new
            {
                kind = 999
            };

            var response = await _client.PostAsJsonAsync($"/api/games", model);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
