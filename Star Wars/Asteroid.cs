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
    public class Asteroid : DrawableGameComponent
    {
        public SpriteBatch spriteBatch;
        public ContentManager Content;

        public const int SCREENWIDTH = 1018;
        public const int SCREENHEIGHT = 962;

        
        public Rectangle asteroidBoundingBox;
        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;
        public float rotationAngle;
        public int speed;
        public int asteroidDelay = -500;

        public bool isVisible;
        Random rand = new Random();
        public float randX, randY;


        public Asteroid(Game game, SpriteBatch spriteBatch, ContentManager Content, Texture2D texture, Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.Content = Content;
            this.texture = texture;
            this.position = position;

            isVisible = true;
            //position = new Vector2(509, asteroidDelay);
            speed = 1;
            randX = rand.Next(0, 750);
            randY = rand.Next(-600, -50);

            LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            
            spriteBatch.Begin();
            
            if (isVisible)
            {
                spriteBatch.Draw(texture, position, null, Color.White, rotationAngle, origin, 1f, SpriteEffects.None, 0); 
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
           
            asteroidBoundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
          

            if (asteroidBoundingBox.Intersects(Player.playerBoundingBox))
            {
                
               
            }
            
            //moving asteroid down the screen and restrating if it goes past the screen
            position.Y = position.Y + speed;

            if (position.Y >= SCREENHEIGHT)
            {
                Direction();
            }
                

            //rotate asteroid
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            rotationAngle += elapsed;
            float circle = MathHelper.Pi * 2;
            rotationAngle = rotationAngle % circle;

            //finding center of the asteroid sprite
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;


            
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            texture = Content.Load<Texture2D>("Images/asteroid_resize");

          
            base.LoadContent();
        }

        public Vector2 Direction()
        {
            int direction;
            Random rand = new Random();
            direction = rand.Next(0, SCREENWIDTH);
            position = new Vector2(direction, asteroidDelay);

            return position;
        }
        
            
    }    


}

