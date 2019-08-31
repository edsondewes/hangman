import React, { useEffect, useState } from "react";
import { Button } from "reactstrap";
import Octicon, { Home } from "@primer/octicons-react";
import Game from "./game/";
import Menu from "./Menu";
import { StatusWin, StatusLose } from "./GameStatus";
import { postGame, postGuessMove, postRevealMove } from "./stores/gameStore";

function App() {
  const [currentGame, setCurrentGame] = useState(null);
  const [showMenu, setShowMenu] = useState(true);

  useEffect(() => {
    if (
      currentGame &&
      (currentGame.status == StatusWin || currentGame.status == StatusLose)
    ) {
      setShowMenu(true);
    }
  }, [currentGame]);

  async function guessLetter(letter) {
    const newState = await postGuessMove(currentGame.id, letter);
    setCurrentGame(newState);
  }

  async function revealLetter() {
    const newState = await postRevealMove(currentGame.id);
    setCurrentGame(newState);
  }

  async function startNewGame(kind) {
    const game = await postGame(kind);

    setShowMenu(false);
    setCurrentGame(game);
  }

  return (
    <>
      <h1 className="display-3">Hangman</h1>
      <div className="d-flex justify-content-between align-items-start">
        <p className="lead">Let&lsquo;s have some fun!</p>
        <Button color="light" onClick={() => setShowMenu(true)}>
          <Octicon icon={Home} /> New game
        </Button>
      </div>
      <hr />
      {showMenu && (
        <Menu
          gameStatus={currentGame ? currentGame.status : null}
          onStartGame={startNewGame}
        />
      )}
      {currentGame && (
        <Game
          state={currentGame}
          onPressLetter={guessLetter}
          onRevealLetter={revealLetter}
        />
      )}
    </>
  );
}

export default App;
