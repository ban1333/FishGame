using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProjectShell
{
    class HelpTextComponent : DrawableGameComponent
    {
        //Texture2D texture;
        string spawnFish = "";
        string pointOfGame = "";
        SpriteFont font;

        public HelpTextComponent(Game game) : base(game)
        {
            
        }


        public override void Initialize()
        {
            spawnFish = "Press G to spawn a guppy\r\nPress P to spawn a piranah";
            pointOfGame = "Fish get sick and\r\npass away after a certain amount of time\r\n\r\n";
            pointOfGame += "Collect coins to add more\r\nfish to your fish tank";
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();

            sb.DrawString(font, spawnFish, Vector2.Zero, Color.Black);
            sb.DrawString(font, pointOfGame, new Vector2(200, 100), Color.Black);

            sb.End();
            base.Draw(gameTime);
        }
        

        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("Fonts/regularFont");
            base.LoadContent();
        }
    }
}
