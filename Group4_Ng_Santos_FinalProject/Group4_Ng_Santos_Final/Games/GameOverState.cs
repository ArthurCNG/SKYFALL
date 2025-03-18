using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Group4_Ng_Santos_Final.Games
{
   
    internal class GameOverState : State
    {
        public List<Component> _components;

        string textGameOver = "Game Over!";

        public GameOverState(SkyFall skyFall, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(skyFall, graphicsDevice, contentManager)
        {
            var buttonBox = contentManager.Load<Texture2D>("Controls/Button");
            var buttonFont = Fonts.Font;

            var replayButton = new Button(buttonBox, buttonFont) //Replay Button
            {
                Position = new Vector2((SkyFall.ScreenSize.X / 2 - 50), (SkyFall.ScreenSize.Y / 2) - 50),
                Text = "Replay",
            };
            replayButton.Click += ReplayButton_Click;

            var menuButton = new Button(buttonBox, buttonFont) //Menu Button
            {
                Position = new Vector2((SkyFall.ScreenSize.X / 2 - 50), (SkyFall.ScreenSize.Y / 2)),
                Text = "Main Menu",
            };
            menuButton.Click += MenuButton_Click;

            var exitButton = new Button(buttonBox, buttonFont) //Exit Button
            {
                Position = new Vector2((SkyFall.ScreenSize.X / 2 - 50), (SkyFall.ScreenSize.Y / 2) + 50),
                Text = "Exit",
            };
            exitButton.Click += ExitButton_Click;

            _components = new List<Component>()
            {
                replayButton,
                menuButton,
                exitButton
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) //Draws the GameOverState
        {
            spriteBatch.Begin();
            foreach (var component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }
            spriteBatch.DrawString(Fonts.GameOverFont, textGameOver, new Vector2(735, SkyFall.ScreenSize.Y / 2 - 280), Color.Black);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime) //Updates the GameOverState
        {
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }
        
        private void MenuButton_Click(object sender, EventArgs e) //Menu Button Click
        {
            _skyFall.ChangeState(new MenuState(_skyFall, _graphicDevice, _content));
        }

        private void ReplayButton_Click(object sender, EventArgs e) //Replay Button Click
        {
            _skyFall.ChangeState(new Level1State(_skyFall, _graphicDevice, _content));
        }

        private void ExitButton_Click(object sender, EventArgs e) //Exit Button Click
        {
            _skyFall.Exit();
        }
    }
}