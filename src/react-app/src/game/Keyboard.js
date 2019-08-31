import React from "react";
import PropTypes from "prop-types";
import { Button } from "reactstrap";

function lowercaseCharRange() {
  const asciiLowercaseBegin = 97;
  const asciiLowercaseEnd = 122;
  const length = asciiLowercaseEnd - asciiLowercaseBegin + 1;

  return Array.from({ length }, (_, index) =>
    String.fromCharCode(asciiLowercaseBegin + index),
  );
}

function Keyboard({ guessedLetters, onPressLetter }) {
  function onClick({ target: { value } }) {
    onPressLetter(value);
  }

  return (
    <div className="d-flex justify-content-around flex-wrap">
      {lowercaseCharRange().map(letter => {
        const disabled = guessedLetters.includes(letter);

        return (
          <Button
            className="m-1"
            color="secondary"
            disabled={disabled}
            key={letter}
            onClick={onClick}
            value={letter}
          >
            {letter}
          </Button>
        );
      })}
    </div>
  );
}

Keyboard.propTypes = {
  guessedLetters: PropTypes.array.isRequired,
  onPressLetter: PropTypes.func.isRequired,
};

export default Keyboard;
