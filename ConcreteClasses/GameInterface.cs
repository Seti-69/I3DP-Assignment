using System;
using System.Collections.Generic;
using Mogre;

namespace RaceGame
{
    /// <summary>
    /// This class implements an example of interface
    /// </summary>
    class GameInterface:HMD     // Game interface inherits form the Head Mounted Dispaly (HMD) class
    {
        private PanelOverlayElement panel;
        private OverlayElement scoreText;
        private OverlayElement levelTime;
        private OverlayElement gameOver;
        private OverlayElement timer;
        private OverlayElement healthBar;
        private OverlayElement shieldBar;
        private Overlay overlay3D;
        private Entity lifeEntity;
        private List<SceneNode> lives;
        private Timer time;
        public Timer Time
        {
            get { return time; }
            set { time = value; }
        }
        private string gmText = "GAME OVER SUCKER!";
        private string lTText = "Well Done. You Survived";
        private float hRatio;
        private float sRatio;
        private string score = "SCORE: ";
        //CharacterStats characterStats;    
        /// <summary>
        /// Read only. This property returns the player stats.
        /// </summary>
        public CharacterStats CharacterStats
        {
            get { return characterStats; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference of a scene manager</param>
        /// <param name="playerStats">A reference to a character stats</param>
        public GameInterface(SceneManager mSceneMgr, 
            RenderWindow mWindow, CharacterStats playerStats)
            : base(mSceneMgr, mWindow, playerStats)  // this calls the constructor of the parent class
        {

            characterStats = playerStats;
            Load("GameInterface");
        }

        /// <summary>
        /// This method initializes the element of the interface
        /// </summary>
        /// <param name="name"> A name to pass to generate the overaly </param>
        protected override void Load(string name)
        {
            base.Load(name);

            lives = new List<SceneNode>();

            healthBar = OverlayManager.Singleton.GetOverlayElement("HealthBar");
            hRatio = healthBar.Width / (float)characterStats.Health.Value;
            
            shieldBar = OverlayManager.Singleton.GetOverlayElement("ShieldBar");
            sRatio = shieldBar.Width / (float)characterStats.Shield.Value;

            scoreText = OverlayManager.Singleton.GetOverlayElement("ScoreText");
            scoreText.Caption = score;
            scoreText.Left = mWindow.Width * 0.5f;
            timer = OverlayManager.Singleton.GetOverlayElement("TimerText");
            timer.Left = mWindow.Width -100f;
            timer.Top = 60f;
            //timer.Caption= convertTime(time.Milliseconds);
            panel = (PanelOverlayElement) OverlayManager.Singleton.GetOverlayElement("GreenBackground");
            panel.Width = mWindow.Width;
            LoadOverlay3D();
        }

        /// <summary>
        /// This method initalize a 3D overlay
        /// </summary>
        private void LoadOverlay3D()
        {
            overlay3D = OverlayManager.Singleton.Create("3DOverlay");
            overlay3D.ZOrder = 15000;

            CreateHearts();

            overlay3D.Show();
        }

        /// <summary>
        /// This method generate as many hearts as the number of lives left
        /// </summary>
        private void CreateHearts()
        {
            for (int i = 0; i < characterStats.Lives.Value; i++)
                AddHeart(i);
        }
        
        /// <summary>
        /// This method add an heart to the 3D overlay
        /// </summary>
        /// <param name="n"> A numeric tag</param>
        private void AddHeart(int n)
        {
            SceneNode livesNode = CreateHeart(n);
            lives.Add(livesNode);
            overlay3D.Add3D(livesNode);
        }

        /// <summary>
        /// This method remove from the 3D overlay and destries the passed scene node
        /// </summary>
        /// <param name="life"></param>
        private void RemoveAndDestroyLife(SceneNode life)
        {
            overlay3D.Remove3D(life);
            lives.Remove(life);
            MovableObject heart = life.GetAttachedObject(0);
            life.DetachAllObjects();
            life.Dispose();
            heart.Dispose();
        }

        /// <summary>
        /// This method initializes the heart node and entity
        /// </summary>
        /// <param name="n"> A numeric tag used to determine the heart postion on sceen </param>
        /// <returns></returns>
        private SceneNode CreateHeart(int n)
        {
            lifeEntity = mSceneMgr.CreateEntity("Heart.mesh");
            lifeEntity.SetMaterialName("HeartHMD");
            SceneNode livesNode;
            livesNode = new SceneNode(mSceneMgr);
            livesNode.AttachObject(lifeEntity);
            livesNode.Scale(new Vector3(0.3f, 0.3f, 0.3f));
            livesNode.Position = new Vector3(5f, 5f, -8) - n * 0.6f * Vector3.UNIT_X; ;
            livesNode.SetVisible(true);
            return livesNode;
        }

        /// <summary>
        /// This method converts milliseconds in to minutes and second format mm:ss
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private string convertTime(float time)
        {
            string convTime;
            float secs = time / 1000f;
            int min = (int)(secs / 60);
            secs = (int) secs % 60f;
            if (secs < 10)
                convTime = min + ":0" + secs;
            else
                convTime = min + ":" + secs;
            return convTime;
        }

        /// <summary>
        /// This method updates the interface
        /// </summary>
        /// <param name="evt"></param>
        public override void Update(FrameEvent evt)
        {
            //base.Update(evt);
            
            Animate(evt);
            if (characterStats.Health.Value <= 0) { Console.WriteLine("Life Lost!"); characterStats.Health.InitValue(100); characterStats.Lives.Decrease(1); }
            if (characterStats.Lives.Value <= 0) { Console.WriteLine("Game Over!"); characterStats.Lives.Decrease(1); }
                                                                                            //   ~~~~
                                                                                            //    ~~O  
            if (lives.Count > characterStats.Lives.Value && characterStats.Lives.Value >= 0)//  ==(oYo)==   
            {                                                                               //     | |
                SceneNode life = lives[lives.Count - 1];                                    //    ( Y )
                RemoveAndDestroyLife(life);                                                 //    | | |
                
            }
            if (lives.Count < characterStats.Lives.Value)
            {
                AddHeart(characterStats.Lives.Value);
            }
            //if (time.Milliseconds>10000) { MaxTime(); }
            healthBar.Width = hRatio * characterStats.Health.Value;
            shieldBar.Width = sRatio * characterStats.Shield.Value;
            scoreText.Caption = score + ((PlayerStats)characterStats).Score.Value;
            timer.Caption="Game Time: "+convertTime(time.Milliseconds);
        }
        /// <summary>
        /// This method is called when Lives = 0
        /// </summary>
        public void GameOver()
        {
            gameOver = OverlayManager.Singleton.GetOverlayElement("GameOverText");
            gameOver.Caption = gmText;
            gameOver.Left = mWindow.Width * 0.5f;
            gameOver.Top = mWindow.Height * 0.5f;

        }
        /// <summary>
        /// This method is called when Level Timer = max time
        /// </summary>
        public void MaxTime()
        {
            levelTime = OverlayManager.Singleton.GetOverlayElement("GameOverText");
            levelTime.Caption = lTText;
            levelTime.Left = mWindow.Width * 0.5f;
            levelTime.Top = mWindow.Height * 0.5f;

        }
        /// <summary>
        /// This method animates the heart rotation
        /// </summary>
        /// <param name="evt"></param>
        protected override void Animate(FrameEvent evt)
        {
            foreach (SceneNode sn in lives)
                sn.Yaw(evt.timeSinceLastFrame);
        }

        /// <summary>
        /// This method disposes of the elements generated in the interface
        /// </summary>
        public override void Dispose()
        {
            List<SceneNode> toRemove = new List<SceneNode>();
            foreach (SceneNode life in lives)
            {
                toRemove.Add(life);
            }
            foreach (SceneNode life in toRemove)
            {
                RemoveAndDestroyLife(life);
            }
            lifeEntity.Dispose();
            toRemove.Clear();
            shieldBar.Dispose();
            healthBar.Dispose();
            scoreText.Dispose();
            panel.Dispose();
            overlay3D.Dispose();
            base.Dispose();
        }
    }
}
