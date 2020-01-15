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
using Microsoft.Xna.Framework.Audio;

namespace Star_Wars
{
    public class SoundManager : DrawableGameComponent
    {
        public static SoundEffect playerShoot;
        public static SoundEffect explode;
        public static SoundEffect enemyShoot;
        public static Song bgMusic;

        public static SoundEffectInstance laserSoundEffectInstance;
        public static SoundEffectInstance enemyLaserSoundEffectInstance;
      

        ContentManager Content;

        public SoundManager(Game game, ContentManager Content) : base(game)
        {
            this.Content = Content;
            
            LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            playerShoot = Content.Load<SoundEffect>("Sounds/XWing-Laser");
            enemyShoot = Content.Load<SoundEffect>("Sounds/TIE-Fire");
            //explode = Content.Load<SoundEffect>("");
            bgMusic = Content.Load<Song>("Sounds/03 I Can Fly Anything");
            laserSoundEffectInstance = playerShoot.CreateInstance();
            enemyLaserSoundEffectInstance = enemyShoot.CreateInstance();


            base.LoadContent();
        }
    }
}
