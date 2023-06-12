using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Tutorial013.Controls;

namespace Tutorial013.States
{
  public class MenuState : State
  {
        private List<Component> _components;
        private Texture2D muteTexture;
        private Texture2D unmuteTexture;
        Button mute;
        private int level;
        private bool sound;


        Song mainmenu;

    public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) 
      : base(game, graphicsDevice, content)
    {
            

            this.mainmenu = content.Load<Song>("Sounds/mainMenu");

            MediaPlayer.Volume = 0.3f; //master volume for songs
            MediaPlayer.Play(mainmenu);
            MediaPlayer.IsRepeating = true;



            muteTexture = _content.Load<Texture2D>("mute");
            unmuteTexture = _content.Load<Texture2D>("unmute");

            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            level = 1;
            sound = true;

            mute = new Button(unmuteTexture, buttonFont)
            {
                Position = new Vector2(700, 420),
                Text = "",
            };
            mute.Click += Mute_Click;

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(155, 240),
                Text = "New Game",
            };

            newGameButton.Click += NewGameButton_Click;

            var loadGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(325, 240),
                Text = "Load Game",
            };

            loadGameButton.Click += LoadGameButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(495, 240),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
            {
                newGameButton,
                loadGameButton,
                quitGameButton,
                mute
            };
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      spriteBatch.Begin();

    spriteBatch.Draw(_content.Load<Texture2D>("menu"), new Rectangle(0, 0, 800, 480), Color.White);

      foreach (var component in _components)
        component.Draw(gameTime, spriteBatch);

      spriteBatch.End();
    }

    private void LoadGameButton_Click(object sender, EventArgs e)
    {
            Load();
        _game.ChangeState(new GameState(_game, _graphicsDevice, _content, level, sound, 0, 0, new Vector3(0, 0, 0)));
    }

    private void NewGameButton_Click(object sender, EventArgs e)
    {
        _game.ChangeState(new GameState(_game, _graphicsDevice, _content, level, sound, 0, 0, new Vector3(0, 0, 0)));
    }

    public override void PostUpdate(GameTime gameTime)
    {
      // remove sprites if they're not needed
    }

    public override void Update(GameTime gameTime)
    {
      foreach (var component in _components)
        component.Update(gameTime);



        }

    private void QuitGameButton_Click(object sender, EventArgs e)
    {
      _game.Exit();
    }

    private void Mute_Click(object sender, EventArgs e)
    {
            if (!sound)
            {
                mute._texture = unmuteTexture;
                sound = true;
                MediaPlayer.Volume = 0.3f;
            }
            else if (sound)
            {
                mute._texture = muteTexture;
                sound = false;
                MediaPlayer.Volume = 0.0f;
            }
        }

        private void Load()
    {
        if (!File.Exists("world.xml"))
            level = 1;
        else
            using (var reader = new StreamReader(new FileStream("world.xml", FileMode.Open)))
            {
                var serializer = new XmlSerializer(typeof(int));

                level = (int)serializer.Deserialize(reader);
            }

    }

    }
}
