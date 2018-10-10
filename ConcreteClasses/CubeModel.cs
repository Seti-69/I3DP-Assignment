using System;
using Mogre;

namespace RaceGame
{
    class CubeModel:CharacterModel
    {

        //private SceneManager mSceneMgr;
        private ManualObject manualObj;
        private Entity manualObjEntity;
        private SceneNode manualObjNode;
        private ModelElement cubeMesh;
        //private ModelElement cubeNode;
        private int count;
        private float x;
        private float y;
        private float z;
        private ModelElement controlNode;
        /// <summary>
        /// Constructor for Cube model.
        /// </summary>
        /// <param name="mSceneMgr"></param>
        public CubeModel(SceneManager mSceneMgr,int index,float x,float y,float z)
       {
            this.mSceneMgr = mSceneMgr;
            this.x = x;
            this.y = y;
            this.z = z;
            count = index;
            getCube("Cube"+count);
            LoadModelElements();
            AssembleModel();
        }

        /// <summary>
        /// This method loads the mesh and the node
        /// </summary>
        protected override void LoadModelElements()
        {
            // Console.WriteLine(count);
            
            cubeMesh = new ModelElement(mSceneMgr, "Cube"+count);
            cubeMesh.GameEntity.SetMaterialName("Cube01");
            //cubeMesh.GameEntity.GetMesh().BuildEdgeList();
            controlNode = new ModelElement(mSceneMgr, "Phys_Cube" + count, 0.01f,60.0f);
            //controlNode.PhysObj.Radius = 50f;
            //cubeMesh.GameNode.Position -= (controlNode.PhysObj.Radius) * Vector3.UNIT_Y;
            controlNode.GameNode.Position=new Vector3(x, y, z);
            controlNode.PhysObj.Position = controlNode.GameNode.Position;
            //controlNode.GameNode.SetPosition(new Vector3(0,100, 0));
            cubeMesh.GameNode.Position+=(new Vector3(0,-15,35));
            //
            this.GameNode = controlNode.GameNode;
        }
        /// <summary>
        /// This method assembles  the Cube elements to the scene
        /// </summary>
        protected override void AssembleModel()
        {


            controlNode.PhysObj.InvMass = 1.5f;
            controlNode.GameNode.AddChild(cubeMesh.GameNode);
            mSceneMgr.RootSceneNode.AddChild(controlNode.GameNode);
            // controlNode.GameNode.SetPosition(x, y, z);

        }
        /// <summary>
        /// This method contructs the mesh for the cube
        /// </summary>
        /// <param name="cubeName"></param>
        /// <returns>cubeMesh object</returns>
        public MeshPtr getCube(string cubeName)
    {
           // Console.Write("getCube: "+count);
        manualObj = mSceneMgr.CreateManualObject("manualObjQuad"+count);
        manualObj.Begin("void", RenderOperation.OperationTypes.OT_TRIANGLE_LIST);    // Starts filling the manual object as a triangle list

        // ---- Vertex Buffer ----
        manualObj.Position(new Vector3(30, 30, 0));               // --- Vertex 0 ---
            manualObj.TextureCoord(new Vector2(0,0));
            manualObj.Normal(1, 1, 1);
            manualObj.Position(new Vector3(-30, 30, 0));              // --- Vertex 1 ---
            manualObj.TextureCoord(new Vector2(1,0));
            manualObj.Normal(1, 1, 1);
            manualObj.Position(new Vector3(30, -30, 0));              // --- Vertex 2 ---
            manualObj.TextureCoord(new Vector2(0,1));
            manualObj.Normal(1, 1, 1);
            manualObj.Position(new Vector3(-30, -30, 0));             // --- Vertex 3 ---
            manualObj.TextureCoord(new Vector2(1,1));
            manualObj.Normal(1, 1, 1);
            manualObj.Position(new Vector3(30, 30, -60));             // --- Vertex 4 ---
            manualObj.TextureCoord(new Vector2(0, 0));
            manualObj.Normal(1, 1, 1);
            manualObj.Position(new Vector3(30, -30, -60));            // --- Vertex 5 ---
            manualObj.TextureCoord(new Vector2(1,0));
            manualObj.Normal(1, 1, 1);
            manualObj.Position(new Vector3(-30, 30, -60));             // --- Vertex 6 ---
            manualObj.TextureCoord(new Vector2(0, 1));
            manualObj.Normal(1, 1, 1);
            manualObj.Position(new Vector3(-30, -30, -60));            // --- Vertex 7 ---
            manualObj.TextureCoord(new Vector2(1,1));
            manualObj.Normal(1, 1, 1);

            // ---- Index Buffer ----
            // --- Triangle 0 ---
            manualObj.Index(0);
        manualObj.Index(1);
        manualObj.Index(2);

        // --- Triangle 1 ---
        manualObj.Index(1);
        manualObj.Index(3);
        manualObj.Index(2);

       
        // --- Triangle 8 ---
        manualObj.Index(4);
        manualObj.Index(5);
        manualObj.Index(6);

        // --- Triangle 9 ---
        manualObj.Index(5);
        manualObj.Index(7);
        manualObj.Index(6);

          
        manualObj.Position(new Vector3(30, 30,-60));               // --- Vertex 8 ---
        manualObj.TextureCoord(new Vector2(1, 0));
            manualObj.Normal(1, 1, 1);
            manualObj.Position(new Vector3(30, 30, 0));              // --- Vertex 9 ---
        manualObj.TextureCoord(new Vector2(0, 0));
            manualObj.Normal(1, 1, 1);
            manualObj.Position(new Vector3(30, -30, -60));              // --- Vertex 10 ---
        manualObj.TextureCoord(new Vector2(1, 1));
            manualObj.Normal(1, 1, 1);
            manualObj.Position(new Vector3(30, -30,0));             // --- Vertex 11 ---
        manualObj.TextureCoord(new Vector2(0, 1));
            manualObj.Normal(1, 1, 1);
            manualObj.Position(new Vector3(-30, 30, 0));             // --- Vertex 12 ---
        manualObj.TextureCoord(new Vector2(0, 0));
            manualObj.Normal(1, 1, 1);
            manualObj.Position(new Vector3(-30, 30, -60));            // --- Vertex 13 ---
        manualObj.TextureCoord(new Vector2(0, 1));
            manualObj.Normal(1, 1, 1);
            manualObj.Position(new Vector3(-30,-30, 0));             // ---Vertex 14 ---
        manualObj.TextureCoord(new Vector2(1, 0));
            manualObj.Normal(1, 1, 1);
            manualObj.Position(new Vector3(-30, -30, -60));            // ---Vertex 15 ---
        manualObj.TextureCoord(new Vector2(1, 1));
            manualObj.Normal(1, 1, 1);

            // ---Triangle 12-- -
            manualObj.Index(8);
        manualObj.Index(9);
        manualObj.Index(10);

        //  ---Triangle 13-- -
        manualObj.Index(9);
        manualObj.Index(11);
        manualObj.Index(10);

        // ---Triangle 14-- -
        manualObj.Index(12);
        manualObj.Index(13);
        manualObj.Index(14);

        //  ---Triangle 15-- -
        manualObj.Index(13);
        manualObj.Index(15);
        manualObj.Index(14);

            manualObj.Position(new Vector3(30, 30,-60));               // --- Vertex 16 ---
            manualObj.TextureCoord(new Vector2(1, 0));
            manualObj.Normal(1, 1, 1);
            manualObj.Position(new Vector3(-30, 30,-60));              // --- Vertex 17 ---
            manualObj.TextureCoord(new Vector2(0, 0));
            manualObj.Normal(1, 1, 1);
            manualObj.Position(new Vector3(30, 30,0));              // --- Vertex 18 ---
            manualObj.TextureCoord(new Vector2(1, 1));
            manualObj.Normal(1, 1, 1);
            manualObj.Position(new Vector3(-30, 30,0));             // --- Vertex 19 ---
            manualObj.TextureCoord(new Vector2(0, 1));
            manualObj.Normal(1, 1, 1);
            manualObj.Position(new Vector3(30, -30,0));             // --- Vertex 20 ---
            manualObj.TextureCoord(new Vector2(0, 0));
            manualObj.Normal(1, 1, 1);
            manualObj.Position(new Vector3(-30, -30,0));            // --- Vertex 21 ---
            manualObj.TextureCoord(new Vector2(0, 1));
            manualObj.Normal(1, 1, 1);
            manualObj.Position(new Vector3(30, -30,-60));             // ---Vertex 22 ---
            manualObj.TextureCoord(new Vector2(1, 0));
            manualObj.Normal(1, 1, 1);
            manualObj.Position(new Vector3(-30, -30,-60));           // ---Vertex 23 ---
            manualObj.TextureCoord(new Vector2(1, 1));
            manualObj.Normal(1, 1, 1);

            // ---Triangle 12-- -
            manualObj.Index(16);
            manualObj.Index(17);
            manualObj.Index(18);

            //  ---Triangle 13-- -
            manualObj.Index(17);
            manualObj.Index(19);
            manualObj.Index(18);

            // ---Triangle 14-- -
            manualObj.Index(20);
            manualObj.Index(21);
            manualObj.Index(22);

            //  ---Triangle 15-- -
            manualObj.Index(21);
            manualObj.Index(23);
            manualObj.Index(22);


            manualObj.End();                                        // Closes the manual objet

        return manualObj.ConvertToMesh(cubeName);                 // Prepares the data to be sent to the GPU
    }
        /// <summary>
        /// This method loads the Cube into the scene
        /// </summary>
        //public void Load()
        //{
            
