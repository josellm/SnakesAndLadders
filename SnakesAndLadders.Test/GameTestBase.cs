using NUnit.Framework;
using SnakesAndLadders.Core;

namespace SnakesAndLadders.Test {
    public abstract class GameTestBase {
        protected SnakesAndLaddersGame _game;
        protected IToken _token;

        [SetUp]
        public virtual void Setup() {
            _game = new SnakesAndLaddersGame();
        }

        [TearDown]
        public void Stop() {
            _game.Finish();
        }

        protected void TheGameIsStarted() {
            _token = new Token("Dummy", TokenColor.Red);
            _game.AddToken(_token);
            _game.Start();
        }

        protected void TheTokenIsOn_Square(int position) {
            Assert.AreEqual(position, _token.SquareNumber, string.Format(@"Token {0} is on square {1}", _token.Name, _token.SquareNumber));
        }

        protected void TheTokenIsSetOn_Square(int position) {
            _token.SetSquareNumber(position);
        }

        protected void TheTokenIsMoved_NSpaces(int spaces) {
            _game.Move(_token, spaces);
        }
    }
}