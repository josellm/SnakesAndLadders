using System;
using NUnit.Framework;
using SnakesAndLadders.Core;
using StoryQ;

namespace SnakesAndLadders.Test {

    public class US3Test: GameTestBase {
        private IDice _dice;

        [SetUp]
        public override void Setup() {
            base.Setup();
            _dice = new Dice();
        }

        [Test]
        public void MovesAreDeterminedByDiceRolls() {
			new Story("US 3 - Moves Are Determined By Dice Rolls")
					.InOrderTo("there is an element of chance in the game")
					.AsA("Player")
					.IWant("to move my token based on the roll of a die")
					    .WithScenario("UAT1 - Dice rolls results are from 1 to 6")
							    .Given(TheGameIsStarted)
							    .When(ThePlayerRollsADice)
							    .Then(TheResultShouldBeBetween1and6Inclusive)
                        .WithScenario("UAT2 - When the player is on square 1 and rolls a 4 the token is on square 5")
                                .Given(ThePlayerRollsA4)
                                .When(TheyMoveTheirToken)
                                .Then(TheTokenIsOn_Square, 5)
                    .Execute();
		}

        private void TheyMoveTheirToken() {
            _game.Move(_token, _dice.Value);
        }

        private void ThePlayerRollsA4() {
            while (_dice.Roll() != 4);
        }

        private void TheResultShouldBeBetween1and6Inclusive() {
            Assert.GreaterOrEqual(_dice.Value, 1);
            Assert.LessOrEqual(_dice.Value, 6);
        }

        private void ThePlayerRollsADice() {
            _dice.Roll();
        }
    }
}