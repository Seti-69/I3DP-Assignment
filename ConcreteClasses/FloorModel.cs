using System;
using Mogre;


namespace RaceGame
{
    class FloorModel
    {

        private SceneManager mSceneMgr;
        
        Entity groundEntity;
        SceneNode groundNode;
        //private ModelElement floor;
        int groundWidth = 4000;
        int groundHeight = 4000;
        int grounXSegs = 100;
        int grounZSegs = 100;
        int uTiles = 40;
        int vTiles = 40;
        //private ModelElement floorPhys;
        public Plane plane;
        public Plane Plane
        {
            get { return plane; }
        }
        /// <summary>
        /// Constructor for Floor
        /// </summary>
        /// <param name="mSceneMgr"></param>
        public FloorModel(SceneManager mSceneMgr)
       {
        this.mSceneMgr = mSceneMgr;
        Load();
        
        }

   
        public void Load()
        {
            
            plane = new Plane(Vector3.UNIT_Y, 0);
            MeshPtr groundMeshPtr = MeshManager.Singleton.CreatePlane("ground", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane, groundWidth, groundHeight, grounXSegs, grounZSegs, true, 1, uTiles, vTiles, Vector3.UNIT_Z);
            groundEntity = mSceneMgr.CreateEntity("ground");
            groundNode = mSceneMgr.CreateSceneNode();
            groundNode.AttachObject(groundEntity);
            mSceneMgr.RootSceneNode.AddChild(groundNode);
            groundEntity.SetMaterialName("Floor");
            groundEntity.GetMesh().BuildEdgeList();
            groundNode.Position = new Vector3(0, 0, 0);
            
        }
        /// <summary>
        /// This method disposes of  the Cube
        /// </summary>
        public void Dispose()
        {
           
            groundNode.DetachAllObjects();
            groundNode.Parent.RemoveChild(groundNode);
            groundNode.Dispose();
            groundEntity.Dispose();
        }
    }
}
