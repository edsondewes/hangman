# Hangman
Simple hangman game using .NET Core and React.

## Running with docker-compose
What you need for this step:
 - [docker](https://docs.docker.com/install/)
 - [docker-compose](https://docs.docker.com/compose/install/)

Go to the project root folder and start the containers using the docker-compose CLI:

```bash
docker-compose up -d
```
This will build the images and start the services.
After that, you can go to your web browser and check the url: http://localhost:8080

If you don't want to (or can't) use the port 8080, you can change it by editing the *docker-compose.yml* file.


## Running in development mode

### .NET API
What you need for this step:
 - [.NET Core SDK](https://dotnet.microsoft.com/download)

You can run the dotnet API using the Visual Studio runner or via CLI:
```bash
cd src/api/Hangman.Api/
dotnet run
```
The API will start at http://localhost:5000.

### React App
What you need for this step:
 - [NodeJS](https://nodejs.org/en/download/)

Go to the React App folder and make sure you have installed all packages:
```bash
cd src/react-app/
npm install
```
After that, you can start the development server:
```bash
npm start
```
The app start at http://localhost:3000.
Make sure that the API is running before starting a game.

### Configuration
By default, the .NET API uses an in-memory database.
You can switch to a MongoDb database adding some settings to your *appsettings.json* (or environment variables):
```json
"MongoDb": {
  "Host": "mongodb://localhost:27017",
  "Database": "hangman"
}
```
If the startup detected this setting is set, it will use the MongoDb persistence.

## Playing the game
There are two modes: easy and hard.
The easy mode gives you two helps and three chances of choosing the wrong letter.
If you choose the hard mode, you can't go wrong and you won't have help!

![Modes](img/modes.PNG?raw=true "Modes")

### Help!
There's one kind of help available. If you click the **Reveal letter** button, the game will show a missing letter from the current word.

![Help](img/reveal-letter.PNG?raw=true "Help")

### Wrong shots remaining
The hearts at your right show how many times you can make mistakes. Be careful if you only have one chance.

![Lifes](img/lifes.PNG?raw=true "Lifes")

### Try to fill in the word
Use the virtual keyboard to enter the letters you think are right.

![Virtual Keyboard](img/keyboard.PNG?raw=true "Virtual Keyboard")

---

**Enjoy the game!**
