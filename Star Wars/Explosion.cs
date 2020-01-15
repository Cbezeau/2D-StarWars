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
    public class Explosion : DrawableGameComponent
    {
        public Texture2D texture;
        public Vector2 position;
        public float timer;
        public float interval;
        public Vector2 origin;
        public int currentFrame;
        public int spriteWidth;
        public int spriteHeight;
        public Rectangle sourceRectangle;
        public bool isVisible;

        public SpriteBatch spriteBatch;
        ContentManager Content;
        
        public Explosion(Game game, SpriteBatch spriteBatch, ContentManager Content ,Texture2D texture, Vector2 position) : base(game)
        {
            this.position = position;
            this.texture = texture;
            this.spriteBatch = spriteBatch;
            this.Content = Content;

            timer = 0f;
            interval = 20f;
            currentFrame = 1;
            spriteWidth = 170;
            spriteHeight = 215;
            isVisible = true;

            LoadContent();

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (isVisible)
            {
                spriteBatch.Draw(texture, position, sourceRectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            //increase the timer by the number of milliseconds since update was last called
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //check the time is more than the choosen interval
            if(timer > interval)
            {
                //show next frame
                currentFrame++;
                //reset timer
                timer = 0f;
            }

            //if were on the last frame, make the explosion invisible and reset to currentframe
            if(currentFrame == 19)
            {
                isVisible = false;
                currentFrame = 0;
            }

            sourceRectangle = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
            origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            //texture = Content.Load<>
            base.LoadContent();
        }
    }
}
