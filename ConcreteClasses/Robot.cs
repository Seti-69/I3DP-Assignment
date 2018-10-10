using System;
using Mogre;

namespace RaceGame
{
    /// <summary>
    /// This class describes the logic and the components of a character in the game
    /// </summary>
    class Robot:Character
    {
        //new  CharacterController controller;       // This field is to contains an instace of the CharacterController
        SceneManager mSceneMgr;
       // new  CharacterStats stats;                 // This field is to contain an instance of the CharacterStats
        //new CharacterModel model;                 // This field is to contain an instance of the CharacterModel
        //new bool isDead;                  // This field is to determine weter the caracter is dead

        /// <summary>
        /// Constructor for class Player.
        /// </summary>
        public Robot(SceneManager mSceneMgr,int index,float x,float y,float z)
        {
            this.mSceneMgr = mSceneMgr;
            model = new RobotModel(mSceneMgr,index,x,y,z);
            controller = new RobotController(this);
            stats = new RobotStats();
        }
        /// <summary>
        /// This overridden method is to implement the logic for shooting of the character
        /// </summary>
        override public void Shoot()
        {

        }

        /// <summary>
        /// This method is to update the character state
        /// </summary>
        /// <param name="evt">A frame event which can be used to tune the character update</param>
        override public void Update(FrameEvent evt)
        {
            
            model.Animate(evt);
            ((RobotModel)model).changeAnimationName(0);
            // controller.Forward= true;
            //((RobotModel)model).GameNode.LookAt(new Vector3(100f,100f,100f));
             controller.Update(evt);
        }
       
    }
}
