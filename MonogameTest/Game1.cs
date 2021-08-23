using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonogameTest
{
    public class Game1 : Game
    {
        // Store ball image into memory
        Texture2D ballTexture;
        Vector2 ballPosition;
        float ballSpeed;

        /* TO ADD CONTENT:
         * 1. Open the Content folder
         * 2. Right click Content.mgcb, Open with mgcb-editor-wpf
         * 3. From the toolbar, click Add Exisiting Item
         * 4. Select item to add
         * 5. Select "Copy to directory"
         * 6. Save and exit editor
         */

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        // Initialise is called before the main game loop (Update/Draw), for querying any required services and load any non-graphical content
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // Set ball position to the center of the screen (i assume preferredbackbufferwidth/height refers to the window dimensions)
            ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                                       _graphics.PreferredBackBufferHeight / 2);
            ballSpeed = 100f;

            base.Initialize();
        }

        // Only called once per game, this is used to load game content before the main game loop
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load Ball Texture
            ballTexture = Content.Load<Texture2D>("ball");

            // TODO: use this.Content to load your game content here
        }

        // Update is called multiple times per second, and is used to update the game state (e.g checking for collisions, gathering input, playing audio, etc.)
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // User Input
            var kstate = Keyboard.GetState(); // Fetch the current keyboard state (what keys are pressed, what aren't), stores the data under the kstate variable.

            if (kstate.IsKeyDown(Keys.Up)) // If up arrow key is pressed
                ballPosition.Y -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                // If up arrow is pressed, the ball moves using the assigned ballSpeed value.
                // The ballSpeed is multiplied by the amount of time sinze the last Update call. This is to ensure that the ball moves at the same speed regardless of framerates

            // Rinse and repeat for the other keys

            if (kstate.IsKeyDown(Keys.Down))
                ballPosition.Y += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Left))
                ballPosition.X -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Right))
                ballPosition.X += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Ensure the ball cannot pass the width or height of the screen
            if (ballPosition.X > _graphics.PreferredBackBufferWidth - ballTexture.Width / 2)
                ballPosition.X = _graphics.PreferredBackBufferWidth - ballTexture.Width / 2;
            else if (ballPosition.X < ballTexture.Width / 2)
                ballPosition.X = ballTexture.Width / 2;

            if (ballPosition.Y > _graphics.PreferredBackBufferHeight - ballTexture.Height / 2)
                ballPosition.Y = _graphics.PreferredBackBufferHeight - ballTexture.Height / 2;
            else if (ballPosition.Y < ballTexture.Height / 2)
                ballPosition.Y = ballTexture.Height / 2;

            base.Update(gameTime);
        }

        // Similar to the Update method, Draw is called multiple times per second but is used for drawing graphics instead.
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            // Draw texture
            // Bad code: _spriteBatch.Draw(ballTexture, ballPosition, Color.White). This centers the texture based on the top-left corner of the image, so itll be slightly off centre.
            _spriteBatch.Draw(
                ballTexture,    // The texture
                ballPosition,   // The drawing bounds on the screen (i.e the position on the screen)
                null,           // Optional. Region of the texture to be rendered. If null, it renders the whole texture.
                Color.White,    // The colour mask
                0f,             // The rotation of the texture
                new Vector2(ballTexture.Width / 2, ballTexture.Height / 2),     // Center of rotation. By default, this is 0,0
                Vector2.One,    // Drawing scaling
                SpriteEffects.None,     // Modificators for drawing
                0f              // A depth of the layer for the sprite
            );
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
