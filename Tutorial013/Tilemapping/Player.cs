using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorial013.Tilemapping; //chuck

namespace Tutorial013
{
    class Player
    {   
        

        private Vector2 position = new Vector2(64, 0);
        private Vector2 velocity;
        public Rectangle rectangle;
        public float agility = 5;

        //new Chuck
        public Vector2 playerDistance;
        private Viewport viewport;
        private int xOfSet_var;
        private Texture2D texture_Gun;
        private Vector2 position_gun;
        private float rotation_of_gun;
        public float maxHeight;

        public bool IsShooting { get; set; }
        Texture2D bulletTexture;

        public List<Bullet> bullets;

        public int timeBetweenShots = 360; // Thats 300 milliseconds
        int shotTimer = 0;

        private List<SoundEffect> list_soundeffects;
        // ---

        public int health;
        private bool sound;
        private bool nxt = false;
        private bool hasJumped = false;
        private bool dead = false;

        AnimationPlayer animationPlayer;

        Animation walkAnimation;
        Animation idleAnimation;
        Animation deathAnimation;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Player(Texture2D gun_texture, Texture2D bullet_Texture, List<SoundEffect> list_of_sound_Effects, bool _sound)
        {
            sound = _sound;
            health = 100;
            maxHeight = -9f;
            //chuck
            texture_Gun = gun_texture;
            this.bulletTexture = bullet_Texture;
            this.IsShooting = false;
            this.list_soundeffects = list_of_sound_Effects;

            bullets = new List<Bullet>();
            //
        }

        public int Health
        {
            get { return health; }
            set { health = value;  }
        }

        public bool hit (Rectangle rec)
        {
            if (rectangle.Intersects(rec))
            {
                return true;
            }
            else
                return false;
        }

        public bool isDead()
        {
            if (Health <= 0)
            {
                health = 0;
                dead = true;
                
                return true;
            }
            else
                return false;
        }

        public bool bulletHit(Vector2 vec)
        {
            if (rectangle.Intersects(new Rectangle((int)vec.X, (int)vec.Y, 10, 10)))
            {
                return true;
            }
            else
                return false;
        }

        public bool isEnemyCollide(Rectangle enemyRec)
        {
            if (rectangle.Intersects(enemyRec))
            {
                return true;
            }
            else
                return false;
        }


        public void NextLvl()
        {
            nxt = true;
        }

        //chuck
        public void xOfSet1()
        {
            xOfSet_var = 1720;
        }

        public void xOfSet2()
        {
            xOfSet_var = 800;
        }

        public void xOfSet3()
        {
            xOfSet_var = 1720;
        }

        public void xOfSet4()
        {
            xOfSet_var = 800;
        }
        //--

        public void Load(ContentManager Content)
        {
            walkAnimation = new Animation(Content.Load<Texture2D>("Walk2"), 36, 0.1f, true);
            idleAnimation = new Animation(Content.Load<Texture2D>("Idle2"), 36, 0.3f, true);
            deathAnimation = new Animation(Content.Load<Texture2D>("death_animation"), 36, 0.3f, false);
        }

