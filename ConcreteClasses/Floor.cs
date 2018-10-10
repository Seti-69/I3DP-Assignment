using System;
using Mogre;


namespace RaceGame
{
    class Cube
    {

        private SceneManager mSceneMgr;

        private ManualObject manualObj;
        private Entity manualObjEntity;
        private SceneNode manualObjNode;


        public Floor(SceneManager mSceneMgr)
       {
        this.mSceneMgr = mSceneMgr;

       }

    public MeshPtr getCube(string cubeName)
    {

        manualObj = mSceneMgr.CreateManualObject("maualObjQuad");
        manualObj.Begin("void", RenderOperation.OperationTypes.OT_TRIANGLE_LIST);    // Starts filling the manual object as a triangle list

        // ---- Vertex Buffer ----
        manualObj.Position(new Vector3(-1000, 0,1000));               // --- Vertex 0 ---
            manualObj.TextureCoord(new Vector2(0,0));
        manualObj.Position(new Vector3(1000, 0,1000));              // --- Vertex 1 ---
            manualObj.TextureCoord(new Vector2(1,0));
        manualObj.Position(new Vector3(1000, 0, -1000));              // --- Vertex 2 ---
            manualObj.TextureCoord(new Vector2(0,1));
        manualObj.Position(new Vector3(-1000, 0,-1000 0));             // --- Vertex 3 ---
            manualObj.TextureCoord(new Vector2(1,1));

        // ---- Index Buffer ----
        // --- Triangle 0 ---
        manualObj.Index(0);
        manualObj.Index(1);
        manualObj.Index(2);

        // --- Triangle 1 ---
        manualObj.Index(1);
        manualObj.Index(3);
        manualObj.Index(2);

       
        manualObj.End();                                        // Closes the manual objet

        return manualObj.ConvertToMesh("Floor");                 // Prepares the data to be sent to the GPU
    }
        /// <summary>
        /// This method loads the Cube into the scene
        /// </summary>
        public void Load()
        {
            getCube("Floor");
            manualObjEntity = mSceneMgr.CreateEntity("Quad_Endtity", "Floor");    // Creates a new entity which contains the geometry
            manualObjNode = mSceneMgr.CreateSceneNode("Quad_Node");              // Creates a new node for the scene graph
            manualObjNode.AttachObject(manualObjEntity);                         // Attaches the entity (geometry) to the node
            mSceneMgr.RootSceneNode.AddChild(manualObjNode);                     // Adds the node as child of the root of the scene graph

            ManualObjMat();
        }
        /// <summary>
        /// This method applies a texture map to the Cube
        /// </summary>
        private void ManualObjMat()
        {
            using (MaterialPtr manualObjMat = MaterialManager.Singleton.Create("floor_mat", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME))// Creates a new Material
            {
                using (TextureUnitState manualObjTexture =
             manualObjMat.GetTechnique(0).GetPass(0).CreateTextureUnitState("floor.jpg")) // Sets the texture for the material
                {
                    manualObjEntity.SetMaterialName("floor_mat");                      // Applies the material to the entity
                }
            }
        }

        /// <summary>
        /// This method disposes of  the Cube
        /// </summary>
        public void Dispose()
        {
            if (manualObjNode != null)
            {
                manualObjNode.Parent.RemoveChild(manualObjNode);
                manualObjNode.DetachAllObjects();
                manualObjNode.Dispose();
                manualObjNode = null;
            }
            if (manualObjEntity != null)
            {
                manualObjEntity.Dispose();
                manualObjEntity = null;
            }
        }
    }
}
