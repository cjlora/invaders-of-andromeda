﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial013
{
    class Tiles
    {
        protected Texture2D texture;

        private Rectangle rectangle;
        private Rectangle unrectangle = new Rectangle (0,0,0,0);
        public Rectangle Rectangle
        {
            get { return rectangle; }
            protected set { rectangle = value; }
        }

        private static ContentManager content;
        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
        public void UnDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, unrectangle, Color.White);
        }
    }

    class CollisionTiles : Tiles
    {
        public CollisionTiles (int i, Rectangle newRectangle)
        {
            texture = Content.Load<Texture2D>("Tile" + i);
            this.Rectangle = newRectangle;
        }
    }
}
