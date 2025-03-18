using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4_Ng_Santos_Final.Games
{
    public abstract class Entity // Base class for all game entities
    {
        protected Texture2D image;

        protected Color color = Color.White;

        protected string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        protected Vector2 _velocity;
        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        protected Vector2 _position;

        public Vector2 Position 
        {
            get { return _position; }
            set
            {
                _position = value;
                CalculateBoundingBox(); // Update the bounding box when the position changes
            }
        }
        protected float _angle = 0f;
        public float Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }

        protected float _scale = 1f;

        protected float Orientation = 0f;
        public float Radius = 20;           // used for circular collision detection
        public bool IsExpired = false;      // true if the entity was destroyed and should be deleted.
        
        public bool IsActive = true;

        public Vector2 Size
        {
            get
            {
                return image == null ? Vector2.Zero :
                    new Vector2(image.Width, image.Height);
            }
        }

        protected Rectangle _boundingBox;
        public Rectangle BoundingBox
        {
            get
            {
                return _boundingBox;
            }
        }

        protected void CalculateBoundingBox()
        {
            // Calculate the bounding box of the object
            _boundingBox = new Rectangle((int)_position.X, (int)_position.Y, (int)(image.Width * _scale), (int)(image.Height * _scale));
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, null, color, Orientation, Size / 2f, _scale, 0, 0);
        }
    }
}

