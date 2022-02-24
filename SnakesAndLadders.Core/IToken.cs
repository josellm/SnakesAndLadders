namespace SnakesAndLadders.Core {
    public interface IToken {
        string Name { get; }

        TokenColor Color { get; }

        int SquareNumber { get; }

        int Score { get; set; }

        void SetSquareNumber(int position);
    }
}