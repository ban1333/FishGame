using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectShell
{
    class CreditTextComponent : DrawableGameComponent
    {
        const string name = "David Broomfield";
        const string soundLink = "https://opengameart.org/content/water-splash-yo-frankie";
        const string pictureLink = "https://opengameart.org/content/fish-set";
        string compiledString = "";
        SpriteFont font;

        public CreditTextComponent(Game game) : base(game)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();

            sb.DrawString(font, compiledString, Vector2.Zero, Color.Black);

            sb.End();
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            compiledString = $"Creator: {name}\r\n\r\n";
            compiledString += $"Sound clip acquired from:\r\n{soundLink}\r\n\r\n";
            compiledString += $"Pictures acquired from:\r\n{pictureLink}";
            base.Initialize();
        }

        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("Fonts/regularFont");
            base.LoadContent();
        }
    }
}
