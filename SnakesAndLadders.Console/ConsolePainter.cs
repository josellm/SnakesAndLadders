using SnakesAndLadders.Core;

namespace SnakesAndLadders.UI {

    /// <summary>
    /// Paint the game in a console
    /// TODO: Separate primitives from screen logic and divide screen in components like Menu->Component->IComponent
    /// </summary>
    public class ConsolePainter : IPainter {
        private const int squareCharSize = 4;
        private const int rowMesssage = 2;
        private const int rowBoard = 5;
        private const int columnBoard = 5;
        private const int numRowsBoard = 10;
        private const int numColumnsBoard = 10;

        public ConsolePainter() {
            SetBackgroundColor(GameColor.Black);
        }

        public int WindowWidth => Console.WindowWidth;

        public int WindowHeight => Console.WindowHeight;

        public void Header(SnakesAndLaddersGame game) {
            SetPosition(WindowWidth / 2 - "Snakes & Ladders".Length/2, 0);//Center text in the screen
            SetForegroundColor(GameColor.Green);
            WriteText("Snakes");
            SetForegroundColor(GameColor.White);
            WriteText(" & ");
            SetForegroundColor(GameColor.Yellow);
            WriteText("Ladders");
            //Tokens with scores
            var tokens = game.Tokens.ToList();
            for (int i = 0; i < tokens.Count; i++)
                Score(tokens[i], i % 2 == 0 ? 0 : (WindowWidth - 20), i < 2 ? 0 : 1);
        }

        public void Menu() {
            Message("Menu");
            var left = WindowWidth / 2 - "1. Add Token".Length/2;
            SetForegroundColor(GameColor.Yellow);
            SetPosition(left, rowBoard);
            WriteText("1. Add Token");
            SetPosition(left, rowBoard + 1);
            WriteText("2. New game");
            SetPosition(left, rowBoard + 2);
            WriteText("3. Exit");
        }

        public void Message(string message, params object[] args) {
            SetPosition(0, rowMesssage);
            SetForegroundColor(GameColor.Red);
            var margin = (WindowWidth + message.Length) / 2 - 1;
            var text = message.PadLeft(margin).PadRight(WindowWidth - 1); //center text writing space to clear old messages
            WriteText(text, args);
            SetForegroundColor(GameColor.Yellow);
        }

        public void Board(SnakesAndLaddersGame game) {
            Clear();
            var left = WindowWidth / 2 - columnBoard * squareCharSize;
            for (int x = 0; x < numColumnsBoard; x++) {
                SetPosition(left, rowBoard + x);
                for (int y = 0; y < numRowsBoard; y++)
                    Square(game, x * numColumnsBoard + y + 1);
            }
            SetBackgroundColor(GameColor.Black);
        }

        public void MoveToken(SnakesAndLaddersGame game, IToken token, int oldPosition) {
            var startleft = WindowWidth / 2 - columnBoard * squareCharSize;
            var top = GetYCoordFromSquare(oldPosition);
            var left = GetXCoordFromSquare(oldPosition);
            SetPosition(left + startleft, rowBoard + top);
            Square(game, oldPosition);
            top = GetYCoordFromSquare(token.SquareNumber);
            left = GetXCoordFromSquare(token.SquareNumber);
            SetPosition(left + startleft, rowBoard + top);
            SetForegroundColor(ToGameColor(token.Color));
            SetBackgroundColor(GetColorFromSquare(token.SquareNumber));
            WriteText("  © ");
            SetBackgroundColor(GameColor.Black);
        }

        public void Clear() {
            var clearText = string.Empty.PadRight(WindowWidth);
            for (int y = rowBoard; y < WindowHeight - rowBoard; y++) {
                SetPosition(0, y);
                WriteText(clearText);
            }
        }

        public void SetBackgroundColor(GameColor color) {
            Console.BackgroundColor = ToConsoleColor(color);
        }

        public void SetForegroundColor(GameColor color) {
            Console.ForegroundColor = ToConsoleColor(color);
        }

        public void SetPosition(int x, int y) {
            Console.SetCursorPosition(x, y);
        }

        public void WriteText(string text, params object[] args) {
            Console.Write(text, args);
        }

        private void Score(IToken token, int x, int y) {
            SetPosition(x, y);
            SetForegroundColor(ToGameColor(token.Color));
            //Truncate name max 15 chars
            WriteText("{0}: {1}", token.Name.Substring(0, Math.Min(token.Name.Length, 15)), token.Score);
        }

        private void Square(SnakesAndLaddersGame game, int squareNumber) {
            SetBackgroundColor(GetColorFromSquare(squareNumber));
            if (game.LadderAtSquare(squareNumber)) 
                FillSquare(GameColor.Yellow,"  L "); //Ladder
            else if (game.SnakeAtSquare(squareNumber))
                FillSquare(GameColor.Red,"  S "); //Snake
            else if (squareNumber == 100) 
                FillSquare(GameColor.Green," FIN"); //FINISH
            else 
                FillSquare(GameColor.White, string.Format(" {0:00} ", squareNumber)); //Square number
        }

        private void FillSquare(GameColor color, string text) {
            SetForegroundColor(color);
            WriteText(text);
        }

        private ConsoleColor ToConsoleColor(GameColor color) {
            return ConsoleColor.Parse<ConsoleColor>(color.ToString());
        }

        private static GameColor ToGameColor(TokenColor color) {
            return Enum.Parse<GameColor>(color.ToString());
        }

        /// <summary>
        /// Deterministic square color
        /// </summary>
        /// <param name="squareNumber"></param>
        /// <returns></returns>
        private static GameColor GetColorFromSquare(int squareNumber) {
            return (GameColor)(squareNumber % 6 + 1);
        }

        /// <summary>
        /// Given a square number return the X position in the matrix board
        /// </summary>
        /// <param name="square"></param>
        /// <returns></returns>
        private static int GetXCoordFromSquare(int square) {
            var tmp = square % numColumnsBoard;
            if (tmp == 0)
                tmp = 10;
            return tmp * squareCharSize - squareCharSize;
        }

        /// <summary>
        /// Given a square number return the Y position in the matrix board
        /// </summary>
        /// <param name="square"></param>
        /// <returns></returns>
        private static int GetYCoordFromSquare(int square) {
            return (square - 1) / numRowsBoard;
        }
    }
}