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
    public class HelpState : State
    {
        public List<Component> _components;
        public HelpState(SkyFall skyFall, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(skyFall, graphicsDevice, contentManager)
        {
            var buttonBox = contentManager.Load<Texture2D>("Controls/Button");
            var buttonFont = Fonts.Font;

            var menuButton = new Button(buttonBox, buttonFont) //Creates a button for the main menu
            {
                Position = new Vector2((SkyFall.ScreenSize.X / 2 - 100), (SkyFall.ScreenSize.Y - 100)),
                Text = "Main Menu",
            };
            menuButton.Click += MenuButton_Click;

            var exitButton = new Button(buttonBox, buttonFont) //Creates a button to exit the game
            {
                Position = new Vector2((SkyFall.ScreenSize.X / 2 + 100), (SkyFall.ScreenSize.Y - 100)),
                Text = "Exit",
            };
            exitButton.Click += ExitButton_Click;

            _components = new List<Component>()
            {
                menuButton,
                exitButton
            };
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Art.GreyBackground, new Vector2(0, 0), Color.White); //Draws the background

            foreach (var component in _components) //Draws the buttons
            {
                component.Draw(gameTime, spriteBatch);
            }

            // Text and Images with the game instructions
            spriteBatch.DrawString(Fonts.GameOverFont, "How to play:", new Vector2(100, 50), Color.Black);
            spriteBatch.Draw(Art.Wasd, new Vector2(100, 200), Color.Black);
            spriteBatch.Draw(Art.ArrowKeys, new Vector2(250, 200), Color.Black);
            spriteBatch.DrawString(Fonts.Font, "Use the WASD or Arrow Keys to move the player", new Vector2(100, 350), Color.Black);
            spriteBatch.Draw(Art.ThunderCloud, new Vector2(100, 400), Color.White);
            spriteBatch.DrawString(Fonts.Font, "    Avoid the Clouds", new Vector2(200, 425), Color.Black);
            spriteBatch.Draw(Art.Fuel, new Vector2(100, 525), Color.White);
            spriteBatch.DrawString(Fonts.Font, "Get the fuel before it runs out!", new Vector2(200, 550), Color.Black);
            spriteBatch.DrawString(Fonts.Font, "Capture the birds to earn points!", new Vector2(100, 600), Color.Black);
            spriteBatch.Draw(Art.WhiteBird, new Vector2(100, 650), Color.White);
            spriteBatch.DrawString(Fonts.Font, "  +25 Points", new Vector2(200, 675), Color.Black);
            spriteBatch.Draw(Art.BrownBird, new Vector2(90, 750), Color.White);
            spriteBatch.DrawString(Fonts.Font, "  +50 Points", new Vector2(200, 775), Color.Black);
            spriteBatch.Draw(Art.BlueBird, new Vector2(100, 850), Color.White);
            spriteBatch.DrawString(Fonts.Font, "  +200 Points", new Vector2(200, 875), Color.Black);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            _skyFall.ChangeState(new MenuState(_skyFall, _graphicDevice, _content));
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            _skyFall.Exit();
        }
    }
    }

