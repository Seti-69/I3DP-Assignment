using Mogre;
using Mogre.TutorialFramework;
using System.Collections.Generic;
using System;
using PhysicsEng;

namespace RaceGame
{
    class Manager : BaseApplication
    {
        private Player player;
        private Robot robot;
        private SceneNode cameraNode;
        private InputsManager inputsManager = InputsManager.Instance;
        private PlayerModel playerModel;
        private RobotModel robotModel;
        private CubeModel cube;
        private int cubeNum;
        private FloorModel floor;
        private GameInterface gameHMD;
        private List<CubeModel> obstacle_cube;
        private List<GemAmmo> gems_ammo;
        private List<GemAmmo> gems_ammo_remover;
        private List<GemHealth> gems_health;
        private List<GemHealth> gems_health_remover;
        private List<Robot> robots;
        private Cannon cannon;
        private GemAmmo ammo;
        private Stat ammoFill;
        private GemHealth health;
        private Stat healthFill;
        private Light headLight;
        private Environment environment;
        private int gemNum2;
        private int gemNum;
        private int robotNum;
        private bool gameOver;
        private bool timeOut;
        Physics physics;
        Timer time;
        /// <summary>
        /// This method starts the rendering loop
        /// </summary>
        public static void Main()
        {
            new Manager().Go();            
        }

        /// <summary>
        /// This method creates the initial scene
        /// </summary>
        protected override void CreateScene()
        {
            gameOver = false;
            physics = new Physics();
            // Lights
            environment = new Environment(mSceneMgr, mWindow);
            // Add cubes and floor
            obstacle_cube = new List<CubeModel>();
            int a;
            cubeNum = 200;
            Random rnd = new Random();
            float x;
            float z;
            for (a = 0; a < cubeNum; a++)
            {
                x = rnd.Next(-2000, 2000);
                z = rnd.Next(-2000, 2000);
                cube = new CubeModel(mSceneMgr, a,x,150,z);
                obstacle_cube.Add(cube);
                obstacle_cube[a].GameNode.SetPosition(x, 150, z);
                obstacle_cube[a].GameNode.Scale(new Vector3(1.25f, 1.25f, 1.25f));
            }
           
            // Add Camera
            cameraNode = mSceneMgr.CreateSceneNode();
            cameraNode.AttachObject(mCamera);
            // Add Player
            robots = new List<Robot>();
            robotNum = 60;
            player = new Player(mSceneMgr, robotNum);
            playerModel = player.Model as PlayerModel;
            playerModel.AddChild(cameraNode);
            player.Model = playerModel;
            player.Model.GameNode.Translate(new Vector3(0,80,0));
            inputsManager.PlayerController = (PlayerController)player.Controller;
            // Add Robots
            
            for (a = 0; a < robotNum; a++)
            {
                x = rnd.Next(-2000, 2000);
                z = rnd.Next(-2000, 2000);
                robot = new Robot(mSceneMgr,a,x,0,z);
                robotModel = robot.Model as RobotModel;
                robot.Model = robotModel;
                robots.Add(robot);
                robots[a].Model.GameNode.SetPosition(x,180,z);
            }
                
            // HUD
            gameHMD = new GameInterface(mSceneMgr, mWindow, player.Stats);
            gameHMD.Time = new Timer();
           
            //Collectables
            x = rnd.Next(-200, 200);
            z = rnd.Next(-200, 200);
            cannon = new Cannon(mSceneMgr,x,100,z);

            cannon.GameNode.SetPosition(x, 180, z);
            cannon.GameNode.Scale(new Vector3(2f, 2f, 2f));
            ammoFill = new Stat();
            ammoFill.InitValue(20);
            gems_ammo = new List<GemAmmo>();
            gems_ammo_remover = new List<GemAmmo>();
            gemNum = 100;
            for (a = 0; a < gemNum; a++)
            {
                x = rnd.Next(-2000, 2000);
                z = rnd.Next(-2000, 2000);
                ammo = new GemAmmo(mSceneMgr, ammoFill, x, 50, z,a);
                gems_ammo.Add(ammo);
                gems_ammo[a].GameNode.SetPosition(x, 50, z);
                gems_ammo[a].GameNode.Scale(new Vector3(5f, 5f, 5f));
            }
            gems_health = new List<GemHealth>();
            gems_health_remover = new List<GemHealth>();
            gemNum2 = 20;
            for (a = 0; a < gemNum2; a++)
            {
                x = rnd.Next(-2000, 2000);
                z = rnd.Next(-2000, 2000);
                health = new GemHealth(mSceneMgr, healthFill, x, 50, z, a);
                gems_health.Add(health);
                gems_health[a].GameNode.SetPosition(x, 50, z);
                gems_health[a].GameNode.Scale(new Vector3(5f, 5f, 5f));
            }
            healthFill = new Stat();
            healthFill.InitValue(100);
            //health = new GemHealth(mSceneMgr, ammoFill);
           // health.GameNode.Translate(new Vector3(-200, 8, -200));
            //health.GameNode.Scale(new Vector3(5f, 5f, 5f));
            physics.StartSimTmer();


        }

