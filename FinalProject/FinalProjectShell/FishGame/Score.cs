using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectShell
{
    class Score : DrawableGameComponent
    {
        int score = 0;
        int scoreMultiplyer = 10;
        private string stringToDisplay = "";
        private SpriteFont font;
        Vector2 scorePosition;

        public Score(Game game) : base(game)
        {
            Game.Services.AddService<Score>(this);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();

            sb.DrawString(font, stringToDisplay, scorePosition, Color.DarkMagenta);

            sb.End();
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            stringToDisplay = $"Score: {score}";
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("Fonts/regularFont");
            scorePosition = new Vector2(Game.GraphicsDevice.Viewport.Width/2, 0);
            base.LoadContent();
        }

        public void AddToScore(int addToScore)
        {
            score += addToScore * scoreMultiplyer;
        }
    }
}
