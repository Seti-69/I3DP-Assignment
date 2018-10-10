using System;
using Mogre;

namespace RaceGame
{
    class Cannon:Gun
    {
        /// <summary>
        /// This class creates the mesh and nodes for a Cannon object.
        /// </summary>
        ModelElement cannon;
        ModelElement cannonNode;
        float x;
        float y;
        float z;
        //private SceneManager mSceneMgr;
        /// <summary>
        /// This is the constructor for a Cannon class
        /// </summary>
        /// <param name="mSceneMgr"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Cannon(SceneManager mSceneMgr,float  x,float y,float z)
        {
            this.mSceneMgr = mSceneMgr;
            this.x = x;
            this.y = y;
            this.z = z;
            LoadModel();
        }

        override protected void LoadModel()
        {
            cannon = new ModelElement(mSceneMgr, "CannonGun.mesh");
            cannon.GameEntity.SetMaterialName("CannonHull");
            cannonNode = new ModelElement(mSceneMgr, "Phys_Cannon_", 0.01f, 20f);
            cannonNode.GameNode.Position = new Vector3(x, y, z);
            //Console.Write(x+" "+y+" "+z);
            cannonNode.PhysObj.InvMass = 2f;
            cannonNode.PhysObj.Position = cannonNode.GameNode.Position;
            this.GameNode = cannonNode.GameNode;
            cannonNode.GameNode.AddChild(cannon.GameNode);
            mSceneMgr.RootSceneNode.AddChild(cannonNode.GameNode);
        }
        override public void ReloadAmmo()
        {

        }
        override public void Fire()
        {

        }    
        public void Update(FrameEvent evt)
        {
            Animate(evt);

        }
        public override void Animate(FrameEvent evt)
        {
            gameNode.Yaw(Mogre.Math.AngleUnitsToRadians(20) * evt.timeSinceLastFrame);
        }
        /// <summary>
        /// This method disposes of  the GemAmmo pick up
        /// </summary>
        public override void Dispose()
        {
            cannon.Dispose();
            cannonNode.Dispose();
        }
    }
}
