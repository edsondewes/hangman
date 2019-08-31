import React from "react";
import PropTypes from "prop-types";

const EmptyLetter = "\0";

function Word({ letters }) {
  return (
    <div className="d-flex justify-content-around my-5">
      {letters.map((letter, index) => (
        <span className="display-4" key={index}>
          {letter === EmptyLetter ? "_" : letter}
        </span>
      ))}
    </div>
  );
}

Word.propTypes = {
  letters: PropTypes.array.isRequired,
};

export default Word;
