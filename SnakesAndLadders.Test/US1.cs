using System.Linq;
using NUnit.Framework;
using StoryQ;

namespace SnakesAndLadders.Test {
    public class US1Test: GameTestBase {

        [Test]
        public void TokenCanMoveAcrossTheBoardTests() {
			new Story("US 1 - Token Can Move Across the Board")
					.InOrderTo("I can get closer to the goal")
					.AsA("Player")
					.IWant("to be able to move my token")
					    .WithScenario("UAT1 - At game start the token is on first square")
							    .Given(TheGameIsStarted)
							    .When(TheTokenIsPlacedOnTheBoard)
							    .Then(TheTokenIsOn_Square, 1)
                        .WithScenario("UAT2 - The token is moved from square 1 to 4")
                                .Given(TheTokenIsSetOn_Square, 1)
                                .When(TheTokenIsMoved_NSpaces, 3)
                                .Then(TheTokenIsOn_Square, 4)
                        .WithScenario("UAT3 - The token is moved from square 4 to 8")
                                .Given(TheTokenIsSetOn_Square, 1)
                                .When(TheTokenIsMoved_NSpaces, 3)
                                .And(ThenItIsMoved4Spaces)
                                .Then(TheTokenIsOn_Square, 8)
                    .Execute();
		}

        private void ThenItIsMoved4Spaces() {
            _game.Move(_token, 4);
        }

        private void TheTokenIsPlacedOnTheBoard() {
            Assert.AreEqual(1, _game.Tokens.Count(), string.Format(@"Game has {0} tokens", _game.Tokens.Count())); ;
        }
    }
}