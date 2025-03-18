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
    public class LevelOne : Level
    {
        //Different velocities for the entities
        private Vector2 _whiteVelocity = new Vector2(-5, 3);
        private Vector2 _brownVelocity = new Vector2(-4, -3);
        private Vector2 _cloudVelocity = new Vector2(-1, 0);
        private Vector2 _fuelVelocity = new Vector2(-2, 0);
        private float _angle = 0f;

        // Random number generator
        private Random _random = new Random();

        // Spawn Timers
        private double _timeSinceLastSpawn = 0;
        private double _spawnInterval = 1; // 1 second
        private double _timeSinceLastFuelSpawn = 0;
        private double _fuelSpawnInterval = 6; // 6 seconds

        // Constructor
        public LevelOne(string levelName)
        {
            Name = levelName;
        }
        
        // Load Content
        public override void LoadContent(ContentManager content)
        {
            // Load level-specific content
            AddRandomEntity();
        }

        public override void UnloadContent()
        {
            EntityManager.Clear(); // Clear the entity manager
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
            int entityType = _random.Next(3); // 3 types of entities excluding fuel
            MovingEntity entity = null;

            switch (entityType) // Randomly choose an entity to spawn, Since is Level One it should be easy
            {
                case 0:
                    entity = new MovingEntity(Art.WhiteBird, Vector2.One, _whiteVelocity, _angle, "whiteBird");
                    break;
                case 1:
                    entity = new MovingEntity(Art.BrownBird, Vector2.One, _brownVelocity, _angle, "brownBird");
                    break;
                case 2:
                    entity = new MovingEntity(Art.ThunderCloud, Vector2.One, _cloudVelocity, _angle, "thunderCloud");
                    break;
            }

            if (entity != null) // If an entity was created, add it to the entity manager
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
