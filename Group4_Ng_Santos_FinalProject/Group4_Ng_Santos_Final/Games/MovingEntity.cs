using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4_Ng_Santos_Final.Games
{
    public class MovingEntity : Entity
    {
        private Random _random;

        public MovingEntity(Texture2D texture, Vector2 startPosition, Vector2 velocity, float angle, string name)
        {
            this.image = texture;
            _position = startPosition;
            _random = new Random();
            _velocity = velocity;
            _angle = angle;
            _name = name;
            _position = new Vector2(SkyFall.ScreenSize.X + 200, _random.NextFloat(0.00f, 0.9f) * SkyFall.ScreenSize.Y);

            CalculateBoundingBox();
        }

        public override void Update(GameTime gameTime)
        {
            Orientation += _angle;
            _position += _velocity;
            CalculateBoundingBox();

            _velocity.X *= 1;

            if (_position.Y < 0 || _position.Y > SkyFall.ScreenSize.Y - (image.Height * 1.1))
            {
                _velocity.Y *= -1;
            }
        }

        public void WasTaken()
        {
            IsExpired = true;
            IsActive = false;
        }
    }

}
