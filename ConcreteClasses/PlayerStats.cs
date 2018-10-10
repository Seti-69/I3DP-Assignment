using System;

namespace RaceGame
{
    /// <summary>
    /// This class sets some basic statistics for the Player in the game
    /// </summary>
    class PlayerStats:CharacterStats
    {

        
        
        Stat score;             // score field and property
        /// <summary>
        /// Read only. This property gives back the score stat of the character.
        /// </summary>
        public Stat Score
        {
            get { return score; }
        }

        public PlayerStats()
        {
            InitStats();
        }

        /// <summary>
        /// This method initializes the stats objects, initial values are to be set in derived classes
        /// </summary>
        override protected void InitStats() 
        {
            lives = new Stat();
            health = new Stat();
            shield = new Stat();
            score = new Stat();
            score.InitValue(0);
            shield.InitValue(100);
            health.InitValue(100);
            lives.InitValue(5);
        }
    }
}
