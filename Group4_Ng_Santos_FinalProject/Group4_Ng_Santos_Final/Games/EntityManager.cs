using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace Group4_Ng_Santos_Final.Games
{
    public static class EntityManager
    {
        public static readonly List<Entity> entities = new List<Entity>();
        
        private static int _counter = 0;
        private static int fuelAmount = 100;
        public static int score = 0;
        private static double fuelTimer = 0;
        public static bool isExplosion = false;
        private static bool isGameOver = false;
        private static double gameOverTimer = 0;
        public static int Score { get { return score; } }
        public static int FuelAmount { get { return fuelAmount; } set { fuelAmount = value; } }
        public static int Count { get { return _counter; } }
        public static AnimatedSprite explosion = new AnimatedSprite(SkyFall.content.Load<Texture2D>("Images/explosionAnimation"), 3, 8);
        public static SoundEffect ExplosionSound { get; set; }
        public static SoundEffect BirdSound { get; set; }

        public static void Add(Entity entity) // Add an entity to the list
        {
            entities.Add(entity);
        }

        public static void Clear() // Clear the list of entities
        {
            _counter = 0;
            entities.Clear();
        }

        public static void Update(GameTime gameTime) // Update the entities
        {
            HandleCollisions();

            foreach (var entity in entities)
                entity.Update(gameTime);

            // Update the fuel timer
            fuelTimer += gameTime.ElapsedGameTime.TotalSeconds;

            // Decrease fuel every second
            if (fuelTimer >= 1)
            {
                DecreaseFuel(2); // Decrease fuel by 2
                fuelTimer = 0;   // Reset the timer
            }
            if (isGameOver) // If the game is over, update the explosion animation
            {
                explosion.Update();
                gameOverTimer += gameTime.ElapsedGameTime.TotalSeconds;
                Airplane.Instance.IsActive = false;

                foreach (var entity in entities) // Stop all entities from moving
                {
                    entity.Velocity = Vector2.Zero;
                }

                if (gameOverTimer >= 3) // If the game is over for 3 seconds, go to the game over state
                {
                    // Reset the game
                    isGameOver = false;
                    gameOverTimer = 0;
                    fuelAmount = 100;
                    _counter = 0;
                    score = 0;
                    isExplosion = false;
                    SkyFall.Instance.ChangeState(new GameOverState(SkyFall.Instance, SkyFall.GameGraphicsDevice, SkyFall.content));

                }
                return;
            }

            static void HandleCollisions() // Handle the collisions between the entities
            {
                if (entities.Count == 0) return;

                for (int i = 0; i < entities.Count; i++)
                {
                    if (entities[i].IsActive && IsColliding(Airplane.Instance, entities[i]))
                    {
                        // If the airplane collides with an entity
                        if (entities[i].Name == "fuel")
                        {
                            TakeFuel(i);
                            break;
                        }
                        else if (entities[i].Name == "whiteBird")
                        {
                            CaptureBird(i);
                            score += 25;
                        }
                        else if (entities[i].Name == "blueBird")
                        {
                            CaptureBird(i);
                            score += 200;
                        }
                        else if (entities[i].Name == "brownBird")
                        {
                            CaptureBird(i);
                            score += 50;
                        }
                        else if (entities[i].Name == "thunderCloud")
                        {
                            DestroyAirplane();
                            isGameOver = true;
                            break;
                        }
                    }
                }
            }
        }

        private static void DestroyAirplane() // Destroy the airplane and play the explosion sound
        {
            if(isGameOver == false)
            {
                ExplosionSound = SkyFall.content.Load<SoundEffect>("Audios/explosionSound");
                ExplosionSound.Play();
            }
            isExplosion = true;
        }

        private static void TakeFuel(int i) // Take fuel from the fuel entity
        {
            entities.RemoveAt(i);
            fuelAmount += 30; // Increase fuel by 30
            if (fuelAmount > 100)
            {
                fuelAmount = 100;
            }
        }

        private static void CaptureBird(int i) // Capture the bird entity
        {
            if (isGameOver == false)
            {
                // Play the bird sound
                BirdSound = SkyFall.content.Load<SoundEffect>("Audios/birdSound");
                BirdSound.Play();
            }
            entities.RemoveAt(i); // Remove the bird entity
        }

        private static void DecreaseFuel(int amount) // Decrease the fuel amount
        {
            fuelAmount -= amount;
            if (fuelAmount < 0) 
            {
                // If the fuel amount is less than 0, set it to 0 to avoid negative values
                fuelAmount = 0;
            }
        }

        private static bool IsColliding(Entity a, Entity b) // Check if two entities are colliding
        {
            return !a.IsExpired && !b.IsExpired
                && a.BoundingBox.Intersects(b.BoundingBox);
        }

        public static void Draw(SpriteBatch spriteBatch) // Draw the entities
        {
            foreach (var entity in entities) // Draw all the entities
            {
                if (entity.IsActive)
                {
                    entity.Draw(spriteBatch);
                }
            }
            if (isExplosion) // Draw the explosion animation
            {
                var explosionLocation = Airplane.Instance.Position;

                // Add explosion to entities
                explosion.Draw(spriteBatch, explosionLocation);
                isExplosion = false;

                // Deactivate the airplane
                Airplane.Instance.IsActive = false;
            }
        }
    }
}