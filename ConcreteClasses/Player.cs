using System;
using Mogre;

namespace RaceGame
{
    /// <summary>
    /// This class describes the logic and the components of a character in the game
    /// </summary>
    class Player:Character
    {
        SceneManager mSceneMgr;
        Armoury playerArmoury;
        bool gotCannon;
        public bool GotCannon
        {
            get { return gotCannon; }
        }
        public Armoury PlayerArmoury
        {
            get { return playerArmoury; }
        }
        int robotNum;
        /// <summary>
        /// Constructor for class Player.
        /// </summary>
        public Player(SceneManager mSceneMgr,int robotNum)
        {
            this.mSceneMgr = mSceneMgr;
            model = new PlayerModel(mSceneMgr);
            controller = new PlayerController(this);
            stats = new PlayerStats();
            playerArmoury = new Armoury();
            this.robotNum = robotNum;
            //playerArmoury.SwapGun(0);
        }
        /// <summary>
        /// This overridden method is to implement the logic for shooting of the character
        /// </summary>
        override public void Shoot()
        {
            playerArmoury.ActiveGun.Fire();
        }

        /// <summary>
        /// This method is to update the character state
        /// </summary>
        /// <param name="evt">A frame event which can be used to tune the character update</param>
        override public void Update(FrameEvent evt)
        {
            controller.Update(evt);
            model.Animate(evt);
            bool collide;
            for (int a = 0; a < robotNum; a++)
            { 
                collide = ((PlayerModel)model).IsCollidingWith("Phys_Robot" + a);

            if (collide)
            {
                //Console.WriteLine("Robot hit Player");
                stats.Health.Decrease(1);
            }
            }
            
            if (playerArmoury.GunChanged)
            {
                ((PlayerModel)model).AttachGun(playerArmoury.ActiveGun);
                playerArmoury.GunChanged = false;
            }
        }
       // public CharacterModel getPlayerModel()
       // {
            
       //     return model;
       // }
    }
}
