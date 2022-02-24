namespace SnakesAndLadders.Core {
    public interface IDice {
        int Value { get; }

        int Roll();
    }
}