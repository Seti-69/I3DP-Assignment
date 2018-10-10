using System;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class PlayerModel:CharacterModel
    {
        /// <summary>
        /// This class initialises all the components of a player model
        /// </summary>
       // SceneManager mSceneMgr;
        private ModelElement bodyMesh;
        private ModelElement wheelmesh;
        private ModelElement powerMesh;
        private ModelElement modelNode;
        private ModelElement hullNode;
        private ModelElement wheelNode;
        private ModelElement gunNode;
        private Light headLight;
        /// <summary>
        /// Constructor for Class PlayerModel
        /// </summary>
        /// <param name="mSceneMgr">A reference to the scene manager</param>
        public PlayerModel(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            LoadModelElements();
            AssembleModel();
            
        }
        /// <summary>
        /// This method loads all the model elements
        /// </summary>
        protected override void LoadModelElements()
        {
            bodyMesh = new ModelElement(mSceneMgr, "Main.mesh");
            wheelmesh = new ModelElement(mSceneMgr, "Sphere.mesh");
            powerMesh = new ModelElement(mSceneMgr, "PowerCells.mesh");
            hullNode = new ModelElement(mSceneMgr, "");
            modelNode = new ModelElement(mSceneMgr,"Phys_Player",0.01f,10f);
            //modelNode.PhysObj.Radius = 10f;
            wheelNode = new ModelElement(mSceneMgr, "");
            gunNode = new ModelElement(mSceneMgr, "");
            this.GameNode = modelNode.GameNode;
            
        }
        /// <summary>
        /// This method attaches all the model elements in the proper heirarchy
        /// </summary>
        protected override void AssembleModel()
        {

            modelNode.GameNode.AddChild(hullNode.GameNode);
            hullNode.GameNode.AddChild(bodyMesh.GameNode);
            hullNode.GameNode.AddChild(powerMesh.GameNode);
            wheelNode.GameNode.AddChild(wheelmesh.GameNode);
            hullNode.GameNode.AddChild(gunNode.GameNode);
            // modelNode.GameNode.AttachObject(light);
            modelNode.PhysObj.Position += (new Vector3(0,15,0));
            //modelNode.PhysObj.Restitution =0.99f;
            modelNode.PhysObj.InvMass = 1.0f;
             modelNode.GameNode.AddChild(wheelNode.GameNode);
            mSceneMgr.RootSceneNode.AddChild(modelNode.GameNode);
            modelNode.GameNode.Translate(new Vector3(0, 100, 0));
            //modelNode.GameNode.Position += 20*Vector3.UNIT_Y;
            headLight = mSceneMgr.CreateLight();                                          // Set an instance of a light;
            headLight.DiffuseColour = new ColourValue(0.4f, 0.5f, 0.4f);                  // Sets the color of the light
            headLight.Position = new Vector3(0, 100, 0);
            headLight.Type = Light.LightTypes.LT_POINT;                                   // Sets the light to be a point light
            float range = 10000f;                                                         // Sets the light range
            float constantAttenuation = 0f;                                               // Sets the constant attenuation of the light [0, 1]
            float linearAttenuation = 0.006f;                                             // Sets the linear attenuation of the light [0, 1]
            float quadraticAttenuation = 0.0f;                                            // Sets the quadratic  attenuation of the light [0, 1]

            headLight.SetAttenuation(range, constantAttenuation,
            linearAttenuation, quadraticAttenuation);               // Not applicable to directional ligths
            this.GameNode.AttachObject(headLight);
        }
        /// <summary>
        /// This property returns the position of the top gameNode.
        /// </summary>
        public Vector3 Position
        {
            get { return modelNode.GameNode.Position; }
        }
        /// <summary>
        /// This method attaches a child to the gameNode.
        /// </summary>
        /// <param child="SceneNode">A reference to the objects gameNode</param>
        public void AddChild(SceneNode child)
        {
            modelNode.GameNode.AddChild(child);
        }
        /// <summary>
        /// This method gets the scene node for the top model element
        /// </summary>
        /// <return>A reference to the top element</param>
       // public ModelElement getTopNode()
       // {
       //    return modelNode;
       // }
        /// <summary>
        /// This method overrides the Move method and moves the palyer
        /// </summary>
        /// <param direction="Vector3">New movement xyz</param>
        public override void Move(Vector3 direction)
        {
             modelNode.Move(direction);
           
        }

        /// <summary>
        /// This method rotate the player accordingly  with the given angles
        /// </summary>
        /// <param name="angles">The angles by which rotate the player along each main axis</param>
        public void Rotate(Vector3 angles)
        {
            modelNode.Rotate(new Quaternion(0f, angles));
        }
        /// <summary>
        /// This override method disposes of all the model elements
        /// </summary>
        public override void DisposeModel()
        {
            bodyMesh.Dispose();
            wheelmesh.Dispose();
            powerMesh.Dispose();
            wheelNode.Dispose();
            hullNode.Dispose();
            modelNode.Dispose();
           // Console.WriteLine("Dispose");
        }
        public void AttachGun(Gun gun)
        {
            if (gunNode.GameNode.NumChildren()!=0)
            {
                gunNode.GameNode.RemoveAllChildren();
            }
            gunNode.GameNode.AddChild(gun.GameNode);
        }
        /// <summary>
        /// Rotates the player model wheel when moving.
        /// </summary>
        /// <param name="speed"></param>
        public override void RotateWheel(float speed)
        {
            wheelmesh.GameNode.Pitch(speed/50);
        }
        /// <summary>
        /// Checks all collisions in the player collision list
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        public bool IsCollidingWith(string objName)
        {
            bool isColliding = false;
            foreach (Contacts c in modelNode.PhysObj.CollisionList)
            {
                if (c.colliderObj.ID == objName || c.collidingObj.ID == objName)
                {
                    isColliding = true;
                    break;
                }
            }
            return isColliding;
        }
        /// <summary>
        ///  Checks collision with a robot
        /// </summary>
        /// <param name="evt"></param>
        public override void Animate(FrameEvent evt)
        {
            //bool collide = IsCollidingWith("Phys_Robot");
            //if (collide)
           // {
            //    Console.WriteLine("Robot hit Player");
            //    ((PlayerStats)player.Stats).Score.Increase(50);
            //}
        }
    }
    }
