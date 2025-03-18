using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Group4_Ng_Santos_Final.Games
{
    public class Level2State : State
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

        public static Song LevelTwoMusic { get; set; }
        public static SoundEffect ExplosionSound { get; set; }

        public Level2State(SkyFall skyFall, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(skyFall, graphicsDevice, contentManager)
        {
            EntityManager.FuelAmount = 100; // Reset fuel amount
            Controls.EnableControls(); // Enable controls

            foreach (var entity in EntityManager.entities)
            {
                entity.IsActive = false;
            }

            LevelTwoMusic = _content.Load<Song>("Audios/levelTwoMusic"); //Changes the music to Level 2 music
            MediaPlayer.Play(LevelTwoMusic);
            MediaPlayer.IsRepeating = true;
            _airplane = new Airplane();
            _levelManager = new LevelManager();
            _spriteBatch = new SpriteBatch(_graphicDevice);
            _position = new Vector2(SkyFall.ScreenSize.X / 2, SkyFall.ScreenSize.Y / 2);

            Art.Load(_content);
            Fonts.Load(_content);

            _airplane = new Airplane();

            Level level2 = new LevelTwo("Level Two");

            level2.LoadContent(_content);

            _levelManager.AddLevel(level2);

            _levelManager.LoadLevel(0);
            _isGameOver = false;
            _isLevelComplete = false;
        }
        // The update method and PostUpdate and Draw are basically the same as the Level1State
        public override void Update(GameTime gameTime)
        {
            _airplane.Position = _position;

            Controls.UpdatePosition(ref _position);
            EntityManager.Update(gameTime);

            _airplane.Update(gameTime);

            if (EntityManager.FuelAmount <= 0)
            {
                Controls.DisableControls();
            }

            if (_isGameOver)
            {
                gameOverTimer += gameTime.ElapsedGameTime.TotalSeconds;

                if (gameOverTimer >= 3)
                {
                    _skyFall.ChangeState(new GameOverState(_skyFall, _graphicDevice, _content));
                    _levelManager.LevelClock = 60;
                }
            }

            if (_isLevelComplete)
            {
                levelCompleteTimer += gameTime.ElapsedGameTime.TotalSeconds;
                foreach (var entity in EntityManager.entities)
                {
                    entity.IsActive = false;
                }
                if (levelCompleteTimer >= 3)
                {
                    _skyFall.ChangeState(new VictoryState(_skyFall, _graphicDevice, _content));
                    _levelManager.LevelClock = 60;
                }
            }

            if (EntityManager.isExplosion)
            {
                Controls.DisableControls();
                _isGameOver = true;
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

            if (EntityManager.FuelAmount <= 0)
            {
                spriteBatch.DrawString(Fonts.GameOverFont, textFuelEnds, new Vector2(250, SkyFall.ScreenSize.Y / 2 - 250), Color.White);
                spriteBatch.DrawString(Fonts.GameOverFont, textGameOver, new Vector2(400, SkyFall.ScreenSize.Y / 2 - 150), Color.Black);
                _isGameOver = true;
            }
            else if (_levelManager.LevelClock <= 0)
            {
                spriteBatch.DrawString(Fonts.GameOverFont, textTimeout, new Vector2(650, SkyFall.ScreenSize.Y / 2 - 250), Color.Black);
                _isLevelComplete = true;
            }

            spriteBatch.End();
        }
    }
}