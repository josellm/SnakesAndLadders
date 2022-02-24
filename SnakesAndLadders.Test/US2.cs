using NUnit.Framework;
using StoryQ;

namespace SnakesAndLadders.Test {

    public class US2Test: GameTestBase {
        [SetUp]
        public override void Setup() {
            base.Setup();
            TheGameIsStarted();
        }

        [Test]
        public void PlayerCanWinTheGame() {
			new Story("US 2 - Player Can Win the Game")
					.InOrderTo("I can gloat to everyone around")
					.AsA("Player")
					.IWant("to be able to win the game")
					    .WithScenario("UAT1 - Token on square 97 and move 3 squares won the game")
							    .Given(TheTokenIsSetOn_Square, 97)
							    .When(TheTokenIsMoved_NSpaces, 3)
							    .Then(TheTokenIsOn_Square, 100)
                                .And(ThePlayerHasWonTheGame)
                        .WithScenario("UAT2 - Token on square 97 and move 4 squares returns to 97 and not won the game")
                                .Given(TheTokenIsSetOn_Square, 97)
                                .When(TheTokenIsMoved_NSpaces, 4)
                                .Then(TheTokenIsOn_Square, 97)
                                .And(ThePlayerHasNotWonTheGame)
                    .Execute();
		}

        private void ThePlayerHasWonTheGame() {
            Assert.IsTrue(_game.IsTheWinner(_token));
        }

        private void ThePlayerHasNotWonTheGame() {
            Assert.IsFalse(_game.IsTheWinner(_token));
        }
    }
}