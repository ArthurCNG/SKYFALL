using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Group4_Ng_Santos_Final.Games
{
    public class ScoreState : State
    {
        public List<Component> _components;
        
        public ScoreState(SkyFall skyFall, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(skyFall, graphicsDevice, contentManager)
        {
            var buttonBox = contentManager.Load<Texture2D>("Controls/Button");
            var buttonFont = Fonts.Font;

            var menuButton = new Button(buttonBox, buttonFont) //Button to go back to the main menu
            {
                Position = new Vector2((SkyFall.ScreenSize.X / 2 - 100), (SkyFall.ScreenSize.Y - 100)),
                Text = "Main Menu",
            };
            menuButton.Click += MenuButton_Click; //Event handler for the button

            var exitButton = new Button(buttonBox, buttonFont) //Button to exit the game
            {
                Position = new Vector2((SkyFall.ScreenSize.X / 2 + 100), (SkyFall.ScreenSize.Y - 100)),
                Text = "Exit",
            };
            exitButton.Click += ExitButton_Click; //Event handler for the button

            _components = new List<Component>()
            {
                menuButton,
                exitButton
            };

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Art.GreyBackground, new Vector2(0, 0), Color.White); //Background

            foreach (var component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            int scorePosition = 0;

            spriteBatch.DrawString(Fonts.GameOverFont, "Top Scores:", new Vector2(100, 100), Color.Black);
            foreach (var score in GetTopScores())
            {
                spriteBatch.DrawString(Fonts.GameOverFont, score, new Vector2(50, SkyFall.ScreenSize.Y / 2 - 250 + scorePosition), Color.Black);
                scorePosition += 100;
            }

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }

        public List<string> GetTopScores()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "scores.txt"); // File path in AppData
            List<string> scores = new List<string>();

            if (File.Exists(filePath)) // If the file exists, read the top 5 scores
            {
                var lines = File.ReadAllLines(filePath); // Read all the lines in the file 
                scores = lines.OrderByDescending(line => int.Parse(line.Split(',')[0].Split(':')[1].Trim())).Take(5).ToList(); // Order the scores in descending order and take the top 5
            }

            return scores;
        }

        private void MenuButton_Click(object sender, EventArgs e) //Event handler for the menu button
        {
            _skyFall.ChangeState(new MenuState(_skyFall, _graphicDevice, _content));
        }

        private void ExitButton_Click(object sender, EventArgs e) //Event handler for the exit button
        {
            _skyFall.Exit();
        }
    }
}
