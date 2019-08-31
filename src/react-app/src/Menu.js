import React from "react";
import PropTypes from "prop-types";
import { Alert, Button, Modal, ModalHeader, ModalBody } from "reactstrap";
import Octicon, { Flame, Octoface } from "@primer/octicons-react";
import { EasyGameKind, HardGameKind } from "./GameKind";
import { StatusWin, StatusLose } from "./GameStatus";

const LoseAlert = () => (
  <Alert color="danger">You lose! Best of luck next time!</Alert>
);
const WinAlert = () => <Alert color="success">You win! Congratulations!</Alert>;

function Menu({ gameStatus, onStartGame }) {
  return (
    <Modal isOpen={true}>
      <ModalHeader className="justify-content-center">
        Let&lsquo;s play hangman!
      </ModalHeader>
      <ModalBody>
        {gameStatus === StatusWin && <WinAlert />}
        {gameStatus === StatusLose && <LoseAlert />}
        <div className="d-flex justify-content-around">
          <Button color="success" onClick={() => onStartGame(EasyGameKind)}>
            <Octicon icon={Octoface} /> Easy mode
          </Button>
          <Button color="danger" onClick={() => onStartGame(HardGameKind)}>
            <Octicon icon={Flame} /> Hard mode
          </Button>
        </div>
      </ModalBody>
    </Modal>
  );
}

Menu.propTypes = {
  gameStatus: PropTypes.number,
  onStartGame: PropTypes.func.isRequired,
};

export default Menu;
