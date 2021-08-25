using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectShell
{
    class Fish : DrawableGameComponent
    {
        GameScene parent;
        Game game;

        //fish will add the coin to the game when it's dropped
        FishType fishType;
        FishCoinDropValue fishCoinValue;
        int currentState;

        //draw
        Vector2 fishPosition;
        List<Texture2D> fishState;

        //fish movement
        Random random = new Random();
        Vector2 newSwimSpot;
        bool movingForward = false;

        //timers fish movement
        double swimTimer = 0.0;
        double timeBeforeNewSwimSpot = 1.0;

        //timers fish hunger
        double timeAfterFeeding = 0.0;
        const double timeBeforeSick = 25.0;
        const double timeBeforeDead = 40.0;
        double deadFishOnScreen = 3;

        //timers fish spawning coins
        double timeAfterCoinDrop = 0.0;
        const double timeBeforeCoinDrops = 5;
        
        public Fish(Game game, FishType fishType, FishCoinDropValue fishCoinValue, GameScene parent) : base(game)
        {
            this.game = game;
            this.fishType = fishType;
            this.fishCoinValue = fishCoinValue;
            this.parent = parent;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.Draw(fishState[currentState],
                    fishPosition,
                    null,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    1f,
                    movingForward ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                    0f);
            sb.End();
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            fishState = new List<Texture2D>();
            swimTimer = timeBeforeNewSwimSpot;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            //setting the new destination
            swimTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (currentState == (int)FishStates.Normal && swimTimer > timeBeforeNewSwimSpot)
            {
                newSwimSpot = CreateNewRandomSpot();
            }

            //moving the fish to the new destination
            if(fishPosition.X > newSwimSpot.X)
            {
                fishPosition.X--;
            }
            else if (fishPosition.X < newSwimSpot.X)
            {
                fishPosition.X++;
            }
            if (fishPosition.Y > newSwimSpot.Y)
            {
                fishPosition.Y--;
            }
            else if (fishPosition.Y < newSwimSpot.Y)
            {
                fishPosition.Y++;
            }

            //feeding and dropping coins
            timeAfterFeeding += gameTime.ElapsedGameTime.TotalSeconds;
            timeAfterCoinDrop += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeAfterFeeding >= timeBeforeSick)
            {
                if(timeAfterFeeding >= timeBeforeDead)
                {
                    currentState = (int)FishStates.Dead;
                    if(timeAfterFeeding >= deadFishOnScreen)
                    {
                        //remove fish after death
                        Game.Components.Remove(this);
                    }
                }
                else
                {
                    currentState = (int)FishStates.Hungry;
                }
            }
            else
            {
                currentState = (int)FishStates.Normal;
                if(timeAfterCoinDrop >= timeBeforeCoinDrops)
                {
                    timeAfterCoinDrop = 0;
                    parent.AddComponent(new Coin(game, parent, fishCoinValue, fishPosition));
                }
            }
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            deadFishOnScreen += timeBeforeDead;
            if (fishType == FishType.Guppy)
            {
                fishState.Add(Game.Content.Load<Texture2D>("Images/GuppyNormal"));
                fishState.Add(Game.Content.Load<Texture2D>("Images/GuppyHungry"));
                fishState.Add(Game.Content.Load<Texture2D>("Images/GuppyDead"));
            }
            else if(fishType == FishType.Piranah)
            {
                fishState.Add(Game.Content.Load<Texture2D>("Images/PredatorNormal"));
                fishState.Add(Game.Content.Load<Texture2D>("Images/PredatorHungry"));
                fishState.Add(Game.Content.Load<Texture2D>("Images/PredatorDead"));
            }

            base.LoadContent();
        }

        /// <summary>
        /// gets a new random spot in the fish tank
        /// </summary>
        /// <returns></returns>
        public Vector2 CreateNewRandomSpot()
        {
            Vector2 newSwimSpot;
            int YMaxDepth = game.GraphicsDevice.Viewport.Height - (fishState[currentState].Height * 2);
            int XMaxLength = game.GraphicsDevice.Viewport.Width - fishState[currentState].Width;

            int newXPosition = random.Next(0, game.GraphicsDevice.Viewport.Width);
            int newYPosition = random.Next(0, YMaxDepth);
            newSwimSpot = new Vector2(newXPosition, newYPosition);

            return newSwimSpot;
        }
    }
}
