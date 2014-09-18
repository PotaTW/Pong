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

        Player[] players = new Player[4];

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

            Texture2D playerTextureVertical = Content.Load<Texture2D>("padle.png");
            Texture2D playerTextureHorizontal = Content.Load<Texture2D>("padleHorizontal.png");

            Texture2D ballTexture = Content.Load<Texture2D>("Ball.png");

            ScoreFontP1 = Content.Load<SpriteFont>("SpriteFont1");
            ScoreFontP2 = Content.Load<SpriteFont>("SpriteFont1");


            padEffect = Content.Load<SoundEffect>("Blopp");



            for (int x = 0; x < players.Length; x++ )
            {
                if (x == 0 || x == 1)
                {
                    players[x] = new Player(playerTextureVertical, screenRect);
                }
                if(x == 2 || x == 3)
                {
                    players[x] = new Player(playerTextureHorizontal, screenRect);
                }
            }




            //player = new Player(playerTextureVertical, screenRect);
            //player2 = new Player(playerTextureVertical, screenRect);
            //player3 = new Player(playerTextureHorizontal, screenRect);
            //player4 = new Player(playerTextureHorizontal, screenRect);

            ball = new Ball(ballTexture, screenRect);


            for (int x = 0; x < players.Length; x++)
            {
                if(x == 0)
                {
                    players[x].SetFlapStartPos(5, graphics.PreferredBackBufferHeight / 2);
                }
                if (x == 1)
                {
                    players[x].SetFlapStartPos(graphics.PreferredBackBufferWidth - 5 - (playerTextureVertical.Width), graphics.PreferredBackBufferHeight / 2);
                }
                if (x == 2)
                {
                    players[x].SetFlapStartPos(graphics.PreferredBackBufferHeight - 5 - (playerTextureVertical.Height), graphics.PreferredBackBufferWidth / 2);
                }
                if (x == 3)
                {
                    players[x].SetFlapStartPos(graphics.PreferredBackBufferWidth / 2, 5);
                }
            }
            //player.SetFlapStartPos(5, graphics.PreferredBackBufferHeight / 2);
            //player2.SetFlapStartPos(graphics.PreferredBackBufferWidth - 5 - (playerTextureVertical.Width), graphics.PreferredBackBufferHeight / 2);
            //player3.SetFlapStartPos(graphics.PreferredBackBufferHeight - 5 - (playerTextureVertical.Height), graphics.PreferredBackBufferWidth / 2);
            //player4.SetFlapStartPos(graphics.PreferredBackBufferWidth / 2, 5);

            

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


            for (int x = 0; x < 1; x++)
            {
                players[x].FlapBounds();
            }
            //player.FlapBounds(); //Passer på at den ikke går uttafor Bounds
            //player2.FlapBounds();

            ball.UpdateBallDirection(); //Om skytes sender ballen avgårde

            for (int x = 0; x < players.Length; x++)
            {
                players[0].UpdateFlapMove(Keys.Up);
                //players[x].UpdateFlapMovement();
            }

            //player.UpdateFlapMovement(); //Beveger flappen
            //player2.UpdateFlapMovement2();
            //player3.UpdateFlapMovement3();
            //player4.UpdateFlapMovement4();

            ball.SetScore();

            
            ball.BallStartPosition(players[0].GetPaddleBounds()); //Setter ballen til star posisjon ///////////////////////////////////

            BallPaddleIntersect();

            ball.UpperWallCollision();
            ball.LowerWallCollision();

            base.Update(gameTime);
        }

        public void BallPaddleIntersect()
        {

            if (players[0].GetUpperPaddleBounds().Intersects(ball.GetBallBounce()))
            {
                ball.UpperRightPadCollision();
                padEffect.Play();
            }
            if (players[0].GetLowerPaddleBounds().Intersects(ball.GetBallBounce()))
            {
                ball.LowerRightPadCollision();
                padEffect.Play();
            }


            if (players[0].GetUpperPaddleBounds().Intersects(ball.GetBallBounce()))
            {
                ball.UpperLeftPadCollision();
                padEffect.Play();
            }
            if (players[0].GetLowerPaddleBounds().Intersects(ball.GetBallBounce()))
            {
                ball.LowerLeftPadCollision();
                padEffect.Play();
            }


            //if (player2.GetUpperPaddleBounds().Intersects(ball.GetBallBounce()))
            //{
            //    ball.UpperRightPadCollision();
            //    padEffect.Play();
            //}
            //if (player2.GetLowerPaddleBounds().Intersects(ball.GetBallBounce()))
            //{
            //    ball.LowerRightPadCollision();
            //    padEffect.Play();
            //}


            //if (player.GetUpperPaddleBounds().Intersects(ball.GetBallBounce()))
            //{
            //    ball.UpperLeftPadCollision();
            //    padEffect.Play();
            //}
            //if (player.GetLowerPaddleBounds().Intersects(ball.GetBallBounce()))
            //{
            //    ball.LowerLeftPadCollision();
            //    padEffect.Play();
            //}

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

            for (int x = 0; x < players.Length; x++)
            {
                players[x].Draw(spriteBatch);
            }

            //player.Draw(spriteBatch);
            //player2.Draw(spriteBatch);
            //player3.Draw(spriteBatch);
            //player4.Draw(spriteBatch);
            
            ball.Draw(spriteBatch);

            spriteBatch.DrawString(ScoreFontP1, "P2 Score: " + ball.Player1Score, new Vector2(30,10), Color.Blue);
            spriteBatch.DrawString(ScoreFontP2,"P2 Score: " + ball.Player2Score, new Vector2(screenRect.Width - 150, 10), Color.Red);



            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
