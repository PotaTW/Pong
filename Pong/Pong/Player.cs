﻿using System;
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


        public void SetFlapStartPos(int setXPos)
        {
            flapPosition.X = setXPos;
            flapPosition.Y = (screenBounds.Height / 2) - (flapTexture.Height / 2);
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