using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Group4_Ng_Santos_Final.Games
{
    public class MenuState : State
    {
        public static Song MenuMusic { get; set; }

        public List<Component> _components;
        
        public MenuState(SkyFall skyFall, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(skyFall, graphicsDevice, contentManager)
        {
            MenuMusic = _content.Load<Song>("Audios/menuMusic"); // Load the menu music
            MediaPlayer.Play(MenuMusic);
            MediaPlayer.IsRepeating = true;

            var buttonBox = contentManager.Load<Texture2D>("Controls/Button");
            var buttonFont = Fonts.Font;

            var playButton = new Button(buttonBox, buttonFont) // Create a new button for the play button
            {
                Position = new Vector2((SkyFall.ScreenSize.X / 2 - 50), (SkyFall.ScreenSize.Y/2) - 50),
                Text = "Play Game",
            };
            playButton.Click += PlayButton_Click; // Add an event handler for the play button

            var aboutButton = new Button(buttonBox, buttonFont) // Create a new button for the about button
            {
                Position = new Vector2((SkyFall.ScreenSize.X / 2 - 50), (SkyFall.ScreenSize.Y / 2)),
                Text = "About",
            };
            aboutButton.Click += AboutButton_Click; // Add an event handler for the about button

            var helpButton = new Button(buttonBox, buttonFont) // Create a new button for the help button
            {
                Position = new Vector2((SkyFall.ScreenSize.X / 2 - 50), (SkyFall.ScreenSize.Y / 2) + 50),
                Text = "Help",
            };

            helpButton.Click += HelpButton_Click; // Add an event handler for the help button

            var scoreButton = new Button(buttonBox, buttonFont) // Create a new button for the top scores button
            {
                Position = new Vector2((SkyFall.ScreenSize.X / 2 - 50), (SkyFall.ScreenSize.Y / 2) + 100),
                Text = "TopScores",
            };
            scoreButton.Click += ScoreButton_Click; // Add an event handler for the top scores button

            var exitButton = new Button(buttonBox, buttonFont) // Create a new button for the exit button
            {
                Position = new Vector2((SkyFall.ScreenSize.X / 2 - 50), (SkyFall.ScreenSize.Y / 2) + 150),
                Text = "Exit",
            };

            exitButton.Click += ExitButton_Click; // Add an event handler for the exit button

            _components = new List<Component>()
            {
                playButton,
                aboutButton, 
                helpButton,
                scoreButton,
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
            // Draw the title of the game
            spriteBatch.DrawString(Fonts.GameOverFont, "SKYFALL", new Vector2(SkyFall.ScreenSize.X / 2 - 150, SkyFall.ScreenSize.Y / 2 - 250), Color.Red);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components) // Update the components
            {
                component.Update(gameTime);
            }
        }

        private void PlayButton_Click(object sender, EventArgs e) // Event handler for the play button
        {
            _skyFall.ChangeState(new Level1State(_skyFall, _graphicDevice, _content));
        }

        private void AboutButton_Click(object sender, EventArgs e) // Event handler for the about button
        {
            _skyFall.ChangeState(new AboutState(_skyFall, _graphicDevice, _content));
        }

        private void ScoreButton_Click(object sender, EventArgs e) // Event handler for the top scores button
        {
            _skyFall.ChangeState(new ScoreState(_skyFall, _graphicDevice, _content)); 
        }

        private void HelpButton_Click(object sender, EventArgs e) // Event handler for the help button
        {
            _skyFall.ChangeState(new HelpState(_skyFall, _graphicDevice, _content));
        }

        private void ExitButton_Click(object sender, EventArgs e) // Event handler for the exit button
        {
            _skyFall.Exit();
        }
    }
}
