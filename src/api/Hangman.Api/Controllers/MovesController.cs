using System;
using System.Threading.Tasks;
using Hangman.Api.ViewModels;
using Hangman.Core.Commands;
using Hangman.Core.Moves;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hangman.Api.Controllers
{
    [Route("api/games/{gameId}/moves")]
    [ApiController]
    public class MovesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Try to guess a letter in a specified game
        /// </summary>
        /// <param name="gameId">Game ID</param>
        /// <param name="model">Guess options</param>
        /// <returns>GameStateModel instance</returns>
        [HttpPost("guess")]
        public async Task<ActionResult<GameStateModel>> PostGuessLetter(Guid gameId, [FromBody]GuessLetterModel model)
        {
            try
            {
                var move = new GuessLetterMove(model.Letter);
                var game = await _mediator.Send(new ApplyMoveCommand(gameId, move));
                return GameStateModel.Map(game);
            }
            catch (ArgumentException)
            {
                return NotFound(new { gameId });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Use some help e reveal a letter in a game
        /// </summary>
        /// <param name="gameId">Game ID</param>
        /// <returns>GameStateModel instance</returns>
        [HttpPost("reveal")]
        public async Task<ActionResult<GameStateModel>> PostRevealLetter(Guid gameId)
        {
            try
            {
                var move = new RevealLetterHelpMove();
                var game = await _mediator.Send(new ApplyMoveCommand(gameId, move));
                return GameStateModel.Map(game);
            }
            catch (ArgumentException)
            {
                return NotFound(new { gameId });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