        /// <summary>
        /// This method create a new camera
        /// </summary>
        protected override void CreateCamera()
        {
           //base.CreateCamera();
            mCamera = mSceneMgr.CreateCamera("PlayerCam");
            mCamera.Position = new Vector3(0, 50, -100);
            mCamera.LookAt(new Vector3(0, 50, 0));
            mCamera.NearClipDistance = 5;
            mCamera.FarClipDistance = 1000;
            mCamera.FOVy = new Degree(70);

            mCameraMan = new CameraMan(mCamera);
            mCameraMan.Freeze = true;
        }

        /// <summary>
        /// This method create a new viewport
        /// </summary>
        protected override void CreateViewports()
        {
          // base.CreateViewports();
            Viewport viewport = mWindow.AddViewport(mCamera);
            viewport.BackgroundColour = ColourValue.Black;
            mCamera.AspectRatio = viewport.ActualWidth / viewport.ActualHeight;
        }

        /// <summary>
        /// This method updates the scene after a frame has finished rendering
        /// </summary>
        /// <param name="evt"></param>
        protected override void UpdateScene(FrameEvent evt)
        {
            if (gameOver || timeOut) { return; }
                //if (!gameOver) { inputsManager.ProcessInput(evt); }
            Vector3 temp = playerModel.Position;
            temp.y = temp.y + 50; // move the camera target higher.
            mCamera.LookAt(temp);
            player.Update(evt);
            //temp.y += 20;
            for (var a = 0; a < robotNum; a++)
            {
                robots[a].Model.GameNode.LookAt(temp, Node.TransformSpace.TS_WORLD, Vector3.UNIT_Z);
                robots[a].Update(evt);
            }
            gameHMD.Update(evt);
            
            foreach (GemAmmo gemAmmo in gems_ammo)                  /// Checks for collision with Ammo Gems
            {
                gemAmmo.Update(evt);
                if (gemAmmo.RemoveMe)
                {
                    gems_ammo_remover.Add(gemAmmo);
                    //Console.WriteLine(gemAmmo.RemoveMe);
                }
                //
            }

            foreach (GemAmmo gemAmmo in gems_ammo_remover)
            {
                //Physics.RemovePhysObj(gemAmmo.PhysObj);
                //gemAmmo.PhysObj = null;
                ((PlayerStats)player.Stats).Score.Increase(50);
                //Console.WriteLine(((PlayerStats)player.Stats).Score.Value);
                gems_ammo.Remove(gemAmmo);

                gemAmmo.GameNode.RemoveAllChildren();
                gemAmmo.GameNode.Parent.RemoveChild(gemAmmo.GameNode);
                gemAmmo.GameNode.DetachAllObjects();
                gemAmmo.Dispose();

                

            }
            gems_ammo_remover.Clear();

            foreach (GemHealth gemhealth in gems_health)                  /// Checks for collision with Health Gems
            {
                gemhealth.Update(evt);
                if (gemhealth.RemoveMe)
                {
                    gems_health_remover.Add(gemhealth);
                    //Console.WriteLine(gemAmmo.RemoveMe);
                }
                //
            }

            foreach (GemHealth gemhealth in gems_health_remover)
            {
                //Physics.RemovePhysObj(gemAmmo.PhysObj);
                //gemAmmo.PhysObj = null;
                ((PlayerStats)player.Stats).Score.Increase(500);
                //Console.WriteLine(((PlayerStats)player.Stats).Score.Value);
                gems_health.Remove(gemhealth);

                gemhealth.GameNode.RemoveAllChildren();
                gemhealth.GameNode.Parent.RemoveChild(gemhealth.GameNode);
                gemhealth.GameNode.DetachAllObjects();
                gemhealth.Dispose();
                
            }
            gems_health_remover.Clear();

            bool collide;
            collide = ((PlayerModel)player.Model).IsCollidingWith("Phys_Cannon_");
            if (collide)
            {
                Console.WriteLine("Player hit Cannon");
                ((PlayerStats)player.Stats).Score.Increase(1000);
                cannon.Dispose();
                //Gun cannonTemp = new Cannon(mSceneMgr, 0, 0, 0);
               // player.PlayerArmoury.CollectedGuns.Add(cannonTemp);
            }
            if (cannon!=null) { cannon.Update(evt); }
           // health.Update(evt);
            gameHMD.Update(evt);
            if (gameHMD.Time.Milliseconds > 120000) { gameHMD.MaxTime(); timeOut = true; }   // checks for end of level //
            // temp = playerModel.Position;
            // temp.y = temp.y + 100;
            // light.Position= temp;
            physics.UpdatePhysics(0.01f);
            if (player.Stats.Lives.Value <= 0) { Console.WriteLine("GAME OVER");gameEnd(evt);
            }
            base.UpdateScene(evt);
        }

