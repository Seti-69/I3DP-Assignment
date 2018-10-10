using System;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class GemAmmo : Gem
    {
        ModelElement gemAmmo;
        ModelElement gemAmmoNode;
        float x;
        float y;
        float z;
        int index;
        // bool removeMe;
        /// <summary>
        /// Constructor inherited from base
        /// </summary>
        /// <param name="mSceneMgr"></param>
        /// <param name="score"></param>
        public GemAmmo(SceneManager mSceneMgr, Stat score,float x,float y,float z,int index):base (mSceneMgr,score)
        {
            this.mSceneMgr = mSceneMgr;
            this.score = score;
            increase = 50;
            this.x = x;
            this.y = y;
            this.z = z;
            this.index = index;
            LoadModel();
        }

        override protected void LoadModel()
        {
            // The link with to physics engine goes here
            // (ignore until week 8) ...
            //base.LoadModel();
            gemAmmo = new ModelElement(mSceneMgr, "Gem.mesh");
            gemAmmoNode = new ModelElement(mSceneMgr, "Phys_GemAmmo_"+index,0.01f,10f);
            gemAmmoNode.GameNode.Position = new Vector3(x, y, z);
            gemAmmoNode.PhysObj.Position = gemAmmoNode.GameNode.Position;
            gemAmmoNode.PhysObj.InvMass = 1f;
            gemAmmoNode.GameNode.AddChild(gemAmmo.GameNode);
            mSceneMgr.RootSceneNode.AddChild(gemAmmoNode.GameNode);
            gemAmmo.GameEntity.SetMaterialName("gemAmmo");
            this.GameNode = gemAmmoNode.GameNode;
            //Console.Write(x+","+y+","+z);
        }

        public override void Update(FrameEvent evt)
        {
            
            this.remove = IsCollidingWith("Phys_Player");
            Animate(evt);
            // Collision detection with the player goes here
            // (ignore until week 8) ...
            //Console.WriteLine(removeMe);
            //base.Update(evt);
        }

        public override void Animate(FrameEvent evt)
        {
            gameNode.Yaw(Mogre.Math.AngleUnitsToRadians(20) * evt.timeSinceLastFrame);
           // Console.Write(evt);
        }
        
        /// <summary>
        /// This method determine wether the gem is colliding with a named object  type
        /// </summary>
        /// <param name="objName">The name of the potential colliding object</param>
        /// <returns>True if a collision with the named object type happens, false otherwhise</returns>
        private bool IsCollidingWith(string objName)
        {
            bool isColliding = false;
            foreach (Contacts c in gemAmmoNode.PhysObj.CollisionList)
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
        /// This method disposes of  the GemAmmo pick up
        /// </summary>
        public override void Dispose()
        {
            gemAmmo.Dispose();
            gemAmmoNode.Dispose();
        }
        }
}
