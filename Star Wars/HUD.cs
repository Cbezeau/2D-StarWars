using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Star_Wars
{
    public class HUD : DrawableGameComponent
    {
        public const int SCREENWIDTH = 1018;
        public const int SCREENHEIGHT = 962;
        public static int playerScore;
        public SpriteFont playerScoreFont;

        public Vector2 playerScorePOS;
        public bool showHUD;

        SpriteBatch spriteBatch;
        ContentManager Content;

        public HUD(Game game, SpriteBatch spriteBatch, ContentManager Content) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.Content = Content;
            playerScore = 0;
            showHUD = true;
            playerScorePOS = new Vector2(SCREENWIDTH / 2, 50);


            LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (showHUD)
            {
                spriteBatch.DrawString(playerScoreFont, "Score : " + playerScore, playerScorePOS, Color.White);
            }

            if (!Player.isAlive)
            {
                spriteBatch.DrawString(playerScoreFont, "Game Over", new Vector2(500, 500), Color.White);
                spriteBatch.DrawString(playerScoreFont, "Final Score : " + playerScore.ToString(), new Vector2(600, 600), Color.Yellow);
                MediaPlayer.Stop();
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            // get keyboard state
            KeyboardState ks = Keyboard.GetState();


            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            playerScoreFont = Content.Load<SpriteFont>("font");
            base.LoadContent();
        }
    }
}
