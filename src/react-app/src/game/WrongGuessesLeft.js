import React from "react";
import PropTypes from "prop-types";
import Octicon, { Heart, X } from "@primer/octicons-react";

function WrongGuessesLeft({ left, max }) {
  return (
    <div className="d-flex justify-content-end">
      {Array.from({ length: max }, (_, index) => {
        const isWrong = left > index;
        const icon = isWrong ? Heart : X;
        const color = isWrong ? "text-danger" : "text-dark";

        return (
          <Octicon
            className={"ml-2 " + color}
            key={index}
            icon={icon}
            size="medium"
            verticalAlign="middle"
          />
        );
      }).reverse()}
    </div>
  );
}

WrongGuessesLeft.propTypes = {
  left: PropTypes.number.isRequired,
  max: PropTypes.number.isRequired,
};

export default WrongGuessesLeft;
