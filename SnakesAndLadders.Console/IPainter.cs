using SnakesAndLadders.Core;

namespace SnakesAndLadders.UI {
    public interface IPainter {
 
        int WindowWidth { get; }

        int WindowHeight { get; }

        void SetBackgroundColor(GameColor color);
        
        void SetForegroundColor(GameColor color);
        
        /// <summary>
        /// Set the brush position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void SetPosition(int x, int y);

        void WriteText(string text, params object[] args);

        void Menu();

        void Header(SnakesAndLaddersGame game);

        void Message(string message, params object[] args);
 
        void Board(SnakesAndLaddersGame game);
 
        void MoveToken(SnakesAndLaddersGame game, IToken token, int oldPosition);
        
        void Clear();
    }

}
