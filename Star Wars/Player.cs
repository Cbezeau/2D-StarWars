using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Star_Wars
{
    public class Player : DrawableGameComponent
    {
        public const int SCREENWIDTH = 1018;
        public const int SCREENHEIGHT = 962;

        public Texture2D laserTexture;
        public float laserDelay;
        public static List<Laser> laserList;

        public Texture2D texture;
        public Vector2 position;
        public SpriteBatch spriteBatch;
        public float speed;

        public ContentManager Content;

        public Texture2D heathTexture;
        public static int health;
        public Rectangle healthRectangle;
        public Vector2 healthPosition;
        public static bool isAlive = true;

        

        //Collison variables
        public bool isColliding;
        public static Rectangle playerBoundingBox;
        public Rectangle laserBoundingBox;


        public Player(Game game, SpriteBatch spriteBatch, ContentManager Content) : base(game)
        {
            this.Content = Content;
            this.spriteBatch = spriteBatch;
            position = new Vector2(300, 300);
            speed = 10;
            isColliding = false;
            laserList = new List<Laser>();
            laserDelay = 1;

            //pixels wide for health bar
            health = 300;
            healthPosition = new Vector2(SCREENWIDTH - 350,50);

            LoadContent();
        }
        protected override void LoadContent()
        {
            texture = Content.Load<Texture2D>("Images/Player");
            laserTexture = Content.Load<Texture2D>("Images/Laser_resize");
            heathTexture = Content.Load<Texture2D>("Images/HealthBar");

            base.LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (isAlive)
            {
                spriteBatch.Draw(texture, position, Color.White);
                spriteBatch.Draw(heathTexture, healthRectangle, Color.White);

                foreach (Laser l in laserList)
                {
                    l.spriteBatch.Draw(laserTexture, l.position, Color.White);
                }
            }
            

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            //boundingBox for playership
            playerBoundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);


            //set rectangle for health bar (25 equals png height)
            healthRectangle = new Rectangle((int)healthPosition.X, (int)healthPosition.Y, health, 30);

            KeyboardState ks = Keyboard.GetState();
            //fire lasers

            if (ks.IsKeyUp(Keys.Space))
            {
                laserDelay = 1;
            }
            if (ks.IsKeyDown(Keys.Space))
            {
                Shoot();
            }

            updateLasers();

            //Ship controls
            if (ks.IsKeyDown(Keys.Up))
            {
                position.Y = position.Y - speed;
            }
            if (ks.IsKeyDown(Keys.Left))
            {
                position.X = position.X - speed;
            }
            if (ks.IsKeyDown(Keys.Down))
            {
                position.Y = position.Y + speed;
            }
            if (ks.IsKeyDown(Keys.Right))
            {
                position.X = position.X + speed;
            }

            //keep the ship within the screen 
            if (position.X <= 0)
                position.X = 0;

            if (position.X >= SCREENWIDTH - texture.Width)
                position.X = SCREENWIDTH - texture.Width;

            if (position.Y <= 0)
                position.Y = 0;

            if (position.Y >= SCREENHEIGHT - texture.Height)
                position.Y = SCREENHEIGHT - texture.Height;

            
            base.Update(gameTime);
        }

        //Shoot - method to set starting position of lasers
        public void Shoot()
        {
            //shoot only if laser delay resets
            if(laserDelay >= 0)
            {
                laserDelay--;
            }

            //if laserDelay is at 0; create a new laster at player position and make it visible, then add laser to list
            if(laserDelay <= 0)
            {
                //SoundManager.playerShoot.Play();
                Laser newLaser = new Laser(Game, spriteBatch, Content, laserTexture);
                newLaser.position = new Vector2(position.X + 5 - newLaser.texture.Width / 2, position.Y + 33);

                Laser newLaser2 = new Laser(Game, spriteBatch, Content, laserTexture);
                newLaser2.position = new Vector2(position.X + 65 - newLaser.texture.Width / 2, position.Y + 30);

                //make laser visible
                newLaser.isVisible = true;
                newLaser2.isVisible = true;

                if (laserList.Count < 20)
                {
                    
                    laserList.Add(newLaser);
                    laserList.Add(newLaser2);
                    SoundManager.laserSoundEffectInstance.Stop();
                }
            }

            //reset laserDelay
            if (laserDelay == 0)
                laserDelay = 10;
            
        }

        //update Lasers
        public void updateLasers()
        {
            //for each laser in our laserList, update the movement and if the laser hits the top of the screen remove it from the list
            foreach(Laser l in laserList)
            {
                //boundingBox for the lasers 
              laserBoundingBox = l.boundingBox = new Rectangle((int)l.position.X, (int)l.position.Y, l.texture.Width, l.texture.Height);

                //movement for bullet upwards
                l.position.Y -= l.speed;
               

                //if laser hits top of the screen, the make laser false
                if (l.position.Y <= 0)
                    l.isVisible = false;
            }

            // iterate through laserList and see if any of the laser are not visible, if they are then remove the laser from laserList
            for(int i = 0; i < laserList.Count; i++)
            {
                if (!laserList[i].isVisible)
                {
                    laserList.RemoveAt(i);
                    i--;
                }
            }
        }

       

      
    }
}
