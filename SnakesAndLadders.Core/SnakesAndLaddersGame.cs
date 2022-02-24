namespace SnakesAndLadders.Core {
    /// <summary>
    /// Snakes and ladders game implementation
    /// </summary>
    public class SnakesAndLaddersGame : IGame {
        private const int goal = 100;
        private readonly List<IToken> _tokens = new List<IToken>();
        private readonly int[] _ladders = new int[goal];
        private readonly int[] _snakes = new int[goal];

        public IEnumerable<IToken> Tokens => _tokens;

        public void AddToken(IToken token) {
            _tokens.Add(token);
        }

        /// <summary>
        /// Move a token a defined number of squares
        /// </summary>
        /// <param name="token">Token to be moved</param>
        /// <param name="squares">Number of squares to move</param>
        public void Move(IToken token, int squares) {
            var nextPosition = token.SquareNumber + squares;
            if (nextPosition > goal)
                return;
            if (LadderAtSquare(nextPosition))
                nextPosition = _ladders[nextPosition];
            else if (SnakeAtSquare(nextPosition))
                nextPosition = _snakes[nextPosition];
            if (nextPosition == goal)
                token.Score++;
            token.SetSquareNumber(nextPosition);
        }

        /// <summary>
        /// Initialize the tokens postion at the first square
        /// </summary>
        public void Start() {
            foreach (var token in _tokens)
                token.SetSquareNumber(1);
        }

        /// <summary>
        /// Add a ladder to the game
        /// </summary>
        /// <param name="start">Square number of the lower side of the ladder</param>
        /// <param name="end">Square number of the upper side of the ladder</param>
        /// <returns>true if the ladder is added, false is not</returns>
        public bool AddLadder(int start, int end) {
            if (start <= 1) //Lower limit
                return false;
            if (end >= goal) //Upper limit
                return false;
            if (end <= start)
                return false;
            if (SnakeAtSquare(start) || SnakeAtSquare(end))
                return false;
            _ladders[start] = end;
            return true;
        }

        /// <summary>
        /// Add a snake to the game
        /// </summary>
        /// <param name="head">Square number of the snake head</param>
        /// <param name="tail">Square number of the snake tail</param>
        /// <returns></returns>
        public bool AddSnake(int head, int tail) {
            if (tail <= 1) //Lower limit
                return false;
            if (head >= goal) //Upper limit
                return false;
            if (tail >= head)
                return false;
            if (LadderAtSquare(head) || LadderAtSquare(tail))
                return false;
            _snakes[head] = tail;
            return true;
        }

        /// <summary>
        /// Check if there is a snake on a square number
        /// </summary>
        /// <param name="position">Square number position to check</param>
        /// <returns>True if there is a snake, false it isn't</returns>
        public bool SnakeAtSquare(int position) {
            return position < goal && _snakes[position] > 0;
        }

        /// <summary>
        /// Check if there is a ladder on a square number
        /// </summary>
        /// <param name="position">Square number position to check</param>
        /// <returns>True if there is a ladder, false it isn't</returns>
        public bool LadderAtSquare(int position) {
            return position < goal && _ladders[position] > 0;
        }

        /// <summary>
        /// Remove tokens, snakes and ladders definition
        /// </summary>
        public void Finish() {
            _tokens.Clear();
            Array.Clear(_ladders);
            Array.Clear(_snakes);
        }

        /// <summary>
        /// Check if the token is the winner according to the rules has won the game
        /// </summary>
        /// <param name="token">Token to check</param>
        /// <returns></returns>
        public bool IsTheWinner(IToken token) {
            return token.SquareNumber == goal;
        }
    }
}