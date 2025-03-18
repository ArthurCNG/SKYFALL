using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4_Ng_Santos_Final.Games
{
    public class LevelTwo : Level // This class used most of the same logic as level one, but with different entities and spawn intervals
    {
        private Vector2 _whiteVelocity = new Vector2(-5, 3);
        private Vector2 _brownVelocity = new Vector2(-4, -3);
        private Vector2 _blueVelocity = new Vector2(-8, 8); // new entity type for the level 2 with a higher velocity (its woth more points so it should be harder to catch)
        private Vector2 _cloudVelocity = new Vector2(-1, 0);
        private Vector2 _fuelVelocity = new Vector2(-2, 0);
        private float _angle = 0f;

        private Random _random = new Random();
        private double _timeSinceLastSpawn = 0;
        private double _spawnInterval = 0.5; // 0.5 second
        private double _timeSinceLastFuelSpawn = 0;
        private double _fuelSpawnInterval = 8; // 8 seconds

        public LevelTwo(string levelName)
        {
            Name = levelName;
        }
        
        public override void LoadContent(ContentManager content)
        {
            // Load level-specific content
            AddRandomEntity();
        }

        public override void UnloadContent()
        {
            // Unload level-specific content
            EntityManager.Clear();
        }

        public override void Update(GameTime gameTime)
        {
            // Update level-specific logic
            EntityManager.Update(gameTime);

            // Update the time since the last spawn
            _timeSinceLastSpawn += gameTime.ElapsedGameTime.TotalSeconds;
            _timeSinceLastFuelSpawn += gameTime.ElapsedGameTime.TotalSeconds;

            // Check if it's time to spawn a new entity
            if (_timeSinceLastSpawn >= _spawnInterval)
            {
                AddRandomEntity();
                _timeSinceLastSpawn = 0; // Reset the timer
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw level-specific content
            Rectangle rect = new Rectangle(0, 0, (int)SkyFall.ScreenSize.X, (int)SkyFall.ScreenSize.Y);
            EntityManager.Draw(spriteBatch);
        }

        public override void AddRandomEntity()
        {
            int entityType = _random.Next(5); // 4 types of entities excluding fuel
            MovingEntity entity = null;

            switch (entityType)
            {
                case 0:
                    entity = new MovingEntity(Art.WhiteBird, Vector2.One, _whiteVelocity, _angle, "whiteBird");
                    break;
                case 1:
                    entity = new MovingEntity(Art.BrownBird, Vector2.One, _brownVelocity, _angle, "brownBird");
                    break;
                case 2:
                    entity = new MovingEntity(Art.BlueBird, Vector2.One, _blueVelocity, _angle, "blueBird");
                    break;
                case 3:
                    entity = new MovingEntity(Art.ThunderCloud, Vector2.One, _cloudVelocity, _angle, "thunderCloud");
                    entity = new MovingEntity(Art.ThunderCloud, Vector2.One, _cloudVelocity, _angle, "thunderCloud");
                    break;
                case 4:
                    entity = new MovingEntity(Art.ThunderCloud, Vector2.One, _cloudVelocity, _angle, "thunderCloud");
                    entity = new MovingEntity(Art.ThunderCloud, Vector2.One, _cloudVelocity, _angle, "thunderCloud");
                    break;
            }

            if (entity != null)
            {
                EntityManager.Add(entity);
            }

            // Check if it's time to spawn a fuel entity
            if (_timeSinceLastFuelSpawn >= _fuelSpawnInterval)
            {
                entity = new MovingEntity(Art.Fuel, Vector2.One, _fuelVelocity, _angle, "fuel");

                EntityManager.Add(entity);
                _timeSinceLastFuelSpawn = 0; // Reset the fuel timer
            }
        }
    }
}
