using System;
using Mogre;


namespace RaceGame
{
    class WallModel
    {

        private SceneManager mSceneMgr;

        // private ManualObject manualObj;
        //private Entity manualObjEntity2;
        // private SceneNode manualObjNode;
        Entity groundEntity;
        SceneNode groundNode;
        Entity groundEntity2;
        SceneNode groundNode2;
        Entity groundEntity3;
        SceneNode groundNode3;
        Entity groundEntity4;
        SceneNode groundNode4;
        //private ModelElement floor;
        int groundWidth =4000;
        int groundHeight = 700;
        int grounXSegs = 100;
        int grounZSegs = 7;
        int uTiles = 40;
        int vTiles = 7;
        //private ModelElement floorPhys;
        public Plane plane;
        public Plane plane2;
        public Plane plane3;
        public Plane plane4;
        public Plane Plane
        {
            get { return plane; }
        }
        /// <summary>
        /// Constructor for Wall
        /// </summary>
        /// <param name="mSceneMgr"></param>
        public WallModel(SceneManager mSceneMgr)
       {
        this.mSceneMgr = mSceneMgr;
        Load();
        
        }

        public void Load()
        {
            
            plane = new Plane(Vector3.UNIT_X, 0);
            MeshPtr groundMeshPtr = MeshManager.Singleton.CreatePlane("wall", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane, groundWidth, groundHeight, grounXSegs, grounZSegs, true, 1, uTiles, vTiles, Vector3.UNIT_Y);
            groundEntity = mSceneMgr.CreateEntity("wall");
            groundNode = mSceneMgr.CreateSceneNode();
            groundNode.AttachObject(groundEntity);
            mSceneMgr.RootSceneNode.AddChild(groundNode);
            groundEntity.SetMaterialName("Floor");
            groundEntity.GetMesh().BuildEdgeList();
            groundNode.Position = new Vector3(-2000, 350, 0);

            plane2 = new Plane(Vector3.UNIT_X, 0);
            MeshPtr groundMeshPtr2 = MeshManager.Singleton.CreatePlane("wall1", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane, groundWidth, groundHeight, grounXSegs, grounZSegs, true, 1, uTiles, vTiles, Vector3.UNIT_Y);
            groundEntity = mSceneMgr.CreateEntity("wall1");
            groundNode = mSceneMgr.CreateSceneNode();
            groundNode.AttachObject(groundEntity);
            mSceneMgr.RootSceneNode.AddChild(groundNode);
            groundEntity.SetMaterialName("Floor");
            groundEntity.GetMesh().BuildEdgeList();
            groundNode.Position = new Vector3(2000, 350, 0);
            groundNode.Yaw(3.14f);

            plane3 = new Plane(Vector3.UNIT_X, 0);
            MeshPtr groundMeshPtr3 = MeshManager.Singleton.CreatePlane("wall2", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane, groundWidth, groundHeight, grounXSegs, grounZSegs, true, 1, uTiles, vTiles, Vector3.UNIT_Y);
            groundEntity = mSceneMgr.CreateEntity("wall2");
            groundNode = mSceneMgr.CreateSceneNode();
            groundNode.AttachObject(groundEntity);
            mSceneMgr.RootSceneNode.AddChild(groundNode);
            groundEntity.SetMaterialName("Floor");
            groundEntity.GetMesh().BuildEdgeList();
            groundNode.Position = new Vector3(0, 350, 1996);
            groundNode.Yaw(1.57f);

            plane4 = new Plane(Vector3.UNIT_X, 0);
            MeshPtr groundMeshPtr4 = MeshManager.Singleton.CreatePlane("wall3", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane, groundWidth, groundHeight, grounXSegs, grounZSegs, true, 1, uTiles, vTiles, Vector3.UNIT_Y);
            groundEntity = mSceneMgr.CreateEntity("wall3");
            groundNode = mSceneMgr.CreateSceneNode();
            groundNode.AttachObject(groundEntity);
            mSceneMgr.RootSceneNode.AddChild(groundNode);
            groundEntity.SetMaterialName("Floor");
            groundEntity.GetMesh().BuildEdgeList();
            groundNode.Position = new Vector3(0, 350, -1996);
            groundNode.Yaw(-1.57f);
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
