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
    class Coin : DrawableGameComponent
    {
        FishCoinDropValue coinValue;

        bool isCoinOnGround = false;
        bool processClick = false;
        const int floorYCord = 500;
        const int coinFallingSpeed = 1;
        //flicker speed
        //time before vanish
        double counterForFlicker = 0;
        double counterForRemoval = 0;
        const double timeBeforeCoinIsRemoved = 4;
        const double flickerCoinInterval = 0.3;
        bool displayCoin = true;

        Vector2 coinPosition;
        Texture2D coinTexture;


        public Coin(Game game, GameScene parent, FishCoinDropValue coinValue, Vector2 fishPosition) : base(game)
        {
            this.coinValue = coinValue;
            this.coinPosition = fishPosition;
            isCoinOnGround = false;
        }

        public override void Draw(GameTime gameTime)
        {
            if(displayCoin)
            {
                SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
                sb.Begin();

                sb.Draw(coinTexture, coinPosition, Color.White);

                sb.End();
            }
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// loads the image of the coin
        /// </summary>
        protected override void LoadContent()
        {
            SpriteBatch sb = new SpriteBatch(GraphicsDevice);
            if(coinValue == FishCoinDropValue.Guppy)
            {
                coinTexture = Game.Content.Load<Texture2D>("Images/Coin Silver");
            }
            else if (coinValue == FishCoinDropValue.Piranah)
            {
                coinTexture = Game.Content.Load<Texture2D>("Images/Diamond");
            }

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// collects coin if user clicks it and removes coin if it's on the floor
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            DidUserClickMouse();
            isCoinOnGround = CheckCoinOnFloor();
            if (!isCoinOnGround)
            {
                MoveCoinToFloor();
            }
            else
            {
                counterForFlicker += gameTime.ElapsedGameTime.TotalSeconds;
                counterForRemoval += gameTime.ElapsedGameTime.TotalSeconds;
                //remove coin after some time
                if (counterForRemoval >= timeBeforeCoinIsRemoved)
                {
                    Game.Components.Remove(this);
                }
                else if (counterForFlicker >= flickerCoinInterval)
                {
                    displayCoin = !displayCoin;
                    counterForFlicker = 0.0;
                }
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// checks if the coin is on the floor, returns true for yes, false for no
        /// </summary>
        /// <returns></returns>
        private bool CheckCoinOnFloor()
        {
            bool retValue = false;

            if((coinPosition.Y + coinTexture.Height) >= floorYCord)
            {
                retValue = true;
            }

            return retValue;
        }

        /// <summary>
        /// moves the coin closer to the floor
        /// </summary>
        private void MoveCoinToFloor()
        {
            coinPosition.Y -= - coinFallingSpeed;
        }

        /// <summary>
        /// returns the value of the coin the fish dropped
        /// </summary>
        /// <returns></returns>
        private int GetCoinValue()
        {
            int retValue = 0;
            switch(coinValue)
            {
                case FishCoinDropValue.Guppy:
                    retValue = (int)FishCoinDropValue.Guppy;
                    break;
                case FishCoinDropValue.Piranah:
                    retValue = (int)FishCoinDropValue.Piranah;
                    break;
                default:
                    retValue = 0;
                    break;
            }
            return retValue;
        }

        /// <summary>
        /// checks if user clicked on the coin, if so: 
        /// </summary>
        private void DidUserClickMouse()
        {
            if(Mouse.GetState().LeftButton == ButtonState.Pressed && !processClick)
            {
                processClick = true;

                Point mousePosition = Mouse.GetState().Position;
                Rectangle coinHitBox = coinTexture.Bounds;
                coinHitBox.Location = coinPosition.ToPoint();

                if (coinHitBox.Contains(mousePosition))
                {
                    Game.Services.GetService<Score>().AddToScore((int)coinValue);
                    //userCollectsCoin.Play();
                    Enabled = false;
                    Game.Components.Remove(this);
                }

                if(Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    processClick = false;
                }
            }
        }
    }
}
