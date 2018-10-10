using System;
using Mogre;

using PhysicsEng;

namespace RaceGame
{
    /// <summary>
    /// This class describes and initialize an element of the character model
    /// </summary>
    class ModelElement:MovableElement
    {

        
        //protected PhysObj physObj;
       // public PhysObj PhysObj
       // {
       //     get { return physObj; }
            
        //}
        float radius;
        //public float Radius
        //{
        //    get { return radius; }
        //    set
        //    {
        //        radius = value;
        //    }
        //}
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference to the scene manager</param>
        /// <param name="modelName">The name of the .mesh file which contains the geometry of the model</param>
        public ModelElement(SceneManager mSceneMgr, string modelName = "", float Im=0, float rad=0)
        {
            // YOUR CODE FOR INITIALIZE THE GAMENODE GOES HERE
           
            gameNode = mSceneMgr.CreateSceneNode();
            if (modelName != "" )
            {
                //YOUR CODE FOR INITIALIZE AND ATTACH THE GAMEENTITY TO THE NODE GOES HERE
                if (modelName.Substring(0,5)!="Phys_")
                {
                    gameEntity = mSceneMgr.CreateEntity(modelName); //load .mesh file
                    gameEntity.GetMesh().BuildEdgeList();
                    gameNode.AttachObject(gameEntity);
                }else
                {
                    // Add physics //
                    
                     this.physObj = new PhysObj(rad, (modelName), Im, 0.7f, 0.3f);
                     physObj.SceneNode = gameNode;
                     physObj.Position = gameNode.Position;
                   // gameNode.Position += rad * Vector3.UNIT_Y;
                    //physObj.Position -= rad * Vector3.UNIT_Y;
                    //physObj.InvMass = 100f;
                    physObj.AddForceToList(new WeightForce(physObj.InvMass)); 
                     Physics.AddPhysObj(physObj);
                    // controlNode.AddChild(gameNode);
                    //Console.Write( modelName+" mass: " +physObj.InvMass);
                }


            }
           // mSceneMgr.RootSceneNode.AddChild(gameNode); 
        }

        /// <summary>
        /// This method moves the model element in the specified direction
        /// </summary>
        /// <param name="direction">A direction in which to move the model element</param>
        public override void Move(Vector3 direction)
        {
            // YOUR CODE FOR MOVING THE GAMENODE GOES HERE
            this.gameNode.Translate(direction);
        }

        /// <summary>
        /// This modeto rotate the model element as described by the quaternion given as parameter in the
        /// specified transformation space
        /// </summary>
        /// <param name="quaternion">The quaternion which describes the rotation axis and angle</param>
        /// <param name="transformSpace">The space in which to perfrom the rotation, local by default</param>
        public override void Rotate(Quaternion quaternion, 
                        Node.TransformSpace transformSpace = Node.TransformSpace.TS_LOCAL)
        {
            // YOUR CODE FOR ROTATING THE GAMENODE GOES HERE
        }

        /// <summary>
        /// This method adds a child to the node of this model element
        /// </summary>
        /// <param name="childNode"></param>
        public void AddChild(SceneNode childNode)
        {
            //YOUR NODE FOR ATTACHING A CHILDNODE TO THE GAMENODE GOES HERE
            //gameNode.AddChild(childNode);
        }
    }
}
