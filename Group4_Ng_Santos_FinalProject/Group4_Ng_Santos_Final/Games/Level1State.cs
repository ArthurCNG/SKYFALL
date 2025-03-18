using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Group4_Ng_Santos_Final.Games
{
    public class Level1State : State
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Vector2 _position;

        LevelManager _levelManager;

        private bool _isGameOver = false;
        private double gameOverTimer = 0;

        private bool _isLevelComplete = false;
        private double levelCompleteTimer = 0;

        Airplane _airplane;

        public static Song LevelOneMusic { get; set; }

        public Level1State(SkyFall skyFall, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(skyFall, graphicsDevice, contentManager)
        {
            EntityManager.FuelAmount = 100; // Set the fuel amount to 100
            Controls.EnableControls(); // Enable the controls

            foreach (var entity in EntityManager.entities)
            {
                entity.IsActive = false;
            }

            LevelOneMusic = _content.Load<Song>("Audios/levelOneMusic"); // Load the level one music
            MediaPlayer.Play(LevelOneMusic); 
            MediaPlayer.IsRepeating = true;
            _airplane = new Airplane();
            _levelManager = new LevelManager();
            _spriteBatch = new SpriteBatch(_graphicDevice);
            _position = new Vector2(SkyFall.ScreenSize.X / 2, SkyFall.ScreenSize.Y / 2);

            Art.Load(_content);
            Fonts.Load(_content);

            _airplane = new Airplane();

            Level level1 = new LevelOne("Level One"); // Create a new level

            level1.LoadContent(_content); // Load the content of the level

            _levelManager.AddLevel(level1); // Add the level to the level manager

            _levelManager.LoadLevel(0); // Load the first level
            _isGameOver = false; // Set the game over to false
            _isLevelComplete = false; // Set the level complete to false
        }
        public override void Update(GameTime gameTime)
        {
            _airplane.Position = _position;
            
            Controls.UpdatePosition(ref _position); 
            EntityManager.Update(gameTime); 
            
            _airplane.Update(gameTime);

            if (EntityManager.FuelAmount <= 0)
            {
                // Make the airplane fall down and to the right and disable the controls
                Controls.DisableControls();
            }

            if (_isGameOver)
            {
                gameOverTimer += gameTime.ElapsedGameTime.TotalSeconds;

                if (gameOverTimer >= 3) // Gives time for the player to see the airplane fall
                {
                    _skyFall.ChangeState(new GameOverState(_skyFall, _graphicDevice, _content));
                    _levelManager.LevelClock = 60;
                }
            }

            if (_isLevelComplete) // If the level is complete
            {
                levelCompleteTimer += gameTime.ElapsedGameTime.TotalSeconds;
                foreach (var entity in EntityManager.entities) // Set the entities to inactive
                {
                    entity.IsActive = false;
                }
                if (levelCompleteTimer >= 3) // Gives time for the player to see the level complete message
                {
                    _skyFall.ChangeState(new Level1CompleteState(_skyFall, _graphicDevice, _content));
                    _levelManager.LevelClock = 60;
                }
            }

            if (EntityManager.isExplosion)
            {
                // If the airplane explodes, disable the controls and make the airplane fall down and to the right
                Controls.DisableControls();
                _isGameOver = true;
            }

            if (_isGameOver == false)
            {
                Controls.EnableControls();
            }
            _levelManager.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            EntityManager.Draw(spriteBatch);

            _levelManager.Draw(spriteBatch);
            if (_airplane != null)
            {
                _airplane.Draw(spriteBatch);
            }

            string textFuel = $"{_levelManager.CurrentLevelName()}: Fuel: {EntityManager.FuelAmount}%";
            string textScore = $"{_levelManager.CurrentLevelName()}: Score: {EntityManager.Score}";
            string textTime = $"{_levelManager.CurrentLevelName()}: Time: 00:{_levelManager.LevelClock}";
            string textFuelEnds = "You ran out of fuel!";
            string textGameOver = "Game Over!";
            string textTimeout = "Level Complete!";

            spriteBatch.DrawString(Fonts.Font, textFuel, new Vector2(100, 50), Color.Red);
            spriteBatch.DrawString(Fonts.Font, textScore, new Vector2(500, 50), Color.White);
            spriteBatch.DrawString(Fonts.Font, textTime, new Vector2(900, 50), Color.White);

            // If the fuel amount is less than or equal to 0, display the game over message
            if (EntityManager.FuelAmount <= 0)
            {
                spriteBatch.DrawString(Fonts.GameOverFont, textFuelEnds, new Vector2(250, SkyFall.ScreenSize.Y / 2 - 250), Color.White);
                spriteBatch.DrawString(Fonts.GameOverFont, textGameOver, new Vector2(400, SkyFall.ScreenSize.Y / 2 - 150), Color.Black);
                _isGameOver = true; // Set the game over to true
            }
            // If the level clock is equal to 0 displays the level complete message
            else if (_levelManager.LevelClock <= 0)
            {
                spriteBatch.DrawString(Fonts.GameOverFont, textTimeout, new Vector2(650, SkyFall.ScreenSize.Y / 2 - 250), Color.Black);
                _isLevelComplete = true;
            }
            spriteBatch.End();
        }
    }
}
