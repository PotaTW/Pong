#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
#endregion

namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D Background;

        SpriteFont ScoreFontP1;
        SpriteFont ScoreFontP2;

        SoundEffect padEffect;

        Player player;
        Player player2;
        Ball ball;

        Rectangle screenRect;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;


            screenRect = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
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

            Background = Content.Load<Texture2D>("Background.png");

            Texture2D playerTexture = Content.Load<Texture2D>("padle.png");

            Texture2D ballTexture = Content.Load<Texture2D>("Ball.png");

            ScoreFontP1 = Content.Load<SpriteFont>("SpriteFont1");
            ScoreFontP2 = Content.Load<SpriteFont>("SpriteFont1");


            padEffect = Content.Load<SoundEffect>("Blopp");

            player = new Player(playerTexture, screenRect);
            player2 = new Player(playerTexture, screenRect);

            ball = new Ball(ballTexture, screenRect);

            player.SetFlapStartPos(5);
            player2.SetFlapStartPos(graphics.PreferredBackBufferWidth - 5 - (playerTexture.Width));

            

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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

            // TODO: Add your update logic here
            
           
            player.FlapBounds(); //Passer på at den ikke går uttafor Bounds
            player2.FlapBounds();

            ball.UpdateBallDirection(); //Om skytes sender ballen avgårde

            player.UpdateFlapMovement(); //Beveger flappen
            player2.UpdateFlapMovement2();

            ball.SetScore();

            ball.BallStartPosition(player.GetPaddleBounds()); //Setter ballen til star posisjon ///////////////////////////////////

            BallPaddleIntersect();

            ball.UpperWallCollision();
            ball.LowerWallCollision();

            base.Update(gameTime);
        }

        public void BallPaddleIntersect()
        {
            if (player2.GetUpperPaddleBounds().Intersects(ball.GetBallBounce()))
            {
                ball.UpperRightPadCollision();
                padEffect.Play();
            }
            if (player2.GetLowerPaddleBounds().Intersects(ball.GetBallBounce()))
            {
                ball.LowerRightPadCollision();
                padEffect.Play();
            }
            

            if (player.GetUpperPaddleBounds().Intersects(ball.GetBallBounce()))
            {
                ball.UpperLeftPadCollision();
                padEffect.Play();
            }
            if (player.GetLowerPaddleBounds().Intersects(ball.GetBallBounce()))
            {
                ball.LowerLeftPadCollision();
                padEffect.Play();
            }

        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.Draw(Background, screenRect, Color.White);

            player.Draw(spriteBatch);
            player2.Draw(spriteBatch);
            
            ball.Draw(spriteBatch);

            spriteBatch.DrawString(ScoreFontP1, "P2 Score: " + ball.Player1Score, new Vector2(30,10), Color.Blue);
            spriteBatch.DrawString(ScoreFontP2,"P2 Score: " + ball.Player2Score, new Vector2(screenRect.Width - 150, 10), Color.Red);



            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
