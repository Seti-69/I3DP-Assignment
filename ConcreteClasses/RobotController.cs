using System;
using Mogre;

namespace RaceGame
{
    /// <summary>
    /// This class allows to implement a robot controller. 
    /// This allows to controll the robot by AI
    /// </summary>
    class RobotController:CharacterController
    {
        
        Vector3 move;
        Timer time;
        float maxTime = 6000f;
        ///<summary>
        ///Constructor for class PlayerController
        /// </summary>
        public RobotController(Character robot)
        {
            speed = 3f;
            character = robot;
            forward = true;
            backward = false;
            left = false;
            right = false;
            move = Vector3.ZERO;
            time = new Timer();
        }
        /// <summary>
        /// This override method updates the state of the Player
        /// </summary>
        /// <param name="evt">A frame event that can be used for the update of the character</param>
        public override  void Update(FrameEvent evt)
        {
            //if (time.Milliseconds > maxTime)
            //{
            //    forward = false;
            //    time.Reset();
            //   // character.changeAnimationName(0);
            //}
            MovementsControl(evt);
           // MouseControls(evt);
           // ShootingControls(evt);
           // Console.WriteLine(evt);
        }
        /// <summary>
        /// This method checks for movement
        /// </summary>
        private void MovementsControl(FrameEvent evt)
        {
           // Vector3 move;
           // 
            

            if (shoot)
            {

            }
            if (forward)
            {
                //Console.Write(move);
                move += character.Model.Forward;
            }
            if (backward)
            {
                move -= character.Model.Forward;
            }
            if (left)
            {
                move += character.Model.Left;

            }
            if (right)
            {
                move -= character.Model.Left;
            }
            if (up)
            {
                move += character.Model.Up;
            }
            if (down)
            {
                move -= character.Model.Up;
            }
            move=move.NormalisedCopy*speed;
            character.Model.Move(move);
            //angles += new Vector3(1,0,1);
            character.Model.Rotate(new Quaternion(0, angles));
            // Console.WriteLine(move);
        }
        /// <summary>
        /// This method 
        /// </summary>
        private void MouseControls(FrameEvent evt)
        {
            //Vector3 angle = Vector3.ZERO;
            //angle.y += -mMouse.MouseState.Y.rel;
            //Console.WriteLine(angles.y);
            //character.Model.Rotate(new Quaternion(0f, angle));
        }


        /// <summary>
        /// This method 
        /// </summary>
        private void ShootingControls(FrameEvent evt)
        {

        }
    }
}
