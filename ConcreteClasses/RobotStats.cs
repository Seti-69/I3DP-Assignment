using System;

namespace RaceGame
{
    /// <summary>
    /// This class sets some basic statistics for the Player in the game
    /// </summary>
    class RobotStats:CharacterStats
    {

        
        new Stat health;      // This field represents the health of the character
        new Stat shield;      // This field represents the shield of the character
        Stat score;
        public RobotStats()
        {
            InitStats();
        }

        /// <summary>
        /// This method initializes the stats objects, initial values are to be set in derived classes
        /// </summary>
        override protected void InitStats() 
        {
            health = new Stat();
            shield = new Stat();
            shield.InitValue(100);
            health.InitValue(100);
        }
    }
}
