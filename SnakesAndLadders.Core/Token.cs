namespace SnakesAndLadders.Core {
    /// <summary>
    /// Represents a board game token, I think a better name for this is Player
    /// TODO: Position and Score are candidates to be part of a class with the game context
    /// </summary>
    public class Token : IToken {
        private int _squareNumber;
        private readonly string _name;
        private readonly TokenColor _color;

        public Token(string name, TokenColor color) {
            _name = name;
            _color = color;
        }

        public string Name => _name;

        public TokenColor Color => _color;

        public int SquareNumber => _squareNumber;

        public int Score { get; set; }

        public void SetSquareNumber(int position) { 
            _squareNumber = position;
        }
    }
}