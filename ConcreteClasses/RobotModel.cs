using System;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    /// <summary>
    /// This class implements a robot
    /// </summary>
    class RobotModel : CharacterModel
    {
        Radian angle;           // Angle for the mesh rotation
        Vector3 direction;      // Direction of motion of the mesh for a single frame
        float radius;           // Radius of the circular trajectory of the mesh

        float maxTime = 2000f;        // Time when the animation have to be changed
        Timer time;                         // Timer for animation changes
        AnimationState animationState;      // Animation state, retrieves and store an animation from an Entity
        bool animationChanged;              // Flag which tells when the mesh animation has changed
        string animationName;               // Name of the animation to use
        int animType;               // Number animation to use
        int index;
        float x;
        float y;
        float z;
       // float distance;
        public string AnimationName
        {
            set
            {
                HasAnimationChanged(value);
                if (IsValidAnimationName(value))
                    animationName = value;
                else
                    animationName = "Idle";
            }
        }
        //RobotController robotControl;
        //SceneManager mSceneMgr;     // A reference to the scene manager
        private ModelElement robot;
        private ModelElement robotNode;
        private Light light;
        /// <summary>
        /// Constructor for class RobotModel
        /// </summary>
        /// <param name="mSceneMgr">A reference to the scene manager</param>
        public RobotModel(SceneManager mSceneMgr,int index,float x,float y,float z)
        {
            this.mSceneMgr = mSceneMgr;
            this.index = index;
            this.x = x;
            this.y = y;
            this.z = z;
            LoadModelElements();
            AssembleModel();
            AnimationSetup();
        }
        //private bool isCollide;
        //public bool IsCollide
        //{
        //    get
        //    {
        //        return isCollide;
        //    }
        //}
        /// <summary>
        /// This method loads the mesh and attaches it to a node 
        /// </summary>
        protected override void LoadModelElements()
        {
            robot = new ModelElement(mSceneMgr,"robot.mesh");
            robotNode = new ModelElement(mSceneMgr,"Phys_Robot"+index, 0.01f, 40.0f);
            robotNode.GameNode.Position = new Vector3(x, y, z);
            robotNode.PhysObj.Position = robotNode.GameNode.Position;
            robot.GameNode.Position -= 40* Vector3.UNIT_Y; ;
            //robotNode.PhysObj.Translate(new Vector3(0, -30, 0));
             robot.GameNode.Yaw(-1.4f);
            //robot.GameNode.Roll(-0.785f);
            //robot.GameNode.Pitch(0.3f);
            robotNode.PhysObj.InvMass =2f;
            this.GameNode = robotNode.GameNode;
        }

        /// <summary>
        /// This method assembles the model and attaches it it the root
        /// </summary>
        protected override void AssembleModel()
        {
            robotNode.GameNode.AddChild(robot.GameNode);
            mSceneMgr.RootSceneNode.AddChild(robotNode.GameNode);
            robot.GameEntity.SetMaterialName("Robot");
            //light = mSceneMgr.CreateLight();                                            // Set an instance of a light;

            //light.DiffuseColour = new ColourValue(1f, 0.3f, 0.3f);                                      // Sets the color of the light
            //light.Position = new Vector3(0, 100, 0);
            //light.Type = Light.LightTypes.LT_POINT;                                     // Sets the light to be a point light

            //float range = 1000f;                                                         // Sets the light range
            //float constantAttenuation = 0f;                                              // Sets the constant attenuation of the light [0, 1]
            //float linearAttenuation = 0.03f;                                                // Sets the linear attenuation of the light [0, 1]
            //float quadraticAttenuation = 0.0f;                                       // Sets the quadratic  attenuation of the light [0, 1]

            //light.SetAttenuation(range, constantAttenuation,
            //          linearAttenuation, quadraticAttenuation); // Not applicable to directional ligths
            
            //this.GameNode.AttachObject(light);
        }
        /// <summary>
        /// This property returns the position of the top gameNode.
        /// </summary>
        public Vector3 Position
        {
            get { return robotNode.GameNode.Position; }
        }
        /// <summary>
        /// This method is called every frame and contains a call to AnimateMesh
        /// </summary>
        /// <param name="evt"></param>
        public override void Animate(FrameEvent evt) {
           // if (animType == 0) { Motion(evt); }
           // if (animType == 2) { Rotate(evt);
           //}
          // bool collide = IsCollidingWith("Phys_Player");
          //  if (collide) {
                //Console.WriteLine("Robot hit Player");
                //((PlayerStats)player.Stats).Score.Increase(50);
           // }
            AnimateMesh(evt);
        }
        /// <summary>
        /// 
        /// </summary>
        private void AnimationSetup()
        {
            radius = 1.5f;
            direction = Vector3.ZERO;
            angle = 5f;
            time = new Timer();
            PrintAnimationNames();
            animationChanged = false;
            animationName = "Walk";
            animType = 1;
           // Random rnd = new Random();   // Randomise the start position of the robot
           // int x=rnd.Next(-300,300);
           // int z= rnd.Next(1, 300); ;
            //robotNode.GameNode.Translate( new Vector3(x,0,z));
           
            LoadAnimation();
        }
        
        /// <summary>
        /// This method sets the animationChanged field to true whenever the animation name changes
        /// </summary>
        /// <param name="newName"> The new animation name </param>
        private void HasAnimationChanged(string newName)
        {
            if (newName != animationName)
                animationChanged = true;
        }

        /// <summary>
        /// This method prints on the console the list of animation tags
        /// </summary>
        private void PrintAnimationNames()
        {
            AnimationStateSet animStateSet = robot.GameEntity.AllAnimationStates;     // Get the set of animation states in the Entity
            AnimationStateIterator animIterator = animStateSet.GetAnimationStateIterator();  // Iterates through the animation states

            while (animIterator.MoveNext())                                       // Gets the next animation state in the set
            {
               // Console.WriteLine(animIterator.CurrentKey);                      // Print out the animation name in the current key
            }
        }

        /// <summary>
        /// This method deternimes whether the name inserted is in the list of valid animation names
        /// </summary>
        /// <param name="newName">An animation name</param>
        /// <returns></returns>
        private bool IsValidAnimationName(string newName)
        {
            bool nameFound = false;

            AnimationStateSet animStateSet = robot.GameEntity.AllAnimationStates;
            AnimationStateIterator animIterator = animStateSet.GetAnimationStateIterator();

            while (animIterator.MoveNext() && !nameFound)
            {
                if (newName == animIterator.CurrentKey)
                {
                    nameFound = true;
                }
            }

            return nameFound;
        }

        /// <summary>
        /// This method changes the animation name 
        /// </summary>
        /// <param name="animId"></param>
        public void changeAnimationName(int animId)
        {
            switch (animId)       // 
            {
                case 0:
                    {
                        AnimationName = "Walk";                 // I use the porperty here instead of the field to determine whether I am actualy changing the animation
                        break;
                    }
                case 1:
                    {
                        AnimationName = "Shoot";
                        break;
                    }
                case 2:
                    {
                        AnimationName = "Idle";
                        break;
                    }
                case 3:
                    {
                        AnimationName = "Slump";
                        break;
                    }
                case 4:
                    {
                        AnimationName = "Die";
                        break;
                    }
            }
        }

        /// <summary>
        /// This method loads the animation from the animation name
        /// </summary>
        private void LoadAnimation()
        {
            animationState = robot.GameEntity.GetAnimationState(animationName);
            animationState.Loop = true;
            animationState.Enabled = true;
        }

        /// <summary>
        /// This method puts the mesh in motion
        /// </summary>
        /// <param name="evt">A frame event which can be used to tune the animation timings</param>
        private void AnimateMesh(FrameEvent evt)
        {
            if (time.Milliseconds > maxTime)
            {
                AnimSet();
                time.Reset();
            }

            if (animationChanged)
            {
                LoadAnimation();
                animationChanged = false;
            }
           
            animationState.AddTime(evt.timeSinceLastFrame);
        }
        /// <summary>
        /// This method this method makes the mesh move in circle
        /// </summary>
        /// <param name="evt">A frame event which can be used to tune the animation timings</param>
        /// 
        //private void Motion(FrameEvent evt)
        //{
        //     //angle += (Radian)evt.timeSinceLastFrame;
        //   // distance += 1;
        //     direction = radius * new Vector3(Mogre.Math.Cos(angle), 0, Mogre.Math.Sin(angle));
        //   // direction = new Vector3(0,0,1);
        //    robotNode.GameNode.Translate(direction);
            
        //}
        //public  void Rotate(FrameEvent evt)
        //{
        //    angle += (Radian)evt.timeSinceLastFrame;
        //     //direction = radius * new Vector3(Mogre.Math.Cos(angle), 0, Mogre.Math.Sin(angle));
        //    //robotNode.GameNode.Translate(direction);
        //    //robotNode.GameNode.Yaw(-evt.timeSinceLastFrame);

        //}
        public override void DisposeModel()
        {
            robot.Dispose();
            robotNode.Dispose();
        }
        public void AnimSet()
        {
            if(animType == 0) {
                 animType = 2; changeAnimationName(animType); maxTime = 1000;
            }
            else {  animType = 0; changeAnimationName(animType); maxTime =3000; }
        }
        /// <summary>
        /// This method attaches a child to the gameNode.
        /// </summary>
        /// <param child="SceneNode">A reference to the objects gameNode</param>
        public void AddChild(SceneNode child)
        {
            robotNode.GameNode.AddChild(child);
        }
     
    }
}
