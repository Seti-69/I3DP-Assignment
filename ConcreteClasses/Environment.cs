using System;
using Mogre;
using PhysicsEng;
namespace RaceGame
{
    /// <summary>
    /// This class implements the environment
    /// </summary>
    class Environment
    {
        Light light;                        // This field will contain a reference of a light

        #region As in Demo 10
        SceneManager mSceneMgr;             // This field will contain a reference of the scene manages
        RenderWindow mWindow;               // This field will contain a reference to the rendering window
        FloorModel floor;               // This field will contain an instance of the ground object
        WallModel wall;
        //Ground ground;                      //This field will contain an instance of the wall object 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference to the scene manager</param>
        /// /// <param name="mWindow">A reference to the scene window</param>
        public Environment(SceneManager mSceneMgr, RenderWindow mWindow)
        {
            this.mSceneMgr = mSceneMgr;
            this.mWindow = mWindow;

            Load();                                 // This method loads  the environment
        }
        #endregion
        
        /// <summary>
        /// This method loads the environment
        /// </summary>
        private void Load()
        {
            SetLights();
            floor = new FloorModel(mSceneMgr);
            Physics.AddBoundary(floor.Plane);
            wall = new WallModel(mSceneMgr);
            //Physics.AddBoundary(wall.Plane);
            #region Part3
            //SetShadows();
            #endregion

            SetFog();
            #region As in Demo 10
              SetSky();
            //   ground = new Ground(mSceneMgr);
            #endregion
        }

        #region As in Demo 10
        /// <summary>
        /// This method dispose of any object instanciated in this class
        /// </summary>
        public void Dispose()
        {
            floor.Dispose();
        }
        
        /// <summary>
        /// This method sets the sky in the environment
        /// </summary>
        private void SetSky()
        {
            //mSceneMgr.SetSkyDome(true, "Sky", 1f, 20, 700, true);

            Plane sky = new Plane(Vector3.NEGATIVE_UNIT_Y, -200);
            mSceneMgr.SetSkyPlane(true, sky, "Sky", 20, 15, true, 0.8f, 100, 100, ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);

            //mSceneMgr.SetSkyBox(true, "SkyBox", 10, true);
        }

        /// <summary>
        /// This method sets the fog in the environment
        /// </summary>
        private void SetFog()
        {
            ColourValue fadeColour = new ColourValue(0.05f, 0.0f, 0.4f);
            mSceneMgr.SetFog(FogMode.FOG_LINEAR, fadeColour, 0.1f, 100, 1000);

            //mSceneMgr.SetFog(FogMode.FOG_EXP, fadeColour, 0.001f);
           // mSceneMgr.SetFog(FogMode.FOG_EXP2, fadeColour, 0.0015f);
            
            mWindow.GetViewport(0).BackgroundColour = fadeColour;
        }
        #endregion

        /// <summary>
        /// This method sets the lights in the environment
        /// </summary>
        private void SetLights()
        {
            mSceneMgr.AmbientLight = new ColourValue(0.2f, 0.2f, 0.2f);                 // Set the ambient light in the scene
            //light = mSceneMgr.CreateLight();                                            // Set an instance of a light;

            //light.DiffuseColour = ColourValue.Red;                                      // Sets the color of the light
            //light.Position = new Vector3(0, 100, 0);                                    // Sets the position of the light

            ////light.Type = Light.LightTypes.LT_DIRECTIONAL;                               // Sets the light to be a directional Light

            ////light.Type = Light.LightTypes.LT_SPOTLIGHT;                                 // Sets the light to be a spot light
            ////light.SetSpotlightRange(Mogre.Math.PI / 6, Mogre.Math.PI / 4, 0.001f);      // Sets the spot light parametes

            ////light.Direction = Vector3.NEGATIVE_UNIT_Y;                                  // Sets the light direction

            //light.Type = Light.LightTypes.LT_POINT;                                     // Sets the light to be a point light

            //float range = 1000;                                                         // Sets the light range
            //float constantAttenuation = 0;                                              // Sets the constant attenuation of the light [0, 1]
            //float linearAttenuation = 0;                                                // Sets the linear attenuation of the light [0, 1]
            //float quadraticAttenuation = 0.0001f;                                       // Sets the quadratic  attenuation of the light [0, 1]

            //light.SetAttenuation(range, constantAttenuation, 
            //          linearAttenuation, quadraticAttenuation); // Not applicable to directional ligths
        }

        #region Part 3
        private void SetShadows()
        {
            //mSceneMgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_MODULATIVE;
            mSceneMgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_ADDITIVE;
        }
        #endregion
    }
}
