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
    public class Laser : DrawableGameComponent
    {
        public SpriteBatch spriteBatch;
        public ContentManager Content;

        public Rectangle boundingBox;
        public Texture2D texture;
        public Vector2 origin;
        public Vector2 position;
        public Vector2 position2;
        public bool isVisible;
        public float speed;

        public Laser(Game game,SpriteBatch spriteBatch, ContentManager Content, Texture2D texture) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.Content = Content;
            this.texture = texture;
            speed = 10;
            isVisible = false;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }
    }
}
