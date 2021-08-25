using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectShell
{
    class Tank : DrawableGameComponent
    {
        Texture2D tankTexture;
        public Tank(Game game) : base(game)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();

            sb.Draw(tankTexture, Vector2.Zero, Color.White);

            sb.End();
            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            tankTexture = Game.Content.Load<Texture2D>("Images/BackgroundBeginner");
            base.LoadContent();
        }
    }
}
