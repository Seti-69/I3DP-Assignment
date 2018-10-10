using System;
using Mogre;

namespace RaceGame
{
    /// <summary>
    /// This class allows to implement a player controller. 
    /// This allows to controll the camera either by keyboard and mouse or by AI
    /// </summary>
    class PlayerController:CharacterController
    {

        Vector3 move;
        bool reverse;
        float acc;
        float acc2;
        bool jump;
        public bool Jump
        {
            set { jump = value; }
            get { return jump; }
        }
        ///<summary>
        ///Constructor for class PlayerController
        /// </summary>
        public PlayerController(Character player)
        {
            speed = 4f;
            character = player;
            forward = false;
            backward = false;
            left = false;
            right = false;
            move = Vector3.ZERO;
            reverse = false;
            acc = 0.0f;
            acc2 = 0.01f;
        }
        /// <summary>
        /// This override method updates the state of the Player
        /// </summary>
        /// <param name="evt">A frame event that can be used for the update of the character</param>
        public override  void Update(FrameEvent evt)
        {
            MovementsControl(evt);
            MouseControls(evt);
            ShootingControls(evt);
           // Console.WriteLine(evt);
        }
        /// <summary>
        /// This method checks for movement
        /// </summary>
        private void MovementsControl(FrameEvent evt)
        {

           // move = Vector3.ZERO;
           // character.Model.RotateWheel(0);
            if (shoot)
            {

            }
            if(jump)
            {
                Console.WriteLine("Player Jump");
            }
            if (forward)
            {

                move += character.Model.Forward;
                //character.Model.RotateWheel(speed);
                acc += 0.03f;
              //  reverse = false;
            }
            else
            {

            }
            if (backward)
            {
                 move -= character.Model.Forward;
                //character.Model.RotateWheel(-speed);
                acc -= 0.03f;
               // if (reverse)
               // {
                //    acc -= 0.3f;
                    //move += character.Model.Forward;
               // }
                
            }

            if (left)
            {
               // if(!forward && !backward) { return; }
               // if (!reverse)
               // {
                //    move += character.Model.Left;
               // }
               // else
               // {
                    move += character.Model.Left;
               // }
            }
            if (right)
            {
                //if (!forward && !backward) { return; }
                //if (!reverse)
                //{
                //    move -= character.Model.Left;
                //}
                //else
                //{
                    move -= character.Model.Left;
                //}
            }
            if (up && character.Model.GameNode.Position.y < 200)
            {
                
                move += character.Model.Up;
            }
            if (down)
            {
                move -= character.Model.Up;
            }
            if (!forward && acc>0)
            {
                acc -= 0.03f;
                if (acc < 0) { acc = 0; }
                move += character.Model.Forward;
                // character.Model.RotateWheel(acc);

            }
            if (!backward && acc<0)
            {

                acc += 0.03f;

                if (acc >0) { acc = 0; }
                move -= character.Model.Forward;
                

            }
            // 
            // if (!backward && !forward && !reverse && acc < 0) { acc = 0; }
            // if (acc >= 0f) { reverse = false; }else { reverse = true; } 
            float temp=0;
            if (acc < 0){ temp=acc *-1;reverse = true; }else { temp = acc;reverse = false; }
            move =move.NormalisedCopy * (speed*temp);
            character.Model.Move(move);
            if (character.Model.GameNode.Position.y < 50) {  }
            character.Model.RotateWheel(acc * speed);
            //Console.WriteLine(reverse);
            if (character.Model.GameNode.Position.x > 1990) { character.Model.Move(-move); }
            if (character.Model.GameNode.Position.x < -1990) { character.Model.Move(-move); }
            if (character.Model.GameNode.Position.z > 1990) { character.Model.Move(-move); }
            if (character.Model.GameNode.Position.z < -1990) { character.Model.Move(-move); }
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
