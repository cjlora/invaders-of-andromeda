using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace Tutorial013.Tilemapping
{
    class Bullet
    {
        
        public Vector2 velocity;
        public Rectangle rectangle;

        public Rectangle bullet_hitbox;

        //chuck
        public Texture2D texture { get; set; }
        public Vector2 position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public int ActiveTime { get; set; }
        public int TotalActiveTime { get; set; }
        
        public Vector2 origin;

        public bool isVisible = true;
        //--

        //chuck
        public Bullet(Texture2D texture, Vector2 position, Vector2 direction, float speed, int activeTime)
        {
            this.texture = texture;
            this.position = position;
            this.Direction = direction;
            this.Speed = speed;
            this.ActiveTime = activeTime;

            this.TotalActiveTime = 0;
        }

        public void Update(GameTime gameTime)
        {
            this.position += Direction * Speed;

            this.TotalActiveTime += gameTime.ElapsedGameTime.Milliseconds;

            bullet_hitbox = new Rectangle((int)this.position.X, (int)this.position.Y
            , 14, 14); //size of bullet hitbox
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                 texture,
                 position,
                 null,
                 Color.White,
                 0f,
                 new Vector2(
                      texture.Width / 2,
                      texture.Height / 2), //origin
                 1.0f,
                 SpriteEffects.None,
                 1.0f);
        }

        //--
        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Bullet");
        }
        //chuck
        
        //--
    }
}
