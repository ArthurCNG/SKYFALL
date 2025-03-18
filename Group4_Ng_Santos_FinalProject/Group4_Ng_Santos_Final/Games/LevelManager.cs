using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Group4_Ng_Santos_Final.Games
{
    public class LevelManager
    {
        private List<Level> levels;
        private Level currentLevel = null;
        private  double levelTimer = 0; // Timer to track elapsed time for level
        public double LevelClock { get; set; } = 60;

        public LevelManager()
        {
            levels = new List<Level>();
        }

        public void AddLevel(Level level)
        {
            levels.Add(level);
        }

        public void LoadLevel(int index)
        {
            if (currentLevel != null)
            {
                currentLevel.UnloadContent();
            }
            currentLevel = levels[index];
        }

        public void Update(GameTime gameTime)
        {
            currentLevel?.Update(gameTime);
            levelTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (levelTimer >= 1)
            {
                DecreaseClock(); // Decrease the clock every second
                levelTimer = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentLevel?.Draw(spriteBatch);
        }

        public void AddEntities()
        {
            currentLevel?.AddRandomEntity();
        }

        public string CurrentLevelName() // Returns the name of the current level
        {
            if (currentLevel != null)
            {
                return currentLevel?.Name;
            }
            else
            {
                return "Unknown";
            }
        }

        private  void DecreaseClock() // Decrease the clock by 1 every second
        {
            LevelClock--; 
            if (LevelClock <= 0)
            {
                LevelClock = 0;
            }
        }
    }
}