        public void Update(GameTime gameTime, int level)
        {

            if (this.isDead())
            {
                list_soundeffects[1].CreateInstance().Play();
                list_soundeffects[6].CreateInstance().Play();
            }
            //chuck --
            int viewportHeight = 480;
            int viewportWidth = 800; //center X of game window
            MouseState mouse = Mouse.GetState();

            if (level == 2 || level == 4)
            {
                playerDistance.X = (mouse.X - position.X) - 23;
                playerDistance.Y = (mouse.Y - position.Y) - 30;
            }
            else if (level == 1 || level == 3)
            {
                if (position.X < (viewportWidth / 2))
                    {
                        playerDistance.X = (mouse.X - position.X) - 23;
                        playerDistance.Y = (mouse.Y - position.Y) - 30;
                    }
                    else if (position.X > xOfSet_var - (viewportWidth / 2))
                    {
                        if (xOfSet_var == 800)
                        {
                            playerDistance.X = (mouse.X - position.X) - 23;
                            playerDistance.Y = (mouse.Y - position.Y) - 30;
                        }
                        else if (xOfSet_var == 1720)
                        {
                            playerDistance.X = (mouse.X - (position.X - 920) - 23);
                            playerDistance.Y = (mouse.Y - position.Y) - 30;
                        }

                    }
                    else
                    {
                        playerDistance.X = (mouse.X - (viewportWidth / 2)) - 23;
                        playerDistance.Y = (mouse.Y - position.Y) - 30;
                    }
            }

            playerDistance.Normalize();

            rotation_of_gun = (float)Math.Atan2(
              (double)playerDistance.Y,
              (double)playerDistance.X);

            //shoot

            if (mouse.LeftButton == ButtonState.Pressed)
            {
                this.IsShooting = true;
            }
            else
            {
                this.IsShooting = false;
            }

            if (IsShooting)
            {
                shotTimer += gameTime.ElapsedGameTime.Milliseconds;

                if (shotTimer > timeBetweenShots)
                {
                    shotTimer = 0;

                    Bullet b = new Bullet(
                         bulletTexture,
                         new Vector2((position.X + 18), (position.Y + 14)),
                         playerDistance,
                         12, // The Speed
                         2000); // The active time in Milliseconds

                    bullets.Add(b);
                    var pew = list_soundeffects[0].CreateInstance();
                    pew.Volume = 0.4f;
                    if (sound)
                        pew.Play();
                }
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update(gameTime);

                if (bullets[i].TotalActiveTime > bullets[i].ActiveTime)
                    bullets.RemoveAt(i);

                if (bullets[i].isVisible == false)
                    bullets.RemoveAt(i);
            }

            //float temp = ((float)xOfSet_var) - position.X;
            //Console.WriteLine("mouse X position: " + mouse.X);
            //Console.WriteLine("mouse Y position: " + mouse.Y);
            Console.WriteLine("playyer X position: " + this.velocity.X);
            Console.WriteLine("Player Y position: " + position.Y);
            //Console.WriteLine("mousedistance X position: " + playerDistance.X);
            //Console.WriteLine("mousedistance Y position: " + playerDistance.Y);

            //Bullet Collider

            
            // --------------------------------------------------------------------------------

            if (nxt)
            {
                position = new Vector2(70, 240);
                nxt = false;
            }

            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y
                        , 40, 36);

            Input(gameTime);

            if (velocity.Y < 10)
                velocity.Y += 0.4f;

            if (velocity.X != 0)
                animationPlayer.PlayAnimation(walkAnimation);
            else if (velocity.X == 0)
                animationPlayer.PlayAnimation(idleAnimation);

            if (health <= 0)
            {
                animationPlayer.PlayAnimation(deathAnimation);
            }

        }

        private void Input(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                velocity.X = agility;
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
                velocity.X = agility * -1f;
            else
                velocity.X = 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                position.Y -= 5f;
                velocity.Y = maxHeight;
                hasJumped = true;
            }
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                //position.Y = newRectangle.Y - rectangle.Height - 2;
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
                hasJumped = false;
            }
            if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 2;
            }

            if (rectangle.TouchRightOf(newRectangle))
            {
                position.X = newRectangle.X + rectangle.Width + 2;
            }
            if (rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }
            /*
            if (position.X < 0) position.X = 0;
            if (position.X > xOffset - rectangle.Width) position.X = xOffset - rectangle.Width;
            if (position.Y < 0) position.Y = 1f;
            if (position.Y > yOffset - rectangle.Height) position.Y = yOffset - rectangle.Height;
            */
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            

            SpriteEffects flip = SpriteEffects.None;

            if (playerDistance.X >= 0)
                flip = SpriteEffects.None;
            else if (playerDistance.X < 0)
                flip = SpriteEffects.FlipHorizontally;

            animationPlayer.Draw(gameTime, spriteBatch, position, flip);

            spriteBatch.Draw(texture_Gun, new Vector2((position.X + 19), (position.Y + 25)), null,  Color.White, rotation_of_gun, new Vector2(texture_Gun.Width / 2, texture_Gun.Height / 2), 1.0f, SpriteEffects.None, 1.0f);

            foreach (Bullet b in bullets)
            {
                b.Draw(spriteBatch);
            }
        }
    }
}
