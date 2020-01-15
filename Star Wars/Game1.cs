using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Star_Wars
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public const int SCREENWIDTH = 1018;
        public const int SCREENHEIGHT = 962;

        Random rand = new Random();

        //Texture2D laserTexture;
        Texture2D asteroidTexture;
        Texture2D enemyTexture;
        Texture2D enemyLaser;
        Texture2D explosionTexture;

        public int enemyLaserDamage;


        List<Asteroid> asteroidList = new List<Asteroid>();
        List<Enemy> enemyList = new List<Enemy>();
        List<Explosion> explosionList = new List<Explosion>();

        //Player p;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = SCREENWIDTH;
            graphics.PreferredBackBufferHeight = SCREENHEIGHT;
            Window.Title = "Star Wars : 2D Space Shooter";
            enemyLaserDamage = 10;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            Background b = new Background(this, spriteBatch, Content);
            Components.Add(b);
            //Asteroid a = new Asteroid(this, spriteBatch, Content);
            //Components.Add(a);
            

            Player p = new Player(this, spriteBatch, Content);
            Components.Add(p);

            HUD h = new HUD(this, spriteBatch, Content);
            Components.Add(h);

            SoundManager s = new SoundManager(this, Content);
            Components.Add(s);

            MediaPlayer.Play(SoundManager.bgMusic);


            asteroidTexture = Content.Load<Texture2D>("Images/asteroid_resize");
            enemyTexture = Content.Load<Texture2D>("Images/EnemyShip");
            enemyLaser = Content.Load<Texture2D>("Images/EnemyLaser");
            explosionTexture = Content.Load<Texture2D>("Images/Explosion");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            //updating enemies and checking collision of enemy to player ship
            foreach(Enemy e in enemyList)
            {
                //check if enemyship is collding with player
                if (e.enemyBoundingBox.Intersects(Player.playerBoundingBox))
                {
                    Player.health -= 40;
                    e.isVisible = false;
                }

                //check enemy laser collision with player ship
                for(int i = 0; i < e.laserList.Count; i++)
                {
                    if (Player.playerBoundingBox.Intersects(e.laserList[i].boundingBox))
                    {
                        Player.health -= enemyLaserDamage;
                        e.laserList[i].isVisible = false;
                    }
                }

                //check player laser colliuson to enemy ship
                for(int i = 0; i < Player.laserList.Count; i++)
                {
                    if (Player.laserList[i].boundingBox.Intersects(e.enemyBoundingBox))
                    {
                        Explosion newExplosion = new Explosion(this, spriteBatch, Content, explosionTexture, new Vector2(e.position.X, e.position.Y));
                        explosionList.Add(newExplosion);
                        Components.Add(newExplosion);
                        //SoundManager.explode.Play();
                        HUD.playerScore += 20;
                        Player.laserList[i].isVisible = false;
                        e.isVisible = false;
                    }
                }

                e.Update(gameTime);
            }

            foreach(Explosion ex in explosionList)
            {
                ex.Update(gameTime);
            }





            foreach(Asteroid a in asteroidList)
            {
                if (a.asteroidBoundingBox.Intersects(Player.playerBoundingBox))
                {
                    Player.health -= 20;
                    a.isVisible = false;
                }
               

                //iterate through laserlist if any asteroids come in contact with lasers, destroy asteroid and remove laser
                for (int i = 0; i < Player.laserList.Count; i++)
                {
                    if (a.asteroidBoundingBox.Intersects(Player.laserList[i].boundingBox))
                    {
                        Explosion newExplosion2 = new Explosion(this, spriteBatch, Content, explosionTexture, new Vector2(a.position.X, a.position.Y));
                        explosionList.Add(newExplosion2);
                        Components.Add(newExplosion2);
                        //SoundManager.explode.Play();
                        HUD.playerScore += 10;
                        a.isVisible = false;
                        Player.laserList.ElementAt(i).isVisible = false;
                    }
                }

                a.Update(gameTime);
            }

            
           if(Player.health <= 0)
            {
                Player.isAlive = false;
            }

            // TODO: Add your update logic here
            
            LoadEnemies();
            LoadAsteroids();
            ManageExplosions();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            foreach(Asteroid a in asteroidList)
            {
                a.spriteBatch.Draw(asteroidTexture, a.position, Color.White);
            }

            foreach(Enemy e in enemyList)
            {
                e.spriteBatch.Draw(enemyTexture, e.position, Color.White);
            }

            foreach(Explosion ex in explosionList)
            {
                ex.spriteBatch.Draw(explosionTexture, ex.position, Color.White);
            }

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        // load asteroids
        public void LoadAsteroids()
        {
            //creating random variables for our X and Y axises of our asteroids
            int randY = rand.Next(-600, -50);
            int randX = rand.Next(0, 1000);

            //Texture2D asteroidTexture = Content.Load<Texture2D>("Images/asteroid_resize");

            //less than 3 asteroids on the screen, then create more until there is 3 again
            Asteroid newAsteroid = new Asteroid(this, spriteBatch, Content, asteroidTexture, new Vector2(randX, randY));
            
            //newAsteroid.position = new Vector2(randX, randY);

            if(asteroidList.Count < 3)
            {
                asteroidList.Add(newAsteroid);
                Components.Add(newAsteroid);
            }

            //if any of the asteroids in the list were invisible then remove them from the list
            for(int i = 0; i<asteroidList.Count; i++)
            {
                if (!asteroidList[i].isVisible)
                {
                    asteroidList.RemoveAt(i);
                    i--;
                }
            }
        }

        //load enemies 
        public void LoadEnemies()
        {

            //creating random variables for our X and Y axises of our asteroids
            int randY = rand.Next(-600, -50);
            int randX = rand.Next(0, 700);

            //Texture2D asteroidTexture = Content.Load<Texture2D>("Images/asteroid_resize");

            //less than 3 asteroids on the screen, then create more until there is 3 again
            Enemy newEnemy = new Enemy(this, spriteBatch, Content, enemyTexture, new Vector2(randX, randY), enemyLaser);

            //newAsteroid.position = new Vector2(randX, randY);

            if (enemyList.Count < 2)
            {
                enemyList.Add(newEnemy);
                Components.Add(newEnemy);
            }

            //if any of the enemies in the list were invisible then remove them from the list
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (!enemyList[i].isVisible)
                {
                    enemyList.RemoveAt(i);
                    i--;
                }
            }


        }

        // manage explosions
        public void ManageExplosions()
        {
            for(int i = 0; i < explosionList.Count; i++)
            {
                if (!explosionList[i].isVisible)
                {
                    explosionList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