        //    manualObjEntity = mSceneMgr.CreateEntity("Quad_Endtity", "Cube");    // Creates a new entity which contains the geometry
        //    manualObjNode = mSceneMgr.CreateSceneNode("Quad_Node");              // Creates a new node for the scene graph
        //    manualObjNode.AttachObject(manualObjEntity);                         // Attaches the entity (geometry) to the node
        //    mSceneMgr.RootSceneNode.AddChild(manualObjNode);                     // Adds the node as child of the root of the scene graph

        //    // ManualObjMat();                 // Calls method to create material
            
        //}
        /// <summary>
        /// This method applies a texture map to the Cube
        /// </summary>
        //private void ManualObjMat()
        //{
        //    using (MaterialPtr manualObjMat = MaterialManager.Singleton.Create("cube_mat", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME))// Creates a new Material
        //    {
        //        using (TextureUnitState manualObjTexture =
        //     manualObjMat.GetTechnique(0).GetPass(0).CreateTextureUnitState("cube.jpg")) // Sets the texture for the material
        //        {
        //            manualObjEntity.SetMaterialName("cube_mat");                      // Applies the material to the entity
        //        }
        //    }
        //}

        /// <summary>
        /// This method disposes of  the Cube
        /// </summary>
        public override void Dispose()
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
            cubeMesh.Dispose();
            controlNode.Dispose();
        }
    }
}
