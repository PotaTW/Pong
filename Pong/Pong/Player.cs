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
    class Player
    {
        Texture2D flapTexture;
        Vector2 flapPosition;
        Vector2 flapMotion;

        KeyboardState keyboardState;

        int flapSpeed = 8;

        Rectangle screenBounds;

        public Player()
        {

        }

        public Player(Texture2D texture, Rectangle screenSize)
        {
            this.flapTexture = texture;
            this.screenBounds = screenSize;
        }

        public void FlapBounds()
        {
            if(flapPosition.Y <= 0)
            {
                flapPosition.Y = 0;
            }
            if (flapPosition.Y >= screenBounds.Height - flapTexture.Height)
            {
                flapPosition.Y = screenBounds.Height - flapTexture.Height;
            }

        }

        public void UpdateFlapMove(Keys UpKey, Keys DownKey) //Keypressed, needs more work
        {
            flapMotion = Vector2.Zero;
            
            Keys upKey = UpKey;
            Keys downKey = DownKey;
            
            if(upKey)
            {
                flapMotion.Y += -1;
            }
            else if(downKey)
            {
                flapMotion.Y += 1;
            }
            
            if()
        }
        public void UpdateFlapMovement()
        {
            flapMotion = Vector2.Zero;

            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                flapMotion.Y += -1;
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                flapMotion.Y += 1;
            }

            flapMotion.Y *= flapSpeed;

            flapPosition += flapMotion;
        }

        public void UpdateFlapMovement2()
        {
            flapMotion = Vector2.Zero;

            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W))
            {
                flapMotion.Y += -1;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                flapMotion.Y += 1;
            }

            flapMotion.Y *= flapSpeed;

            flapPosition += flapMotion;
        }

        public void UpdateFlapMovement3()
        {
            flapMotion = Vector2.Zero;

            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.F))
            {
                flapMotion.X += -1;
            }

            if (keyboardState.IsKeyDown(Keys.H))
            {
                flapMotion.X += 1;
            }

            flapMotion.Y *= flapSpeed;

            flapPosition += flapMotion;
        }

        public void UpdateFlapMovement4()
        {
            flapMotion = Vector2.Zero;

            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.NumPad4))
            {
                flapMotion.X += -1;
            }

            if (keyboardState.IsKeyDown(Keys.NumPad6))
            {
                flapMotion.X += 1;
            }

            flapMotion.Y *= flapSpeed;

            flapPosition += flapMotion;
        }


        public void SetFlapStartPos(int setXPos, int setYPos)////////////////SetyPos
        {
            flapPosition.X = setXPos;
            flapPosition.Y = setYPos;
        }


        public Rectangle GetUpperPaddleBounds()
        {
            return new Rectangle((int)flapPosition.X, (int)flapPosition.Y, flapTexture.Width, flapTexture.Height - (flapTexture.Height / 2));
        }
      
        public Rectangle GetLowerPaddleBounds()
        {
            return new Rectangle((int)flapPosition.X, (int)flapPosition.Y + (flapTexture.Height / 2), flapTexture.Width, flapTexture.Height);
        }

        public Rectangle GetPaddleBounds()
        {
            return new Rectangle((int)flapPosition.X, (int)flapPosition.Y, flapTexture.Width, flapTexture.Height);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(flapTexture, flapPosition, Color.White);
        }
       
    }
}
