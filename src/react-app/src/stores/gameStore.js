// eslint-disable-next-line no-undef
const baseUrl = __API_URL__ + "/api/games";

export function postGame(kind) {
  return postJson(baseUrl, { kind });
}

export function postGuessMove(gameId, letter) {
  return postJson(`${baseUrl}/${gameId}/moves/guess`, { letter });
}

export function postRevealMove(gameId) {
  return postJson(`${baseUrl}/${gameId}/moves/reveal`);
}

async function postJson(url, data) {
  const response = await fetch(url, {
    body: data ? JSON.stringify(data) : null,
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    method: "POST",
  });

  const json = await response.json();
  return json;
}
