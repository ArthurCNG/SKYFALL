using Group4_Ng_Santos_Final.Games;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Group4_Ng_Santos_Final
{
    public class SkyFall : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private LevelManager _levelManager;
        public static ContentManager content;
        private State currentState;
        private State nextState;
        public static Platform _platform = Platform.Windows;

        public static SkyFall Instance { get; private set; }
        public static Viewport Viewport
        {
            get
            {
                return Instance.GraphicsDevice.Viewport;
            }
        }
        public static Vector2 ScreenSize
        {
            get
            {
                return new Vector2(Viewport.Width, Viewport.Height);
            }
        }
        public static GameTime GameTime { get; private set; }
        public static GraphicsDevice GameGraphicsDevice
        {
            get
            {
                return Instance.GraphicsDevice;
            }
        }

        public SkyFall(Platform platform)
        {
            Instance = this;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _platform = platform;
            if (_platform == Platform.Android || _platform == Platform.iOS) // If the platform is Android or iOS, the mouse is not visible
            {
                IsMouseVisible = false;
            }
            else
            {
                IsMouseVisible = true;
            }
            content = Content;
            _levelManager = new LevelManager();
        }

        protected override void Initialize()
        {
            // Sets the screen size to 1920x1080
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();

            if (_platform == Platform.Android || _platform == Platform.iOS) // If the platform is Android or iOS, the touch input is enabled
            {
                // Enable touch input
                TouchPanel.EnabledGestures = GestureType.Tap | GestureType.DoubleTap
                    | GestureType.Flick | GestureType.FreeDrag;
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Art.Load(Content); // Loads the images
            Fonts.Load(Content); // Loads the fonts

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            currentState = new MenuState(this, GraphicsDevice, Content); // Sets the current state to the MenuState
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (nextState != null)
            {
                currentState = nextState;
                nextState = null;
            }

            currentState.Update(gameTime); // Updates the current state

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue); // Sets the background color
            _spriteBatch.Begin();
            _spriteBatch.Draw(Art.SkyBackground, new Vector2(0, 0), Color.White); // Adds the background image
            _spriteBatch.End();
            currentState.Draw(gameTime, _spriteBatch); // Draws the current state, it doesnt need the Begin and End because it is already in the State class

            base.Draw(gameTime);

        }
        //This method is to set the next State of the game
        public void ChangeState(State state)
        {
            nextState = state;
        }
    }
}

