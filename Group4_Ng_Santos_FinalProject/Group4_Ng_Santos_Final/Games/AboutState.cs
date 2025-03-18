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
    public class AboutState : State
    {
        public List<Component> _components;
        
        public AboutState(SkyFall skyFall, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(skyFall, graphicsDevice, contentManager)
        {
            var buttonBox = contentManager.Load<Texture2D>("Controls/Button");
            var buttonFont = Fonts.Font;

            var menuButton = new Button(buttonBox, buttonFont) //Button for the main menu
            {
                Position = new Vector2((SkyFall.ScreenSize.X / 2 - 100), (SkyFall.ScreenSize.Y - 100)),
                Text = "Main Menu",
            };
            menuButton.Click += MenuButton_Click;

            var exitButton = new Button(buttonBox, buttonFont) //Button to exit the game
            {
                Position = new Vector2((SkyFall.ScreenSize.X / 2 + 100), (SkyFall.ScreenSize.Y - 100)),
                Text = "Exit",
            };
            exitButton.Click += ExitButton_Click;

            _components = new List<Component>() //List of components
            {
                menuButton,
                exitButton
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        { 
            //Draws the background and the buttons and adds the text
            spriteBatch.Begin();
            spriteBatch.Draw(Art.GreyBackground, new Vector2(0, 0), Color.White);
            foreach (var component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }
            spriteBatch.DrawString(Fonts.GameOverFont, "About:\nThis is a Game Made by Arthur Ng\nand Bruno Cezare for the final project!", new Vector2(50, 100), Color.Black);
            
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            //Updates the buttons
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            //Changes the state to the menu state
            _skyFall.ChangeState(new MenuState(_skyFall, _graphicDevice, _content));
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            //Exits the game
            _skyFall.Exit();
        }
    }
}
    