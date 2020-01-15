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
    public class Enemy : DrawableGameComponent
    {
        public const int SCREENWIDTH = 1018;
        public const int SCREENHEIGHT = 962;

        public Rectangle enemyBoundingBox;
        public Texture2D texture;
        public Texture2D laserTexture;
        public Vector2 position;
        public int health;
        public int speed;
        public int laserDelay;
        public int currentDifficultyLevel;

        public bool isVisible;
        public List<Laser> laserList;

        public ContentManager Content;
        public SpriteBatch spriteBatch;
        public Rectangle laserBoundingBox;

        public int timer;
        public int soundInterval;



        public Enemy(Game game, SpriteBatch spriteBatch, ContentManager Content, Texture2D texture, Vector2 position, Texture2D laserTexture) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.Content = Content;
            this.texture = texture;
            this.position = position;
            this.laserTexture = laserTexture;

            laserList = new List<Laser>();
            health = 5;
            currentDifficultyLevel = 1;
            laserDelay = 180;
            speed = 1;
            isVisible = true;
            timer = 0;
            soundInterval = 100;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (isVisible)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }

            if (isVisible)
            {
                foreach (Laser L in laserList)
                {
                    L.spriteBatch.Draw(laserTexture, L.position, Color.White);
                    
                }
            }
          
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            //update collison rectangle
            enemyBoundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            //update enemy movement
            position.Y += speed;

            //move enemy back to the top of the screen if he goes off it
            if (position.Y >= SCREENHEIGHT)
                position.Y = -75;

            EnemyShoot();

            //if (timer >= soundInterval)
            //{
            //    SoundManager.enemyLaserSoundEffectInstance.Play();
            //    timer = 0;

            //}
            //else
            //{
            //    timer++;
            //}
            UpdateLasers();

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void UpdateLasers()
        {
            //for each laser in our laserList, update the movement and if the laser hits the top of the screen remove it from the list
            foreach (Laser l in laserList)
            {
                //boundingBox for the lasers 
                laserBoundingBox = l.boundingBox = new Rectangle((int)l.position.X, (int)l.position.Y, l.texture.Width, l.texture.Height);

                //movement for bullet upwards
                l.position.Y += l.speed;


                //if laser hits bottom of the screen, the make laser false
                if (l.position.Y >= SCREENHEIGHT)
                    l.isVisible = false;
            }

            // iterate through laserList and see if any of the laser are not visible, if they are then remove the laser from laserList
            for (int i = 0; i < laserList.Count; i++)
            {
                if (!laserList[i].isVisible)
                {
                    laserList.RemoveAt(i);
                    i--;
                }
            }

            
        }

        //Enemy shoot function
        public void EnemyShoot()
        {
            //shoot only if laserdelay resets
            if (laserDelay >= 0)
                laserDelay--;

            if (laserDelay <= 0)
            {
                //SoundManager.enemyLaserSoundEffectInstance.Stop();
                

                Laser newLaser = new Laser(Game, spriteBatch, Content, laserTexture);
                newLaser.position = new Vector2(position.X + texture.Width / 2 - newLaser.texture.Width /2, position.Y + 33);

                //Laser newLaser2 = new Laser(Game, spriteBatch, Content, laserTexture);
                //newLaser2.position = new Vector2(position.X + 65 - newLaser.texture.Width / 2, position.Y + 30);

                //make laser visible
                newLaser.isVisible = true;
               

                if (laserList.Count < 20)
                {
                    laserList.Add(newLaser);
                    //laserList.Add(newLaser2);
                    

                }
            }

            //reset laserDelay
            if (laserDelay == 0)
            {
                laserDelay = 180;
                
            }

            

        }
    }
}
