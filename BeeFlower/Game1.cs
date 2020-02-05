using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace BeeFlower
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D beeGfx, flowerGfx, spiderGfx;
        Vector2 beePos, flowerPos, spiderPos;
        SpriteFont font;
        int score = 0;
        //Skapa en randomerare som jag kan slumpa tal med
        Random randomizer;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            randomizer = new Random(); //För att skapa slumptal

            //Placera ut biet
            beePos = new Vector2(GraphicsDevice.Viewport.Width/2, GraphicsDevice.Viewport.Height/2);

            //Placera ut en första blomma
            flowerPos.X = randomizer.Next(GraphicsDevice.Viewport.Width);
            flowerPos.Y = randomizer.Next(GraphicsDevice.Viewport.Height);

            //Placera ut fienden
            spiderPos = new Vector2(0, 0);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            beeGfx = Content.Load<Texture2D>("bee");
            flowerGfx = Content.Load<Texture2D>("lotus_2080");
            font = Content.Load<SpriteFont>("myFont");
            spiderGfx = Content.Load<Texture2D>("spider");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Förflytta biet
            KeyboardState kb = Keyboard.GetState();

            if (kb.IsKeyDown(Keys.Left))
                beePos.X -= 5;
            if (kb.IsKeyDown(Keys.Right))
                beePos.X += 5;
            if (kb.IsKeyDown(Keys.Up))
                beePos.Y -= 5;
            if (kb.IsKeyDown(Keys.Down))
                beePos.Y += 5;

            //Se om biet når fram till mitten av blomman
            if (Math.Abs(beePos.X - flowerPos.X) < 10 && Math.Abs(beePos.Y - flowerPos.Y) < 10)
            {
                score++;
                flowerPos.X = randomizer.Next(GraphicsDevice.Viewport.Width);
                flowerPos.Y = randomizer.Next(GraphicsDevice.Viewport.Height);
            }

            //Förflytta spindeln
            if (beePos.X > spiderPos.X) spiderPos.X++;
            if (beePos.X < spiderPos.X) spiderPos.X--;
            if (beePos.Y > spiderPos.Y) spiderPos.Y++;
            if (beePos.Y < spiderPos.Y) spiderPos.Y--;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(flowerGfx, flowerPos - new Vector2(flowerGfx.Width/2, flowerGfx.Height/2), Color.White);
            spriteBatch.Draw(beeGfx, beePos - new Vector2(beeGfx.Width / 2, beeGfx.Height / 2), Color.White);
            spriteBatch.Draw(spiderGfx, spiderPos - new Vector2(128, 72), Color.White);
            spriteBatch.DrawString(font, "Poäng: " + score, new Vector2(40, 40), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
