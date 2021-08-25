using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
    class FishTankManager : GameComponent
    {
        GameScene parent;
        Game game;
        SoundEffect splash;
        Song backgroundMusic;

        bool guppyKeyDown = false;
        bool piranahKeyDown = false;
        bool firstIterationThrough = true;

        public FishTankManager(Game game, GameScene parent) : base(game)
        {
            this.game = game;
            this.parent = parent;
        }

        public override void Initialize()
        {
            splash = Game.Content.Load<SoundEffect>("Sounds/bloop");
            backgroundMusic = Game.Content.Load<Song>("Sounds/gone_fishin");
            MediaPlayer.Volume = 0.2f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);
            game.IsMouseVisible = true;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if(firstIterationThrough)
            {
                firstIterationThrough = false;
                parent.AddComponent(new Tank(game));
            }
            KeyboardState ks = Keyboard.GetState();

            if(ks.IsKeyDown(Keys.G) && !guppyKeyDown)
            {
                guppyKeyDown = true;
                //add guppy if user has enough money
                parent.AddComponent(new Fish(Game, FishType.Guppy, FishCoinDropValue.Guppy, parent));
                splash.Play();
            }
            else if(ks.IsKeyDown(Keys.P) && !piranahKeyDown)
            {
                piranahKeyDown = true;
                //add piranah if user has enough money
                parent.AddComponent(new Fish(Game, FishType.Piranah, FishCoinDropValue.Piranah, parent));
                splash.Play();
            }

            if (ks.IsKeyUp(Keys.G))
            {
                guppyKeyDown = false;
            }
            else if (ks.IsKeyUp(Keys.P))
            {
                piranahKeyDown = false;
            }

            base.Update(gameTime);
        }
    }
}
