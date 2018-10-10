using System;
using Mogre;
using PhysicsEng;
namespace RaceGame
{
    class GemHealth : Gem
    {
        ModelElement gemHealth;
        ModelElement gemHealthNode;
        float x;
        float y;
        float z;
        int index;
        /// <summary>
        /// Constructor inherited from base
        /// </summary>
        /// <param name="mSceneMgr"></param>
        /// <param name="score"></param>
        public GemHealth(SceneManager mSceneMgr, Stat score,float x,float y,float z,int index):base (mSceneMgr,score)
        {
            this.mSceneMgr = mSceneMgr;
            this.score = score;
            increase = 100;
            this.x = x;
            this.y = y;
            this.z = z;
            this.index = index;
            LoadModel();
        }

        override protected void LoadModel()
        {
            // The link with to phisics engine goes here
            // (ignore until week 8) ...
           // base.LoadModel();
            gemHealth = new ModelElement(mSceneMgr, "Gem.mesh");
            gemHealthNode = new ModelElement(mSceneMgr, "Phys_gemHealth"+index, 0.01f, 10f);
            gemHealthNode.GameNode.Position = new Vector3(x, y, z);
            gemHealthNode.PhysObj.Position = gemHealthNode.GameNode.Position;
            gemHealthNode.PhysObj.InvMass = 1f;
            gemHealthNode.GameNode.AddChild(gemHealth.GameNode);
            mSceneMgr.RootSceneNode.AddChild(gemHealthNode.GameNode);
            gemHealth.GameEntity.SetMaterialName("gemHealth");
            this.GameNode = gemHealthNode.GameNode;
        }

        public override void Update(FrameEvent evt)
        {

            this.remove = IsCollidingWith("Phys_Player");
            Animate(evt);
            // Collision detection with the player goes here
            // (ignore until week 8) ...

            base.Update(evt);
        }

        public override void Animate(FrameEvent evt)
        {
            gameNode.Yaw(Mogre.Math.AngleUnitsToRadians(20) * evt.timeSinceLastFrame);
        }
        /// <summary>
        /// This method determine wether the gem is colliding with a named object  type
        /// </summary>
        /// <param name="objName">The name of the potential colliding object</param>
        /// <returns>True if a collision with the named object type happens, false otherwhise</returns>
        private bool IsCollidingWith(string objName)
        {
            bool isColliding = false;
            foreach (Contacts c in gemHealthNode.PhysObj.CollisionList)
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
            gemHealth.Dispose();
            gemHealthNode.Dispose();
        }
    }
}
