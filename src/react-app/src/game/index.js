import React from "react";
import PropTypes from "prop-types";
import Helps from "./Helps";
import Keyboard from "./Keyboard";
import Word from "./Word";
import WrongGuessesLeft from "./WrongGuessesLeft";
import { StatusOpen } from "../GameStatus";

function Game({ state, onPressLetter, onRevealLetter }) {
  return (
    <>
      <div className="d-flex justify-content-between">
        <Helps
          disabled={state.status !== StatusOpen}
          helpsLeft={state.helpsLeft}
          onRevealLetter={onRevealLetter}
        />
        <WrongGuessesLeft
          left={state.wrongGuessesLeft}
          max={state.maxWrongGuesses}
        />
      </div>
      <Word letters={state.word} />
      {state.status === StatusOpen && (
        <Keyboard
          guessedLetters={state.guessedLetters}
          onPressLetter={onPressLetter}
        />
      )}
    </>
  );
}

Game.propTypes = {
  state: PropTypes.shape({
    guessedLetters: PropTypes.array.isRequired,
    helpsLeft: PropTypes.number.isRequired,
    maxWrongGuesses: PropTypes.number.isRequired,
    status: PropTypes.number.isRequired,
    word: PropTypes.array.isRequired,
    wrongGuessesLeft: PropTypes.number.isRequired,
  }).isRequired,
  onPressLetter: PropTypes.func.isRequired,
  onRevealLetter: PropTypes.func.isRequired,
};

export default Game;
