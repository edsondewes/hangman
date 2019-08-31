import React from "react";
import PropTypes from "prop-types";
import { Badge, Button } from "reactstrap";
import Octicon, { Star } from "@primer/octicons-react";

function Helps({ disabled, helpsLeft, onRevealLetter }) {
  return (
    <Button
      color="info"
      disabled={disabled || helpsLeft === 0}
      onClick={onRevealLetter}
      title="Need help? Reveal a letter using this star power!"
    >
      <Octicon icon={Star} className="text-warning" /> Reveal letter{" "}
      <Badge color="light">{helpsLeft}</Badge>
    </Button>
  );
}

Helps.propTypes = {
  disabled: PropTypes.bool.isRequired,
  helpsLeft: PropTypes.number.isRequired,
  onRevealLetter: PropTypes.func.isRequired,
};

export default Helps;
