using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;


namespace Pong
{
    class Ball 
    {

        Texture2D ballTexture;
        Vector2 ballPosition;
        public Vector2 ballMotion;

        
        KeyboardState keyboardState;

        float ballSpeed = 4;

        public int Player1Score = 0;
        public int Player2Score = 0;

        bool IsActive = false;
        bool BallMoving = false;
        bool Player1ScoreBool = false;
        bool Player2ScoreBool = false;

        

        Rectangle screenBounds;

        
        public Ball()
        {

        }

        public Ball(Texture2D ballTexture, Rectangle screenBounds)
        {
            this.ballTexture = ballTexture;
            this.screenBounds = screenBounds;
        }



        public void BallStartPosition(Rectangle PadBounds)
        {
            if (BallMoving == false)
            {
                ballPosition.Y = PadBounds.Y + (PadBounds.Height / 2) - ballTexture.Height / 2;
                ballPosition.X = PadBounds.X + (PadBounds.Width);
            }
        }

        public void UpdateBallDirection()
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                IsActive = true;
            }

            if(IsActive == true)
            {

                ballMotion.X = ballSpeed;

                ballPosition += ballMotion;

                BallMoving = true;
            }

            BallOutOfBounds();
        }

        public void BallOutOfBounds()//////////////////////
        {
            if (ballPosition.X >= screenBounds.Width - (ballTexture.Width))
            {
                ballMotion = Vector2.Zero;
                IsActive = false;
                BallMoving = false;
                Player1ScoreBool = true;
            }
            if(ballPosition.X < 0)
            {
                ballMotion = Vector2.Zero;
                IsActive = false;
                BallMoving = false;
                Player2ScoreBool = true;
            }
        }

        public int SetScore()
        {

            if(Player1ScoreBool == true)
            {
                Player1Score++;
                Player1ScoreBool = false;
                ballSpeed = 4.0f;
                return Player1Score;
            }
            if(Player2ScoreBool == true)
            {
                Player2Score++;
                Player2ScoreBool = false;
                ballSpeed = 4.0f;
                return Player2Score;
            }
            return 0;

        }

        public void UpperRightPadCollision()
        {
            Random rand = new Random();
            int randomNum = rand.Next(-3, -1);
            ballSpeed *= -1;
            ballMotion.X = ballSpeed;
            ballMotion.Y = randomNum;
            ballPosition += ballMotion;
            ballSpeed -= 0.2f;
        }

        public void LowerRightPadCollision()
        {
            Random rand = new Random();
            int randomNum = rand.Next(1, 3);
            ballSpeed *= -1;
            ballMotion.X = ballSpeed;
            ballMotion.Y = randomNum;
            ballPosition += ballMotion;
            ballSpeed -= 0.2f;
        }

        public void UpperLeftPadCollision()
        {
            Random rand = new Random();
            int randomNum = rand.Next(-3, -1);
            ballSpeed *= -1;
            ballMotion.X = ballSpeed;
            ballMotion.Y = randomNum;
            ballPosition += ballMotion;
            ballSpeed += 0.2f;
        }

        public void LowerLeftPadCollision()
        {
            Random rand = new Random();
            int randomNum = rand.Next(1, 3);
            ballSpeed *= -1;
            ballMotion.X = ballSpeed;
            ballMotion.Y = randomNum;
            ballPosition += ballMotion;
            ballSpeed += 0.2f;
        }

        public void UpperWallCollision()
        {
            if(ballPosition.Y <= 0 && ballMotion.X < 0)
            {
                ballMotion.Y = 3; //bounce Down
                ballPosition += ballMotion;
            }
            if(ballPosition.Y <= 0 && ballMotion.X > 0)
            {
                ballMotion.Y = 3; //bounce Down
                ballPosition += ballMotion;
            }
        }

        public void LowerWallCollision()
        {
            if (ballPosition.Y + ballTexture.Height >= screenBounds.Height && ballMotion.X < 0)
            {
                ballPosition.Y = screenBounds.Height - ballTexture.Height;
                ballMotion.Y *= -1; //bounce Up
                ballPosition += ballMotion;
            }
            if (ballPosition.Y + ballTexture.Height >= screenBounds.Height && ballMotion.X > 0)
            {
                ballPosition.Y = screenBounds.Height - ballTexture.Height;
                ballMotion.Y *= -1; //bounce Up
                ballPosition += ballMotion;
            }
        }


        public Rectangle GetBallBounce()
        {
            return new Rectangle((int)ballPosition.X, (int)ballPosition.Y, ballTexture.Width, ballTexture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ballTexture, ballPosition, Color.White);
        }

    }
}