        /// <summary>
        /// This method creates a frame listener to handle events before, during or after the frame rendering
        /// </summary>
        protected override void CreateFrameListeners()
        {
           
            mRoot.FrameRenderingQueued +=
            new FrameListener.FrameRenderingQueuedHandler(inputsManager.ProcessInput);
            base.CreateFrameListeners();
        }

        /// <summary>
        /// This method initilize the inputs reading from keyboard adn mouse
        /// </summary>
        protected override void InitializeInput()
        {
            
            int windowHandle;
            mWindow.GetCustomAttribute("WINDOW", out windowHandle);
            inputsManager.InitInput(ref windowHandle);
            base.InitializeInput();
        }
        private void gameEnd (FrameEvent evt)
        {
            //DestroyScene();
            gameOver = true;
            time = new Timer();
            gameHMD.GameOver();
            gameHMD.Update(evt);
            if (time.Milliseconds > 3000)
            {
                
                //time.Reset();
                Go();
            }
            //gameEnd( evt);
        }
        /// <summary>
        /// This method disposes of all noes and entities in the scene
        /// </summary>
        protected override void DestroyScene()
        {
            
            cameraNode.DetachAllObjects();
            cameraNode.Dispose();
            player.Model.Dispose();
            if (robot != null) { robot.Model.Dispose(); }
            //cube.Dispose();
            gameHMD.Dispose();
            int a;
            for (a = 0; a < cubeNum; a++)
            {
                obstacle_cube[a].Dispose();
            }
            foreach (GemAmmo gemAmmo in gems_ammo)
            {
                gemAmmo.Dispose();
            }
            foreach (GemHealth gemHealth in gems_health)
            {
                gemHealth.Dispose();
            }
            physics.Dispose();
            base.DestroyScene();
        }
    }
}