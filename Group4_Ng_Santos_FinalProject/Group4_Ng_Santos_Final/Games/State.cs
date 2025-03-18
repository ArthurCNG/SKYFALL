using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4_Ng_Santos_Final.Games
{
    public abstract class State //abstract class for the game states
    {
        //This class makes it easier to switch between different states of the game
        // Also, it makes it easier to update and draw the game
        // The class is abstract because it is a base class for the different states of the game
        public ContentManager _content;
        public GraphicsDevice _graphicDevice;
        public SkyFall _skyFall;
        public abstract void Draw(GameTime gameTime , SpriteBatch spriteBatch); //abstract method for drawing the game
        public abstract void Update(GameTime gameTime); //abstract method for updating the game
        public State(SkyFall skyFall, GraphicsDevice graphicsDevice, ContentManager contentManager) //constructor for the state class
        {
            _skyFall = skyFall;
            _graphicDevice = graphicsDevice;
            _content = contentManager;
        }
    }
}
