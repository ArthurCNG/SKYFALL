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

namespace Group4_Ng_Santos_Final.Games
{
    public class VictoryState : State
    {
        public List<Component> _components;
        public static SoundEffect VictoryMusic { get; set; }
        double score = 0;
        public AnimatedSprite firework = new AnimatedSprite(SkyFall.content.Load<Texture2D>("Images/fireworkAnimation"), 5, 6);
        public Vector2 location1;
        public Vector2 location2;
        public Vector2 location3;
        public Vector2 location4;
        public VictoryState(SkyFall skyFall, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(skyFall, graphicsDevice, contentManager)
        {
            VictoryMusic = _content.Load<SoundEffect>("Audios/victorySound"); // Load the victory sound
            VictoryMusic.Play(); // Play the victory sound

            var buttonBox = contentManager.Load<Texture2D>("Controls/Button");
            var buttonFont = Fonts.Font;

            var saveScoreButton = new Button(buttonBox, buttonFont) // Create a button to save the score
            {
                Position = new Vector2((SkyFall.ScreenSize.X / 2 - 50), (SkyFall.ScreenSize.Y / 2) - 50),
                Text = "Save Score",
            };
            saveScoreButton.Click += SaveScoreButton_Click;

            var menuButton = new Button(buttonBox, buttonFont) // Create a button to go back to the menu
            {
                Position = new Vector2((SkyFall.ScreenSize.X / 2 - 50), (SkyFall.ScreenSize.Y / 2)),
                Text = "About",
            };
            menuButton.Click += MenuButton_Click;

            var exitButton = new Button(buttonBox, buttonFont) // Create a button to exit the game
            {
                Position = new Vector2((SkyFall.ScreenSize.X / 2 - 50), (SkyFall.ScreenSize.Y / 2) + 50),
                Text = "Exit",
            };

            exitButton.Click += ExitButton_Click;

            _components = new List<Component>()
            {
                saveScoreButton,
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
            // Draw the firework animation
            location1 = new Vector2(1720, 880);
            location2 = new Vector2(100, 880);
            location3 = new Vector2(1720, 100);
            location4 = new Vector2(100, 100);
            firework.Draw(spriteBatch, location1);
            firework.Draw(spriteBatch, location2);
            firework.Draw(spriteBatch, location3);
            firework.Draw(spriteBatch, location4);

            // Draw the victory text
            spriteBatch.DrawString(Fonts.GameOverFont, "Victory!", new Vector2(800, SkyFall.ScreenSize.Y / 2 - 250), Color.Black);

            spriteBatch.End();
        }
        
        public override void Update(GameTime gameTime)
        {
            firework.Update(); // Update the firework animation
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }
        
        private void SaveScoreButton_Click(object sender, EventArgs e) // Save the score to a file and go to the score state
        {
            score = EntityManager.Score;
            SaveScoreToFile(score);
            _skyFall.ChangeState(new ScoreState(_skyFall, _graphicDevice, _content));
        }

        private void MenuButton_Click(object sender, EventArgs e) // Go back to the menu
        {
            _skyFall.ChangeState(new MenuState(_skyFall, _graphicDevice, _content));
        }

        private void ExitButton_Click(object sender, EventArgs e) // Exit the game
        {
            _skyFall.Exit();
        }

        private void SaveScoreToFile(double score) // Save the score to a file
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "scores.txt"); // File path in AppData
                                                                                                                                // Write the score and the date to the file
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"Score: {score}, Date: {DateTime.Now}");
            }
            EntityManager.score = 0; // Reset the score
        }
    }
}
