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
        public Cube(SceneManager mSceneMgr)
       {
        this.mSceneMgr = mSceneMgr;

       }

    public MeshPtr getCube(string cubeName)
    {

        manualObj = mSceneMgr.CreateManualObject("maualObjQuad");
        manualObj.Begin("void", RenderOperation.OperationTypes.OT_TRIANGLE_LIST);    // Starts filling the manual object as a triangle list

        // ---- Vertex Buffer ----
        manualObj.Position(new Vector3(10, 10, 0));               // --- Vertex 0 ---
            manualObj.TextureCoord(new Vector2(0,0));
        manualObj.Position(new Vector3(-10, 10, 0));              // --- Vertex 1 ---
            manualObj.TextureCoord(new Vector2(1,0));
        manualObj.Position(new Vector3(10, -10, 0));              // --- Vertex 2 ---
            manualObj.TextureCoord(new Vector2(0,1));
        manualObj.Position(new Vector3(-10, -10, 0));             // --- Vertex 3 ---
            manualObj.TextureCoord(new Vector2(1,1));
        manualObj.Position(new Vector3(10, 10, -20));             // --- Vertex 4 ---
            manualObj.TextureCoord(new Vector2(0, 0));
        manualObj.Position(new Vector3(10, -10, -20));            // --- Vertex 5 ---
            manualObj.TextureCoord(new Vector2(0, 1));
        manualObj.Position(new Vector3(-10, 10, -20));             // --- Vertex 6 ---
            manualObj.TextureCoord(new Vector2(0, 0));
        manualObj.Position(new Vector3(-10, -10, -20));            // --- Vertex 7 ---
            manualObj.TextureCoord(new Vector2(0, 1));

            // ---- Index Buffer ----
            // --- Triangle 0 ---
            manualObj.Index(0);
        manualObj.Index(1);
        manualObj.Index(2);

        // --- Triangle 1 ---
        manualObj.Index(1);
        manualObj.Index(3);
        manualObj.Index(2);

        // --- Triangle 2 ---
        manualObj.Index(4);
        manualObj.Index(0);
        manualObj.Index(5);

        // --- Triangle 3 ---
        manualObj.Index(0);
        manualObj.Index(2);
        manualObj.Index(5);

        // --- Triangle 4 ---
        manualObj.Index(1);
        manualObj.Index(6);
        manualObj.Index(3);

        // --- Triangle 5 ---
        manualObj.Index(6);
        manualObj.Index(7);
        manualObj.Index(3);

        // --- Triangle 6 ---
        manualObj.Index(4);
        manualObj.Index(6);
        manualObj.Index(0);

        // --- Triangle 7 ---
        manualObj.Index(6);
        manualObj.Index(1);
        manualObj.Index(0);

        // --- Triangle 8 ---
        manualObj.Index(6);
        manualObj.Index(4);
        manualObj.Index(7);

        // --- Triangle 9 ---
        manualObj.Index(4);
        manualObj.Index(5);
        manualObj.Index(7);

        // --- Triangle 10 ---
        manualObj.Index(2);
        manualObj.Index(3);
        manualObj.Index(5);

        // --- Triangle 11 ---
        manualObj.Index(3);
        manualObj.Index(7);
        manualObj.Index(5);

        manualObj.End();                                        // Closes the manual objet

        return manualObj.ConvertToMesh("Cube");                 // Prepares the data to be sent to the GPU
    }
        /// <summary>
        /// This method loads the Cube into the scene
        /// </summary>
        public void Load()
        {
            getCube("Cube");
            manualObjEntity = mSceneMgr.CreateEntity("Quad_Endtity", "Cube");    // Creates a new entity which contains the geometry
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
            using (MaterialPtr manualObjMat = MaterialManager.Singleton.Create("cube_mat", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME))// Creates a new Material
            {
                using (TextureUnitState manualObjTexture =
             manualObjMat.GetTechnique(0).GetPass(0).CreateTextureUnitState("cube.jpg")) // Sets the texture for the material
                {
                    manualObjEntity.SetMaterialName("cube_mat");                      // Applies the material to the entity
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
