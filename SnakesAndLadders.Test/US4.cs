using System;
using NUnit.Framework;
using SnakesAndLadders.Core;
using StoryQ;

namespace SnakesAndLadders.Test {

    public class US4Test: GameTestBase {
        private const int snakeHead = 20;
        private const int snakeTail = 10;
        private const int ladderLowerSide = 5;
        private const int ladderUpperSide = 15;

        [SetUp]
        public override void Setup() {
            base.Setup();
            TheGameIsStarted();
        }
        [Test]
        public void SnakesAndLaddersChangeTheSquarePosition() {
            new Story("US 4 - Snakes and ladders change the square position")
                    .InOrderTo("to make the game more fun")
                    .AsA("Player")
                    .IWant("to move my token based on the roll of a die")
                        .WithScenario("UAT1 - When the token is on a ladder square is moved to a defined upper position")
                                .Given(SomeLaddersInTheGame)
                                .When(TheTokenMovesOnALadderSquare)
                                .Then(TheTokeIsTranslatedToAUpperSquare)
                        .WithScenario("UAT2 - When the snake is on a ladder square is moved to a defined upper position")
                                .Given(SomeSnakesInTheGame)
                                .When(TheTokenMovesOnASnakeSquare)
                                .Then(TheTokeIsTranslatedToALowerSquare)
                    .Execute();
        }

        private void SomeLaddersInTheGame() {
            Assert.IsTrue(_game.AddLadder(ladderLowerSide, ladderUpperSide));
        }

        private void SomeSnakesInTheGame() {
            Assert.IsTrue(_game.AddSnake(snakeHead, snakeTail));
        }

        private void TheTokenMovesOnASnakeSquare() {
            _game.Move(_token, snakeHead - _token.SquareNumber);
        }

        private void TheTokenMovesOnALadderSquare() {
            _game.Move(_token, ladderLowerSide - _token.SquareNumber);
        }

        private void TheTokeIsTranslatedToALowerSquare() {
            Assert.AreEqual(snakeTail, _token.SquareNumber);
        }
        private void TheTokeIsTranslatedToAUpperSquare() {
            Assert.AreEqual(ladderUpperSide, _token.SquareNumber);
        }

    }
}