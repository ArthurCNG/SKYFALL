using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4_Ng_Santos_Final.Games
{
    public static class Fonts // Class for the fonts used in the game
    {
        public static SpriteFont Font { get; private set; }
        public static SpriteFont GameOverFont { get; private set; }

        public static void Load(ContentManager content) // Load the fonts
        {
            Font = content.Load<SpriteFont>("Fonts/infoFont");
            GameOverFont = content.Load<SpriteFont>("Fonts/gameOver");
        }
    }
}
