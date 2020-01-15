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
    public class Background : DrawableGameComponent
    {
        public const int SCREENWIDTH = 1018;
        public const int SCREENHEIGHT = 962;


        public Texture2D texture;
        public Vector2 bgPosition1;
        public Vector2 bgPosition2;
        public int speed;

        public ContentManager Content;
        public SpriteBatch spriteBatch;

        public Background(Game game, SpriteBatch spriteBatch, ContentManager Content) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.Content = Content;
            bgPosition1 = new Vector2(0, 0);
            bgPosition2 = new Vector2(0, -SCREENHEIGHT);
            speed = 3;

            LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, bgPosition1, Color.White);
            spriteBatch.Draw(texture, bgPosition2, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            bgPosition1.Y = bgPosition1.Y + speed;
            bgPosition2.Y = bgPosition2.Y + speed;

            //Paralax Background Effect
            if(bgPosition1.Y >= SCREENHEIGHT)
            {
                bgPosition1.Y = 0;
                bgPosition2.Y = -SCREENHEIGHT;
            }

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            texture = Content.Load<Texture2D>("Images/Galaxy4");
            base.LoadContent();
        }
    }
}
