using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Group4_Ng_Santos_Final.Games
{
    public class Airplane : Entity
    {
        public static Airplane Instance { get; private set; }
        
        public Airplane() //constructor
        {
            Instance = this;

            image = Art.Airplane;
            _position = SkyFall.ScreenSize / 2;
            //_position = new Vector2(SkyFall.ScreenSize.X / 2, SkyFall.ScreenSize.Y / 2);
            Radius = 20;
            _name = "airplane";

            CalculateBoundingBox();
        }

        public override void Update(GameTime gameTime)
        {
            CalculateBoundingBox();
        }
    }
}
