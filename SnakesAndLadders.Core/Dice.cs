using System;

namespace SnakesAndLadders.Core {
    /// <summary>
    /// Simple Dice implementation using Random class
    /// </summary>
    public class Dice : IDice {
        private Random _random;
        private int _value;
        private int _sides;
        /// <summary>
        /// Initializes a instance of a dice with 6 sides by default
        /// </summary>
        public Dice():this(6) {  
        }
        /// <summary>
        /// Initializes a instance of a dice using specified sides
        /// </summary>
        /// <param name="sides">Number of dice sides</param>
        public Dice(int sides) {
            _sides = sides;
            _random = new Random();
        }
        /// <summary>
        /// Current dice roll value
        /// </summary>
        public int Value => _value;

        /// <summary>
        /// Rolling the dice
        /// </summary>
        public int Roll() {
            return _value = _random.Next(1, _sides + 1);
        }
    }
}