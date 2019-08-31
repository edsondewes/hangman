using System;
using System.Threading.Tasks;
using Hangman.Api.ViewModels;
using Hangman.Core;
using Hangman.Core.Commands;
using Hangman.Core.Modes;
using Hangman.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hangman.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GamesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get the state of a game
        /// </summary>
        /// <param name="id">Game ID</param>
        /// <returns>GameStateModel instance</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GameStateModel>> Get(Guid id)
        {
            var game = await _mediator.Send(new GameByIdQuery(id));
            if (game is null)
            {
                return NotFound(new { gameId = id });
            }

            return GameStateModel.Map(game);
        }

        /// <summary>
        /// Start a new game
        /// </summary>
        /// <param name="model">Game options</param>
        /// <returns>GameStateModel instance</returns>
        [HttpPost]
        public async Task<ActionResult<GameStateModel>> Post([FromBody]NewGameModel model)
        {
            IParameters parameters;
            switch (model.Kind)
            {
                case NewGameModel.GameModeKind.Easy:
                    parameters = new EasyGameMode();
                    break;
                case NewGameModel.GameModeKind.Hard:
                    parameters = new HardGameMode();
                    break;
                default:
                    return BadRequest("Game mode not allowed");
            }

            var game = await _mediator.Send(new StartGameCommand(parameters));
            return GameStateModel.Map(game);
        }
    }
}
