namespace SnakesAndLadders.Core {
    /// <summary>
    /// Minimal game properties
    /// </summary>
    public interface IGame {
        IEnumerable<IToken> Tokens { get; }

        void Start();

        void Finish();

        void AddToken(IToken token);

        void Move(IToken token, int position);

        bool IsTheWinner(IToken token);
    }
}