using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProjectShell
{
    public abstract class GameScene : GameComponent
    {

        /// <summary>
        /// Used to hold a reference to the components that belong to 
        /// this GameScene instance.  Used to quickly iterate through components 
        /// that belong to the scence to enable and make visible where applicable
        /// </summary>
        protected List<GameComponent> SceneComponents { get; set; }

        public GameScene(Game game) : base(game)
        {
            SceneComponents = new List<GameComponent>();
            Hide();
        }

        public void AddComponent(GameComponent component)
        {
            this.SceneComponents.Add(component);
            Game.Components.Add(component);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// Shows this instance of game scene and all of its
        /// components.  Sets Enabled and Visible to true
        /// for all of the components that belong to this 
        /// scene
        /// </summary>
        public virtual void Show()
        {

            this.Enabled = true;
            // iterate though all components held by this scene 
            // and set Enabled to true and if it's also a DrawableGameComponent
            // set Visible to true
            foreach( GameComponent component in SceneComponents)
            {
                component.Enabled = true;
                if( component is DrawableGameComponent)
                {
                    ((DrawableGameComponent)component).Visible = true;
                }
            }  
        }

        /// <summary>
        /// Hides this instance of game scene and all of its
        /// components.  Sets Enabled and Visible to false
        /// for all of the components that belong to this 
        /// scene
        /// </summary>
        public virtual void Hide()
        {
            this.Enabled = false;
            // iterate though all components held by this scene 
            // and set Enabled to false and if it's also a DrawableGameComponent
            // set Visible to false
            foreach (GameComponent component in SceneComponents)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
                {
                    ((DrawableGameComponent)component).Visible = false;
                }
            }
        }

        /// <summary>
        /// As a final step of initialization of the GameScene
        /// we will iterate through all the items that we added to the scene 
        /// and add them to the game components collection
        /// </summary>
        public override void Initialize()
        {
            // iterate through the list of scene components and
            // add them to the game component for the framework to manage
            foreach (GameComponent component in SceneComponents)
            {
                if( Game.Components.Contains( component) == false)
                {
                    Game.Components.Add(component);
                }
                
            }
            
            base.Initialize();
        }
    }
}
