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
    public class Level1CompleteState : State
    {
        public List<Component> _components;

        public AnimatedSprite firework = new AnimatedSprite(SkyFall.content.Load<Texture2D>("Images/fireworkAnimation"), 5, 6);
        public Vector2 location1;
        public Vector2 location2;
        public Vector2 location3;
        public Vector2 location4;

        string textLevel1Complete = "Level 1 Completed!";
        
        public Level1CompleteState(SkyFall skyFall, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(skyFall, graphicsDevice, contentManager)
        {
            var buttonBox = contentManager.Load<Texture2D>("Controls/Button");
            var buttonFont = Fonts.Font;

            var continueButton = new Button(buttonBox, buttonFont) //Button to go to next level
            {
                Position = new Vector2((SkyFall.ScreenSize.X / 2 - 50), (SkyFall.ScreenSize.Y / 2) - 50),
                Text = "Next Level",
            };
            continueButton.Click += ContinueButton_Click;

            var menuButton = new Button(buttonBox, buttonFont) //Button to go back to main menu
            {
                Position = new Vector2((SkyFall.ScreenSize.X / 2 - 50), (SkyFall.ScreenSize.Y / 2)),
                Text = "Main Menu",
            };
            menuButton.Click += MenuButton_Click;

            var exitButton = new Button(buttonBox, buttonFont) //Button to exit the game
            {
                Position = new Vector2((SkyFall.ScreenSize.X / 2 - 50), (SkyFall.ScreenSize.Y / 2) + 50),
                Text = "Exit",
            };
            exitButton.Click += ExitButton_Click;

            _components = new List<Component>()
            {
                continueButton,
                menuButton,
                exitButton
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }
            //Draws the text for level 1 completion
            spriteBatch.DrawString(Fonts.GameOverFont, textLevel1Complete, new Vector2(550, SkyFall.ScreenSize.Y / 2 - 250), Color.Black);
            location1 = new Vector2(1720, 880);
            location2 = new Vector2(100, 880);
            location3 = new Vector2(1720, 100);
            location4 = new Vector2(100, 100);
            firework.Draw(spriteBatch, location1);
            firework.Draw(spriteBatch, location2);
            firework.Draw(spriteBatch, location3);
            firework.Draw(spriteBatch, location4);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }

            firework.Update(); //Updates the firework animation
        }

        private void ContinueButton_Click(object sender, EventArgs e) //Changes the state to Level 2
        {
            _skyFall.ChangeState(new Level2State(_skyFall, _graphicDevice, _content));
        }

        private void MenuButton_Click(object sender, EventArgs e) //Changes the state to the main menu
        {
            _skyFall.ChangeState(new MenuState(_skyFall, _graphicDevice, _content));
        }

        private void ExitButton_Click(object sender, EventArgs e) //Exits the game
        {
            _skyFall.Exit();
        }
    }
}
