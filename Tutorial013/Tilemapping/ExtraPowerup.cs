using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Tutorial013;

namespace Tutorial013
{
    class ExtraPowerup
    {
        private Texture2D _texture;
        public Rectangle extra_rectangle;
        public bool upBool;

        private SoundEffect soundeffect;
        private bool sound;

        public ExtraPowerup(Texture2D texture, SoundEffect soundeffect_p, bool sound_p)
        {
            upBool = false;
            _texture = texture;
            soundeffect = soundeffect_p;
            sound = sound_p;
        }

        public void Update(Player _player)
        {
            if (_player.rectangle.Intersects(extra_rectangle) && upBool == false)
            {
                upBool = true;
                _player.maxHeight = -11f;

                if (sound)
                    soundeffect.CreateInstance().Play(); //death
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!upBool)
            {
                spriteBatch.Draw(_texture, extra_rectangle, Color.White);
            }
        }
    }
}
