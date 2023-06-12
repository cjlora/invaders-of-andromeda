using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Text;
using Tutorial013;

namespace Tutorial013
{
    class Lives
    {
        private Texture2D _texture;
        public Rectangle extra_rectangle;
        public bool HelthupBool;

        private SoundEffect soundeffect;
        private bool sound;

        public Lives(Texture2D texture, SoundEffect soundeffect_p, bool sound_p)
        {
            HelthupBool = false;
            _texture = texture;
            soundeffect = soundeffect_p;
            sound = sound_p;
        }

        public void Update(Player _player)
        {
            if (_player.rectangle.Intersects(extra_rectangle) && HelthupBool == false)
            {
                HelthupBool = true;
                _player.Health += 30;

                if (sound)
                    soundeffect.CreateInstance().Play(); //death

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!HelthupBool)
            {
                spriteBatch.Draw(_texture, extra_rectangle, Color.White);
            }
        }
    }
}
