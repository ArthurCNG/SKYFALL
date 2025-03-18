using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Group4_Ng_Santos_Final.Games
{
    public static class Art
    {
        public static Texture2D SkyBackground { get; private set; }
        public static Texture2D BlueBird { get; private set; }
        public static Texture2D BrownBird { get; private set; }
        public static Texture2D WhiteBird { get; private set; }
        public static Texture2D ThunderCloud { get; private set; }
        public static Texture2D Airplane { get; private set; }
        public static Texture2D Fuel { get; private set; }
        public static Texture2D Wasd { get; private set; }
        public static Texture2D ArrowKeys { get; private set; }
        public static Texture2D GreyBackground { get; private set; }

        public static void Load(ContentManager content) //loads all the images
        {
            SkyBackground = content.Load<Texture2D>("Images/sky");
            BlueBird = content.Load<Texture2D>("Images/blueBird");
            BrownBird = content.Load<Texture2D>("Images/brownBird");
            WhiteBird = content.Load<Texture2D>("Images/whiteBird");
            ThunderCloud = content.Load<Texture2D>("Images/thunderCloud");
            Airplane = content.Load<Texture2D>("Images/airplane");
            Fuel = content.Load<Texture2D>("Images/fuel");
            Wasd = content.Load<Texture2D>("Images/wasdkeys");
            ArrowKeys = content.Load<Texture2D>("Images/arrowKeys");
            GreyBackground = content.Load<Texture2D>("Images/greySky");
        }
    }
}