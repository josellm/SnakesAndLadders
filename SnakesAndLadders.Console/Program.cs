// See https://aka.ms/new-console-template for more information
using System.Text;
using SnakesAndLadders.Core;
using SnakesAndLadders.UI;

IPainter painter = new ConsolePainter();
var game = new SnakesAndLaddersGame();
var dice = new Dice();

AddSomeLaddersAndSnakes(game);
while (true) {
    painter.Clear();
    painter.Header(game);
    painter.Menu();
     switch (Console.ReadKey(true).Key) {
        case ConsoleKey.D1:
            AddToken(game);
            break;
        case ConsoleKey.D2:
            NewGame(game, dice);
            break;
        case ConsoleKey.D3: //Exit
            painter.Message("Bye!");
            game.Finish();
            return;
    }
}

static void AddSomeLaddersAndSnakes(SnakesAndLaddersGame game) {
    game.AddLadder(2, 38);
    game.AddLadder(7, 14);
    game.AddLadder(8, 31);
    game.AddLadder(15, 26);
    game.AddLadder(21, 42);
    game.AddLadder(36, 44);
    game.AddLadder(51, 67);
    game.AddLadder(71, 91);
    game.AddLadder(78, 98);
    game.AddLadder(87, 94);
    game.AddSnake(16, 6);
    game.AddSnake(46, 25);
    game.AddSnake(49, 11);
    game.AddSnake(62, 19);
    game.AddSnake(74, 53);
    game.AddSnake(89, 68);
    game.AddSnake(92, 88);
    game.AddSnake(95, 75);
    game.AddSnake(99, 80);
}

//Starts a new game, paint the board and controls the token turn
void NewGame(SnakesAndLaddersGame game, Dice dice) {
    if (!game.Tokens.Any()) {
        painter.Message("Add a token first");
        return;
    }
    game.Start();
    painter.Board(game);
    //Game loop
    bool anyWinner = false;
    do {
        foreach (var token in game.Tokens) {
            TokenTurn(game, dice, token);
            anyWinner = game.IsTheWinner(token);
            if (anyWinner) {
                painter.Message("Congrats {0}, you've won!", token.Name);
                Console.ReadKey(true);
                break;
            }
            Console.ReadKey(true);
        }
    } while (!anyWinner);
}

void TokenTurn(SnakesAndLaddersGame game, Dice dice, IToken token) {
    painter.Message("{0} roll the dice!", token.Name);
    Console.ReadKey(true);
    dice.Roll();
    var message = new StringBuilder();
    message.AppendFormat("{0}! ", dice.Value);

    var lastSquare = token.SquareNumber;
    var nextSquare = lastSquare + dice.Value;
    if (game.SnakeAtSquare(nextSquare))
        message.AppendFormat("Snake in square {0}! ", nextSquare);
    if (game.LadderAtSquare(nextSquare))
        message.AppendFormat("Ladder in square {0}! ", nextSquare);

    game.Move(token, dice.Value);
    painter.Message(message.AppendFormat("{0} moves to square {1}", token.Name, token.SquareNumber).ToString());
    painter.MoveToken(game, token, lastSquare);
}

void AddToken(SnakesAndLaddersGame game) {
    painter.Clear();
    if (game.Tokens.Count() >= 4)
        painter.Message("No more tokens alloweds");
    else
        game.AddToken(new Token(WriteTokenName(), SelectTokenColor(game)));
}

//Menu to select a token color not used
TokenColor SelectTokenColor(SnakesAndLaddersGame game) {
    painter.Clear();
    painter.Message("Select color:");
    var left = painter.WindowWidth / 2 - 8;
    var top = 5;
    var freeColors = TokenColor.GetValues<TokenColor>().Where(color => !game.Tokens.Any(token => token.Color == color)).ToArray();
    for (int i = 0; i < freeColors.Length; i++) {
        var tokenColor = freeColors[i];
        painter.SetPosition(left, top + i);
        painter.WriteText("{0}. {1}", ((int)tokenColor + 1), tokenColor);
    }
    TokenColor selectedColor;
    do {
        selectedColor = (TokenColor)(Console.ReadKey(true).Key - ConsoleKey.D0 - 1);
    } while (!freeColors.Contains(selectedColor));
    return selectedColor;
}

//Input the token name
string WriteTokenName() {
    painter.Message("Token name:");
    var tokenName = string.Empty;
    do {
        var left = painter.WindowWidth / 2 - 4;
        var top = 5;
        painter.SetPosition(left, top);
        tokenName = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(tokenName));
    return tokenName;
}