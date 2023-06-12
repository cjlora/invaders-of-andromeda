using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial013.Tilemapping
{
    class Enemy
    {
        //Texture2D texture;
        Texture2D enemy_bullet_texture;
        public int type;
        public Vector2 position;
        Vector2 origin;
        public Vector2 velocity;
        public Vector2 player_position;
        public Vector2 enemy_player_distance;
        public float speed;

        public Rectangle _rectangle;

        public bool right, dead = false;
        float distance;
        float oldDistance;

        float timer = 0.2f;

        //enemy stuff
        public int health;
        //

        //bullet crap
        public List<Bullet> bullets;
        public bool IsShooting { get; set; }
        public int timeBetweenShots = 1000; // Thats 300 milliseconds
        public int shotTimer = 0;
        public Vector2 bullet_direction;
        public Vector2 bullet_direction_2;
        public int speed_of_bullet;
        public bool two_guns;
        public Bullet b;
        //

        //animation
        public int size;
        public AnimationPlayer animationPlayer;
        public Animation walkAnimation;

        public Animation deathAnimation;

        //ced
        bool up, down;


        public Enemy(int _type, Texture2D bullet_Texture)
        {
            type = _type;
            //texture = newwTexture;
            position = new Vector2(0,0);
            distance = 100;
            oldDistance = distance;
            velocity.X = -1f;
            up = true;
            down = false;

            this.enemy_bullet_texture = bullet_Texture;
            this.IsShooting = true;
            bullets = new List<Bullet>();
            speed_of_bullet = 4;
            health = 50;
            two_guns = false;
            speed = 1f;
        }

        public Vector2 mouseDistance;

        public void Update(Player player, GameTime gameTime)
        {
            enemy_player_distance.X = player.Position.X - this.position.X;
            enemy_player_distance.Y = player.Position.Y - this.position.Y;
            //position += velocity;



            if (!dead)
            {
                if (type == 8)
                {
                    _rectangle = new Rectangle((int)position.X + 95, (int)position.Y + 60
                        , size, size);
                }
                else if (type == 7)
                {
                    _rectangle = new Rectangle((int)position.X + 40, (int)position.Y + 40
                        , size, size);
                }
                else
                _rectangle = new Rectangle((int)position.X, (int)position.Y
                        , size, size); //NEW CODE
            }

            //if (velocity.X != 0)
            
            if (this.health <= 0)
            {
                animationPlayer.PlayAnimation(deathAnimation);
            }
            else animationPlayer.PlayAnimation(walkAnimation);


            origin = new Vector2(size / 2, size / 2); // NEW CODE

            if (type == 0)
            {
                bullet_direction = new Vector2(enemy_player_distance.X / 600, enemy_player_distance.Y / 600);
                velocity.X = 0f;
            }

            if (type == 1)
            {
                //if (distance <= 0)
                //{
                //    right = false;
                //    velocity.X = 1f;
                //}
                //else if (distance >= oldDistance)
                //{
                //    right = true;
                //    velocity.X = -1f;
                //}

                //if (right)
                //{
                //    distance += 1;
                //}
                //else
                //    distance -= 1;

                position += velocity;

                bullet_direction = new Vector2(this.velocity.X, 0);

                if (position.X > 1150)
                {
                    velocity.X = -1f;
                }
                else if (position.X < 900)
                {
                    velocity.X = 1f;
                }
            }
            else if (type == 2)
            {
                bullet_direction = new Vector2(-1, 0);
                bullet_direction_2 = new Vector2(1, 0);

                position += velocity;

                if (position.X >= 1185)
                {
                    velocity.X = -1.5f;
                }
                else if (position.X <= 862)
                {
                    velocity.X = 1.5f;
                }




            }
            else if (type == 3)
            {
                IsShooting = false;
                position += velocity;
                if (up)
                {
                    if (position.X == 1510)
                    {
                        velocity.X = 0;
                        velocity.Y = -1f;
                    }
                    if (position.Y == 290)
                    {
                        velocity.X = -1f;
                        velocity.Y = 0;
                    }
                    if (position.X == 1470)
                    {
                        velocity.X = 0;
                        velocity.Y = -1f;
                    }
                    if (position.Y == 250)
                    {
                        velocity.X = -1f;
                        velocity.Y = 0;
                    }
                    if (position.X == 1430)
                    {
                        velocity.X = 0;
                        velocity.Y = -1f;
                    }
                    if (position.Y == 210)
                    {
                        velocity.X = -1f;
                        velocity.Y = 0;
                    }
                    if (position.X == 1390)
                    {
                        velocity.X = 0;
                        velocity.Y = -1f;
                    }
                    if (position.Y == 170)
                    {
                        velocity.X = -1f;
                        velocity.Y = 0;
                    }
                    if (position.X == 1350)
                    {
                        velocity.X = 0;
                        velocity.Y = -1f;
                    }
                    if (position.Y == 130)
                    {
                        velocity.X = -1f;
                        velocity.Y = 0;
                    }
                    if (position.X == 1310)
                    {
                        velocity.X = 0;
                        velocity.Y = -1f;
                    }
                    if (position.Y == 90)
                    {
                        velocity.X = -1f;
                        velocity.Y = 0;
                    }
                    if (position.X < 1240)
                    {
                        up = false;
                        down = true;
                    }
                }
                if (down)
                {
                    if (position.X < 1240)
                    {
                        velocity.X = 1f;
                        velocity.Y = 0;

                    }
                    if (position.X == 1310)
                    {
                        velocity.X = 0;
                        velocity.Y = 1f;
                    }
                    if (position.Y == 130)
                    {
                        velocity.X = 1f;
                        velocity.Y = 0;
                    }
                    if (position.X == 1350)
                    {
                        velocity.X = 0;
                        velocity.Y = 1f;
                    }
                    if (position.Y == 170)
                    {
                        velocity.X = 1f;
                        velocity.Y = 0;
                    }
                    if (position.X == 1390)
                    {
                        velocity.X = 0;
                        velocity.Y = 1f;
                    }
                    if (position.Y == 210)
                    {
                        velocity.X = 1f;
                        velocity.Y = 0;
                    }
                    if (position.X == 1430)
                    {
                        velocity.X = 0;
                        velocity.Y = 1f;
                    }
                    if (position.Y == 250)
                    {
                        velocity.X = 1f;
                        velocity.Y = 0;
                    }
                    if (position.X == 1470)
                    {
                        velocity.X = 0;
                        velocity.Y = 1f;
                    }
                    if (position.Y == 290)
                    {
                        velocity.X = 1f;
                        velocity.Y = 0;
                    }
                    if (position.X == 1510)
                    {
                        velocity.X = 0;
                        velocity.Y = 1f;
                    }
                    if (position.Y == 330)
                    {
                        velocity.X = 1f;
                        velocity.Y = 0;
                    }
                    if (position.X == 1560)
                    {
                        up = true;
                        down = false;
                        velocity.X = -1f;
                    }
                }
            }

            if (type == 5)
            {
                position += velocity;
                
                if (position.X >= 790)
                {
                    velocity.X = -1.5f;
                }
                else if (position.X <= 280)
                {
                    velocity.X = 1.5f;
                }
            }

            if (type == 7)
            {
                bullet_direction = new Vector2((enemy_player_distance.X - 10) / 600, (enemy_player_distance.Y - 10) / 600);

                position += velocity;

                //mouseDistance = player.Position.X - this.position.Y;

                mouseDistance.X = player.Position.X - (this.position.X + 72);
                mouseDistance.Y = player.Position.Y - (this.position.Y + 72);


                if (mouseDistance.X >= -500 && mouseDistance.X <= 500)
                {
                    if (mouseDistance.X < -1)
                    {
                        velocity.X = speed * -1f;
                    }

                    else if (mouseDistance.X > 1)
                    {
                        velocity.X = speed * 1f;
                    }

                    else if (mouseDistance.X <= 10 && mouseDistance.X >= -10)
                    {
                        velocity.X = 0f;
                    }
                }

                if (mouseDistance.Y >= -500 && mouseDistance.Y <= 500)
                {
                    if (mouseDistance.Y < -1)
                    {
                        velocity.Y = -0.5f;
                    }

                    else if (mouseDistance.Y > 1)
                    {
                        velocity.Y = 0.5f;
                    }

                    else if (mouseDistance.Y <= 10 && mouseDistance.Y >= -10)
                    {
                        velocity.Y = 0f;
                    }
                }
            }

            if (type == 8)
            {
                bullet_direction = new Vector2((enemy_player_distance.X - 80) / 600, (enemy_player_distance.Y - 50) / 600);
                velocity.X = 0f;

                //Console.WriteLine("x" + bullet_direction.X);
                //Console.WriteLine("y" + bullet_direction.Y);
            }

            //bullet function
            if (IsShooting)
            {
                shotTimer += gameTime.ElapsedGameTime.Milliseconds;

                if (shotTimer > timeBetweenShots)
                {
                    shotTimer = 0;

                    if (type == 8) 
                    {
                        b = new Bullet(
                             enemy_bullet_texture,
                             new Vector2((int)position.X + 95, (int)position.Y + 60),
                             bullet_direction,
                             speed_of_bullet, // The Speed
                             10000); // The active time in Milliseconds

                        bullets.Add(b);
                    }
                    else
                    {
                        b = new Bullet(
                             enemy_bullet_texture,
                             new Vector2(position.X + (size / 2), position.Y + (size / 2)),
                             bullet_direction,
                             speed_of_bullet, // The Speed
                             10000); // The active time in Milliseconds

                        bullets.Add(b);
                    }

                    if (two_guns)
                    {
                        b = new Bullet(
                         enemy_bullet_texture,
                         new Vector2(position.X + (size / 2), position.Y + (size / 2)),
                         bullet_direction_2,
                         speed_of_bullet, // The Speed
                         10000); // The active time in Milliseconds

                        bullets.Add(b);
                    }
                    //list_soundeffects[0].CreateInstance().Play();
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
            //

            if (this.health <= 0)
            {
                dead = true;
            }

        }


        /*public bool ShootDirection()
        {
            if (velocity.X == -1f)
                return false;
            else
                return true;
        }
        public bool ShootTimer(GameTime gameTime)
        {
            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer <= 0 && !dead)
            {
                timer = 5;
                return true;
            }
            else
                return false;
        }*/

        public Vector2 Position
        {
            get { return position; }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //NEW CODE 
            SpriteEffects flip = SpriteEffects.None;

            if (velocity.X != 0)
            {
                if (velocity.X <= 0)
                    flip = SpriteEffects.None;
                else if (velocity.X > 0)
                    flip = SpriteEffects.FlipHorizontally;
            }
            else if (velocity.X == 0)
            {
                if (enemy_player_distance.X <= 0)
                    flip = SpriteEffects.None;
                else if (enemy_player_distance.X > 0)
                    flip = SpriteEffects.FlipHorizontally;
            }

            //if (!dead)
            animationPlayer.Draw(gameTime, spriteBatch, position, flip);
            //

            
            foreach (Bullet b in this.bullets)
            {
                b.Draw(spriteBatch);
            }
            
        }
    }
}

