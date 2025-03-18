using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4_Ng_Santos_Final.Games
{
    public class Button : Component //Button class that inherits from the Component class
    {
        //This class is used to create buttons for the game
        //This makes it easier to create buttons for the game in the States classes
        private MouseState _currentMouse;
        private MouseState _previousMouse;
        private bool _isHovering;
        public bool IsClicked { get; private set; }
        private SpriteFont _font;
        private Texture2D _texture;
        public event EventHandler Click;
        public Rectangle Rectangle //Rectangle for the button
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }
        public Color Colour { get; set; }
        public Vector2 Position { get; set; }
        public string Text { get; set; }

        public Button(Texture2D texture, SpriteFont font) //Constructor for the button
        {
            _texture = texture;
            _font = font;
            Colour = Color.Black;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
        {
            var colour = Color.White;
            if (_isHovering) //If the mouse is hovering over the button, the button will change colour
            { 
                colour = Color.Gray; 
            }
            spriteBatch.Draw(_texture, Rectangle, colour);

            //Center the text
            var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
            var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);
            //Draw the text
            spriteBatch.DrawString(_font, Text, new Vector2(x, y), Colour);
        }

        public override void Update(GameTime gameTime)
        {
            //Check if the mouse is hovering over the button
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();
            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
            _isHovering = false;
            
            //If the mouse is hovering over the button and the button is clicked, the button will be clicked
            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
