using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Tutorial013.Controls;
using Tutorial013.Tilemapping;

namespace Tutorial013.States
{
    
    public class GameState : State
  {
        public int value;

        private Enemy _enemy;
        //private Enemy enemy_boss1;
        //private Enemy enemy_boss2;

        public int player_gun_dmg = 10;

        public Vector3 upgrade_vec3;

        private bool finish;

        public bool upgrade_section;

        bool temp123 = true;

        private int TotalActiveTime;

        Song stage1;
        Song stage2;
        Song stage3;
        Song stage4;
        Song mainmenu;
        Song credits;

        private ExtraPowerup _jump;
        private Lives _helth;
        private Lives _helth2;
        private List<Enemy> enemies;
        private List<Enemy> enemies2;

        private List<Component> _components;

        Camera camera;
        Map map, map2, map3, map4;
        Player player;
        bool next = false, next2 = false, next3 = false, next4 = false, Shoot = true;
        public int level;
        public int temp_level;

        bool left = false, right = true;

        bool nextlevel = false;

        bool pause = false;

        bool down = false;

        bool debug = false;

        bool gate = true;

        List<Vector2> bulletsRight;
        List<Vector2> bulletsLeft;

        List<SoundEffect> soundEffects;

        // chuck
        // bullets
        List<Bullet> bullets = new List<Bullet>();

        int timeBetweenShots = 300; // Thats 300 milliseconds
        int shotTimer = 0;


        List<Bullet> enemy_bullets = new List<Bullet>();
        // --

        float bulletspeed = 200f;
        Texture2D bulletTexture;
        Texture2D e_bulletTexture1;
        
        //Texture2D enemy_texture_1;
        //Texture2D enemy_texture_2;
        //Texture2D enemy_texture_3;
        Texture2D enemy_texture_4;
        Texture2D enemy_texture_5;
        Texture2D enemy_texture_6;
        Texture2D boss_texture_1;
        Texture2D boss_texture_2;
        Texture2D explosionTexure;

        Texture2D pauseTexture;
        Rectangle pauseRectangle;

        Texture2D deadTexture;
        Rectangle deadRectangle;

        Texture2D bleed_texture;

        Texture2D finishTexture;

        SpriteFont debugFont;

        Game1 _game1;
        bool sound;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, int _level, bool _sound, int _templevel, int _type, Vector3 _value) 
      : base(game, graphicsDevice, content)
    {
            upgrade_vec3 = _value;
            _game1 = game;
            sound = _sound;
            level = _level;
            temp_level = _templevel;
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            debugFont= _content.Load<SpriteFont>("Fonts/Font");

            map = new Map();
            map2 = new Map();
            map3 = new Map();
            map4 = new Map();

            //sound effects
            soundEffects = new List<SoundEffect>();
            soundEffects.Add(content.Load<SoundEffect>("Sounds/Pew")); //0
            soundEffects.Add(content.Load<SoundEffect>("Sounds/ouch")); //1
            soundEffects.Add(content.Load<SoundEffect>("Sounds/explosion")); //2
            soundEffects.Add(content.Load<SoundEffect>("Sounds/hit_confirm")); //3
            soundEffects.Add(content.Load<SoundEffect>("Sounds/explosion_boss")); //4
            soundEffects.Add(content.Load<SoundEffect>("Sounds/powerUp")); //5
            soundEffects.Add(content.Load<SoundEffect>("Sounds/dead")); //6
            soundEffects.Add(content.Load<SoundEffect>("Sounds/playerhit")); //7
            //--

            player = new Player(content.Load<Texture2D>("gun"), content.Load<Texture2D>("player_bullet"), soundEffects, sound);


            camera = new Camera(graphicsDevice.Viewport);
            bulletsRight = new List<Vector2>();
            bulletsLeft = new List<Vector2>();
            enemies = new List<Enemy>();
            enemies2 = new List<Enemy>();



            //enemy textures
            e_bulletTexture1 = content.Load<Texture2D>("enemyBullet");

            enemy_texture_4 = content.Load<Texture2D>("enemy4");
            enemy_texture_5 = content.Load<Texture2D>("enemy5");
            enemy_texture_6 = content.Load<Texture2D>("enemy6");
            boss_texture_1 = content.Load<Texture2D>("boss1");
            boss_texture_2 = content.Load<Texture2D>("boss2");

            explosionTexure = content.Load<Texture2D>("explosion2");
            //--

            //ced code
            //finish texture
            finishTexture = content.Load<Texture2D>("finish");
            //

            bulletTexture = content.Load<Texture2D>("Bullet");

            bleed_texture = content.Load<Texture2D>("bleed");
            pauseTexture = content.Load<Texture2D>("Pause");
            deadTexture = content.Load<Texture2D>("DeadScreen");

            /*
            enemy_boss1 = new Enemy(7, e_bulletTexture1)
            {
                size = 144,
                walkAnimation = new Animation(boss_texture_1, 144, 0.1f, true),
                position = new Vector2(450, 200),
                health = 300
                //velocity = new Vector2(0, 0)
            };
            */
            

            //

            //enemies[0].position = new Vector2(700, 340);

            //enemies[1].position = new Vector2(582, 180);

            //enemies[2].position = new Vector2(1570, 360);


            var mainMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(500, 100),
                Text = "Main Menu",
            };
            mainMenuButton.Click += MainMenuButton_Click;

            _components = new List<Component>()
            {
            mainMenuButton
            };

            Tiles.Content = content;

            if (level == 1)
                map.Generate(new int[,]{
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,1},
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,1},
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,1,1,1,1,0,0,0,0,0,0,0,1},
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,1},
                { 1,0,0,0,1,1,1,0,0,1,1,1,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,1},
                { 1,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,1},
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                }, 40);

            else if (level == 2)
                map2.Generate(new int[,]{
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                { 1,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,1},
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                { 1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1},
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                { 1,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,1},
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                }, 40);

            else if (level == 3)
                map3.Generate(new int[,]{
                { 2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                { 2,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2},
                { 2,2,2,0,0,0,2,2,0,0,0,0,0,0,0,2,2,2,2,2,0,0,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2},
                { 2,0,0,0,0,0,2,2,2,2,2,0,0,0,2,2,2,2,2,2,2,0,0,2,2,0,0,2,2,2,2,0,0,0,2,2,0,0,0,0,2,2,2},
                { 2,0,0,2,2,2,2,2,2,2,2,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,2,2,2,2,0,0,2,2,2,2,2,2,0,0,0,2},
                { 2,0,0,2,2,2,2,2,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,0,0,0,0,0,0,2},
                { 2,0,0,2,2,2,2,2,0,0,0,0,0,0,0,0,0,2,0,2,2,2,0,0,0,0,0,0,0,0,0,0,2,2,2,2,0,0,2,2,2,2,2},
                { 2,0,0,0,0,0,2,2,0,0,0,2,2,2,0,0,0,2,0,0,0,0,0,2,2,0,0,2,2,2,2,0,2,2,2,2,0,0,0,0,0,0,2},
                { 2,0,0,0,0,0,0,0,0,0,2,2,2,2,2,0,0,2,0,0,0,0,2,2,2,0,0,2,2,2,2,0,2,2,2,2,0,0,0,0,0,0,2},
                { 2,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,0,0,0,0,0,0,0,2,2,2,2,0,0,0,0,0,0,2},
                { 2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                { 2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                }, 40);

            else if (level == 4)
                map4.Generate(new int[,]{
                { 2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                { 2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                { 2,2,0,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,2,2},
                { 2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2},
                { 2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2},
                { 2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                { 2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                { 2,0,2,2,2,0,0,0,0,0,0,0,0,0,0,2,2,2,0,2},
                { 2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                { 2,0,0,0,0,0,0,2,2,2,2,2,2,0,0,0,0,0,0,2},
                { 2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                { 2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                }, 40);

            else if (level == 5)
                map2.Generate(new int[,]{
                { 2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                { 2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                { 2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                { 2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                { 2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                { 2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                { 2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                { 2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                { 2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                { 2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                { 2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                { 2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                }, 40);
            

            player.Load(content);
            
            this.stage1 = content.Load<Song>("Sounds/Stage1");
            this.stage2 = content.Load<Song>("Sounds/Stage2");
            this.stage3 = content.Load<Song>("Sounds/Stage3");
            this.stage4 = content.Load<Song>("Sounds/Stage4");
            this.mainmenu = content.Load<Song>("Sounds/mainMenu");
            this.credits = content.Load<Song>("Sounds/credits");

            int button_disappear = 1000;

            //UPGRADE SECTION
            var gun_dmg_Button = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(80, button_disappear),
                Text = player_gun_dmg + " => " + (player_gun_dmg + 10),
            };

            gun_dmg_Button.Click += Gun_dmg_Click;

            var agility_Button = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(325, button_disappear),
                Text = player.agility + " => " + (player.agility + 2),
            };

            agility_Button.Click += Agility_Click;

            var attack_speed_Button = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(560, button_disappear),
                Text = player.timeBetweenShots + " => " + (player.timeBetweenShots - 100),
            };

            attack_speed_Button.Click += Attack_speed_Click;
            //

            if (level == 1)
            {
                var _jumpTexture = content.Load<Texture2D>("extra");
                _jump = new ExtraPowerup(_jumpTexture, content.Load<SoundEffect>("Sounds/powerUp"), sound)
                {
                    extra_rectangle = new Rectangle(1180, 360, 40, 40)
                };

                if (sound)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(stage1);
                    MediaPlayer.IsRepeating = true;
                }
                

                _enemy = new Enemy(0, e_bulletTexture1)
                {
                    size = 36, //NEW CODE 
                    deathAnimation = new Animation(content.Load<Texture2D>("exposion"), 72, 0.1f, true),
                    walkAnimation = new Animation(content.Load<Texture2D>("enemy1"), 36, 0.1f, true), //NEW CODE
                    position = new Vector2(400, 280),
                    timeBetweenShots = 2000,
                    speed_of_bullet = 8,
                    health = 30
                };

                enemies.Add(_enemy);

                _enemy = new Enemy(0, e_bulletTexture1)
                {
                    size = 36, //NEW CODE 
                    deathAnimation = new Animation(content.Load<Texture2D>("exposion"), 72, 0.1f, true),
                    walkAnimation = new Animation(content.Load<Texture2D>("enemy1"), 36, 0.1f, true), //NEW CODE
                    position = new Vector2(580, 200),
                    timeBetweenShots = 1600,
                    speed_of_bullet = 8,
                    health = 30
                };

                enemies.Add(_enemy);

                _enemy = new Enemy(2, e_bulletTexture1)
                {
                    size = 36, //NEW CODE 
                    deathAnimation = new Animation(content.Load<Texture2D>("exposion"), 72, 0.1f, true),
                    walkAnimation = new Animation(content.Load<Texture2D>("enemy1"), 36, 0.1f, true), //NEW CODE
                    position = new Vector2(900, 340),
                    timeBetweenShots = 1000,
                    speed_of_bullet = 8,
                    two_guns = true,
                    health = 30
                };

                enemies.Add(_enemy);

                _enemy = new Enemy(1, e_bulletTexture1)
                {
                    size = 36,
                    deathAnimation = new Animation(content.Load<Texture2D>("exposion"), 72, 0.1f, true),
                    walkAnimation = new Animation(content.Load<Texture2D>("enemy2"), 36, 0.1f, true),
                    position = new Vector2(862, 180),
                    health = 30
                };

                enemies.Add(_enemy);

                _enemy = new Enemy(3, e_bulletTexture1)
                {
                    size = 72,
                    deathAnimation = new Animation(content.Load<Texture2D>("exposion"), 72, 0.1f, true),
                    walkAnimation = new Animation(content.Load<Texture2D>("enemy3"), 72, 0.1f, true),
                    position = new Vector2(1570, 200),
                    health = 70
                };

                enemies.Add(_enemy);
            }

            if (level == 2)
            {
                bullets.Clear();

                _enemy = new Enemy(7, content.Load<Texture2D>("enemy3_bullet"))
                {
                    size = 80,
                    deathAnimation = new Animation(content.Load<Texture2D>("bigger_exposion"), 144, 0.1f, true),
                    walkAnimation = new Animation(boss_texture_1, 144, 0.1f, true),
                    position = new Vector2(500, 200),
                    health = 1000,
                    timeBetweenShots = 1000,
                    speed_of_bullet = 6,
                    speed = 1f
                    
                    //velocity = new Vector2(0, 0)
                };
                
                enemies.Add(_enemy);

                if (sound)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(stage2);
                    MediaPlayer.IsRepeating = true;
                }
                
            }

            if (level == 3)
            {
                player.xOfSet3();
                bullets.Clear();

                if (sound)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(stage3);
                    MediaPlayer.IsRepeating = true;
                }
                

                _enemy = new Enemy(0, e_bulletTexture1)
                {
                    size = 72,
                    deathAnimation = new Animation(content.Load<Texture2D>("exposion"), 72, 0.1f, true),
                    walkAnimation = new Animation(enemy_texture_4, 72, 0.1f, true),
                    position = new Vector2(460, 200),
                    health = 30,
                    timeBetweenShots = 800,
                    speed_of_bullet = 6
                    //velocity = new Vector2(0, 0)
                };

                enemies.Add(_enemy);

                _enemy = new Enemy(5, e_bulletTexture1)
                {
                    size = 36,
                    deathAnimation = new Animation(content.Load<Texture2D>("exposion"), 72, 0.1f, true),
                    walkAnimation = new Animation(enemy_texture_5, 36, 0.1f, true),
                    position = new Vector2(520, 360),
                    health = 30,
                    timeBetweenShots = 800,
                    speed_of_bullet = 6,
                    IsShooting = false
                    //velocity = new Vector2(0, 0)
                };

                enemies.Add(_enemy);

                _enemy = new Enemy(5, e_bulletTexture1)
                {
                    size = 36,
                    deathAnimation = new Animation(content.Load<Texture2D>("exposion"), 72, 0.1f, true),
                    walkAnimation = new Animation(enemy_texture_5, 36, 0.1f, true),
                    position = new Vector2(440, 360),
                    health = 30,
                    timeBetweenShots = 800,
                    speed_of_bullet = 6,
                    IsShooting = false
                    //velocity = new Vector2(0, 0)
                };

                enemies.Add(_enemy);

                _enemy = new Enemy(0, e_bulletTexture1)
                {
                    size = 72,
                    deathAnimation = new Animation(content.Load<Texture2D>("exposion"), 72, 0.1f, true),
                    walkAnimation = new Animation(enemy_texture_6, 72, 0.1f, true),
                    position = new Vector2(350, 45),
                    health = 30,
                    timeBetweenShots = 800,
                    speed_of_bullet = 6
                    //velocity = new Vector2(0, 0)
                };

                enemies.Add(_enemy);

                _enemy = new Enemy(0, e_bulletTexture1)
                {
                    size = 72,
                    deathAnimation = new Animation(content.Load<Texture2D>("exposion"), 72, 0.1f, true),
                    walkAnimation = new Animation(enemy_texture_4, 72, 0.1f, true),
                    position = new Vector2(1130, 210),
                    health = 80,
                    timeBetweenShots = 800,
                    speed_of_bullet = 6
                    //velocity = new Vector2(0, 0)
                };

                enemies.Add(_enemy);

                _enemy = new Enemy(0, e_bulletTexture1)
                {
                    size = 72,
                    deathAnimation = new Animation(content.Load<Texture2D>("exposion"), 72, 0.1f, true),
                    walkAnimation = new Animation(enemy_texture_4, 72, 0.1f, true),
                    position = new Vector2(1130, 45),
                    health = 80,
                    timeBetweenShots = 800,
                    speed_of_bullet = 6
                    //velocity = new Vector2(0, 0)
                };

                enemies.Add(_enemy);
            }

            var _helthTexture = content.Load<Texture2D>("health");
            _helth = new Lives(_helthTexture, content.Load<SoundEffect>("Sounds/powerUp"), sound)
            {
                extra_rectangle = new Rectangle(40, 360, 40, 40)
            };

            _helth2 = new Lives(_helthTexture, content.Load<SoundEffect>("Sounds/powerUp"), sound)
            {
                extra_rectangle = new Rectangle(720, 360, 40, 40)
            };

            if (level == 4)
            {
                bullets.Clear();

                player.xOfSet4();

                if (sound)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(stage4);
                    MediaPlayer.IsRepeating = true;
                }


                _enemy = new Enemy(8, content.Load<Texture2D>("enemy3_bullet"))
                {
                    size = 30,
                    deathAnimation = new Animation(content.Load<Texture2D>("biggest_exposion"), 200, 0.1f, true),
                    walkAnimation = new Animation(boss_texture_2, 190, 0.1f, true),
                    position = new Vector2(300, 150),
                    health = 1800,
                    timeBetweenShots = 500,
                    speed_of_bullet = 7
                    //velocity = new Vector2(0, 0)
                };

                enemies.Add(_enemy);
            }

            if (level == 5)
            {
                if (sound)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(mainmenu);
                    MediaPlayer.IsRepeating = true;
                }

                //UPGRADE SECTION
                gun_dmg_Button = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(80, 200),
                    Text = upgrade_vec3.X + " => " + (upgrade_vec3.X + 10),
                };

                gun_dmg_Button.Click += Gun_dmg_Click;

                agility_Button = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(325, 200),
                    Text = upgrade_vec3.Y + " => " + (upgrade_vec3.Y + 2),
                };

                agility_Button.Click += Agility_Click;

                attack_speed_Button = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(560, 200),
                    Text = upgrade_vec3.Z + " => " + (upgrade_vec3.Z - 100),
                };

                attack_speed_Button.Click += Attack_speed_Click;

            }

            _components = new List<Component>()
            {
                gun_dmg_Button,
                agility_Button,
                attack_speed_Button
            };
            player_gun_dmg += (int)upgrade_vec3.X;
            player.agility += (int)upgrade_vec3.Y;
            player.timeBetweenShots -= (int)upgrade_vec3.Z;
        }

        private void Save()
        {
            using (var writer = new StreamWriter(new FileStream("world.xml", FileMode.Create)))
            {
                var serializer = new XmlSerializer(typeof(int));

                serializer.Serialize(writer, level);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            /*spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                 camera.Transform);*/

            spriteBatch.Begin(transformMatrix: camera.Transform);
            // spriteBatch.Begin();

            /*if (level == 0 && upgrade_section == true)
            {
                //player.xOfSet2();
                //spriteBatch.Draw(_content.Load<Texture2D>("upgrade"), new Rectangle(0, 0, 1720, 480), Color.White);
                //map.Draw(spriteBatch, next);

                Console.WriteLine("UPGRADE APPEAR");
                spriteBatch.Draw(_content.Load<Texture2D>("upgrade"), new Rectangle(0, 0, 800, 480), Color.White);

                foreach (var component in _components)
                    component.Draw(gameTime, spriteBatch);

            }*/
            if (level == 1)
                {
                player.xOfSet1();
                spriteBatch.Draw(_content.Load<Texture2D>("background1"), new Rectangle(0, 0, 1720, 480), Color.White);
                _jump.Draw(spriteBatch);

                temp_level = 2;

                if (enemies.Count() == 0)
                {
                    spriteBatch.Draw(_content.Load<Texture2D>("portal"), new Rectangle(1640, 280, 40, 120), Color.White);

                    if (player.hit(new Rectangle(1640, 280, 40, 120)))
                    {
                        //upgrade_section = true;

                        next = true;
                        nextlevel = true;
                        level = 5;
                        Save();
                        player.NextLvl(); //position reset of player
                        _game1.ChangeState(new GameState(_game1, _graphicsDevice, _content, level, sound, temp_level, 0, upgrade_vec3)); 
                        //enemy_boss1.walkAnimation = new Animation(boss_texture_1, 144, 0.1f, true);

                        // chuck
                        player.xOfSet2();
                                //


                                //Enemies or boss put here

                                /*
                                _enemy = new Enemy(2, e_bulletTexture1)
                                {
                                    size = 36, //NEW CODE 
                                    walkAnimation = new Animation(enemy_texture_4, 36, 0.1f, true), //NEW CODE
                                    position = new Vector2(700, 340),
                                    timeBetweenShots = 500,
                                    health = 30
                                };
                                enemies.Add(_enemy);
                                */
                        //
                    }
                }
                map.Draw(spriteBatch, next);
            }

            if (level == 2)
            {
                spriteBatch.Draw(_content.Load<Texture2D>("background1"), new Rectangle(0, 0, 800, 480), Color.White);

                temp_level = 3;

                if (enemies.Count() != 0)
                {
                    if (enemies[0].health <= 500)
                    {
                        enemies[0].speed = 2.5f;
                        enemies[0].timeBetweenShots = 500;
                    }
                }

                if (enemies.Count() == 0)
                {
                    spriteBatch.Draw(_content.Load<Texture2D>("portal"), new Rectangle(720, 280, 40, 120), Color.White);



                    if (player.hit(new Rectangle(720, 280, 40, 120)))
                    {

                        next2 = true;
                        nextlevel = true;
                        level = 5;
                        Save();
                        player.NextLvl();
                        _game1.ChangeState(new GameState(_game1, _graphicsDevice, _content, level, sound, temp_level, 0, upgrade_vec3));
                        // chuck
                        player.xOfSet3();
                        //

                        
                    }
                }
                map2.Draw(spriteBatch, next2);
            }

            if (level == 3)
            {
                spriteBatch.Draw(_content.Load<Texture2D>("background2"), new Rectangle(0, 0, 1720, 480), Color.White);

                temp_level = 4;

                if (enemies.Count() == 0)
                {
                    spriteBatch.Draw(_content.Load<Texture2D>("portal"), new Rectangle(1640, 280, 40, 120), Color.White);
                    if (player.hit(new Rectangle(1640, 280, 40, 120)))
                    {
                        next3 = true;
                        nextlevel = true;
                        level = 5;
                        Save();
                        player.NextLvl();
                        _game1.ChangeState(new GameState(_game1, _graphicsDevice, _content, level, sound, temp_level, 0, upgrade_vec3));
                        // chuck
                        player.xOfSet4();
                        //
                        
                        //MediaPlayer.Play(stage4);
                        //MediaPlayer.IsRepeating = true;
                    }
                }
                map3.Draw(spriteBatch, next3);
            }

            if (level == 4)
            {
                spriteBatch.Draw(_content.Load<Texture2D>("background2"), new Rectangle(0, 0, 800, 480), Color.White);
                _helth.Draw(spriteBatch);
                _helth2.Draw(spriteBatch);
                if (enemies.Count() != 0)
                {
                    if (enemies[0].health <= 1000)
                    {
                        enemies[0].timeBetweenShots = 200;
                        enemies[0].speed_of_bullet = 12;
                    }
                }

                if (enemies.Count() == 0)
                {
                    spriteBatch.Draw(_content.Load<Texture2D>("portal"), new Rectangle(720, 280, 40, 120), Color.White);
                    if (player.hit(new Rectangle(720, 280, 40, 120)))
                    {
                        //_game.Exit();

                        //_game1.ChangeState(new GameState(_game1, _graphicsDevice, _content, level, sound));

                        if (gate)
                        {
                            if (sound)
                            {
                                MediaPlayer.Stop();
                                MediaPlayer.Play(credits);
                                MediaPlayer.IsRepeating = true;
                            }
                            gate = false;
                        }

                        finish = true;
                    }
                }
                map4.Draw(spriteBatch, next4);
            }



            foreach (Enemy n in enemies)
            {
                n.Draw(spriteBatch, gameTime);
            }
            player.Draw(gameTime, spriteBatch);
            //if (next && nextlevel)
            //{
            //    player.NextLvl();
            //    nextlevel = false;
            //} 

            //DEBUG
            if (Keyboard.GetState().IsKeyDown(Keys.O))
            {
                debug = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                debug = false;
            }
            if (debug)
            {
                //spriteBatch.Draw(HudTexture, Vector2.Zero, Color.White);
                string value = player.Health.ToString();
                spriteBatch.DrawString(debugFont, "Player Health:" +value, new Vector2((int)camera.Center.X - 400, (int)camera.Center.Y), Color.White);
            }

            if (finish)
            {
                pauseRectangle = new Rectangle((int)camera.Center.X - 400, (int)camera.Center.Y, 800, 480);
                spriteBatch.Draw(finishTexture, pauseRectangle, Color.White);
            }

            if (pause)
            {
                pauseRectangle = new Rectangle((int)camera.Center.X - 400, (int)camera.Center.Y, 800, 480);
                spriteBatch.Draw(pauseTexture, pauseRectangle, Color.White);
            }

            if (player.isDead())
            {
                deadRectangle = new Rectangle((int)camera.Center.X - 400, (int)camera.Center.Y, 800, 480);
                spriteBatch.Draw(deadTexture, deadRectangle, Color.White);

            }

            if (player.Health <= 30)
            {
                spriteBatch.Draw(_content.Load<Texture2D>("bleed"), new Rectangle((int)camera.Center.X - 400, (int)camera.Center.Y, 800, 480), Color.White);
            }

            if (level == 5)
            {
                Console.WriteLine("UPGRADE APPEAR");
                spriteBatch.Draw(_content.Load<Texture2D>("upgrade"), new Rectangle(0, 0, 800, 480), Color.White);

            }

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

    public override void Update(GameTime gameTime)
    {

            //ced code
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                enemies.Clear();
                if(sound)
                    MediaPlayer.Stop();
                _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
            }


            //

            //chuck 
            //IF BULLET HITS TILES USING TWO RECTS AND INTERSECTS FUNCTION
            //BULLET COLLISION CODE
            foreach (Bullet b in player.bullets)
            {
                foreach (CollisionTiles tile in map.CollisionTiles)
                {
                    if (b.bullet_hitbox.Intersects(tile.Rectangle))
                    {
                        b.isVisible = false;
                    }
                }
            }

            foreach (Bullet b in player.bullets)
            {
                foreach (CollisionTiles tile in map2.CollisionTiles)
                {
                    if (b.bullet_hitbox.Intersects(tile.Rectangle))
                    {
                        b.isVisible = false;
                    }
                }
            }

            foreach (Bullet b in player.bullets)
            {
                foreach (CollisionTiles tile in map3.CollisionTiles)
                {
                    if (b.bullet_hitbox.Intersects(tile.Rectangle))
                    {
                        b.isVisible = false;
                    }
                }
            }

            foreach (Bullet b in player.bullets)
            {
                foreach (CollisionTiles tile in map4.CollisionTiles)
                {
                    if (b.bullet_hitbox.Intersects(tile.Rectangle))
                    {
                        b.isVisible = false;
                    }
                }
            }

            foreach (Bullet b in player.bullets)
            {
                foreach (Enemy n in enemies)
                {
                    if (b.bullet_hitbox.Intersects(n._rectangle))
                    {
                        if (sound)
                            soundEffects[3].CreateInstance().Play(); //death

                        n.health -= player_gun_dmg;
                        b.isVisible = false;
                    }
                }

                
                /*if (b.bullet_hitbox.Intersects(_enemy._rectangle))
                {
                    b.isVisible = false;
                    _enemy.isDead();
                }

                if (b.bullet_hitbox.Intersects(_enemy2._rectangle))
                {
                    b.isVisible = false;
                    _enemy2.isDead();
                }

                if (b.bullet_hitbox.Intersects(_enemy3._rectangle))
                {
                    b.isVisible = false;
                    _enemy3.isDead();
                }*/

            }

            foreach (Enemy n in enemies)
            {
                foreach (Bullet b in n.bullets)
                {
                    foreach (CollisionTiles tile in map.CollisionTiles)
                    {
                        if (b.bullet_hitbox.Intersects(tile.Rectangle))
                        {
                            b.isVisible = false;
                        }
                    }

                }
            }

            foreach (Enemy n in enemies)
            {
                foreach (Bullet b in n.bullets)
                {
                    foreach (CollisionTiles tile in map2.CollisionTiles)
                    {
                        if (b.bullet_hitbox.Intersects(tile.Rectangle))
                        {
                            b.isVisible = false;
                        }
                    }

                }
            }

            foreach (Enemy n in enemies)
            {
                foreach (Bullet b in n.bullets)
                {
                    foreach (CollisionTiles tile in map3.CollisionTiles)
                    {
                        if (b.bullet_hitbox.Intersects(tile.Rectangle))
                        {
                            b.isVisible = false;
                        }
                    }

                }
            }

            foreach (Enemy n in enemies)
            {
                foreach (Bullet b in n.bullets)
                {
                    foreach (CollisionTiles tile in map4.CollisionTiles)
                    {
                        if (b.bullet_hitbox.Intersects(tile.Rectangle))
                        {
                            b.isVisible = false;
                        }
                    }

                }
            }



            //check enemies
            
            for (int i = 0; i < enemies.Count; i++)
            {

                if (enemies[i].dead == true)
                {
                    enemies[i].IsShooting = false;

                    if (enemies[i].type != 8) //if not last boss
                    {
                        if (temp123 == true)
                        {
                            if (sound)
                                soundEffects[2].CreateInstance().Play(); //death

                            temp123 = false;
                        }

                        this.TotalActiveTime += gameTime.ElapsedGameTime.Milliseconds;

                        if (this.TotalActiveTime >= 500)
                        {
                            temp123 = true;
                            this.TotalActiveTime = 0;
                            enemies.RemoveAt(i);
                        }
                    }
                    else //if last boss
                    {
                        this.TotalActiveTime += gameTime.ElapsedGameTime.Milliseconds;

                        if (temp123 == true)
                        {
                            if (sound)
                                soundEffects[4].CreateInstance().Play(); //death

                            temp123 = false;
                        }

                        if (this.TotalActiveTime >= 1500)
                        {
                            temp123 = true;
                            this.TotalActiveTime = 0;
                            enemies.RemoveAt(i);
                        }
                    }
                        
                }
                    
            }



            //----

            if (!pause && !player.isDead() && !finish)
            {

                foreach (Enemy n in enemies)
                {
                    n.Update(player, gameTime);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    pause = true;     
                }

                //If enemy collide with player
                
                foreach (Enemy n in enemies)
                {
                    if (player.isEnemyCollide(n._rectangle))
                    {
                        player.Health--;
                    }
                }

                foreach (Enemy n in enemies)
                {
                    foreach (Bullet b in n.bullets)
                    {
                        if (b.bullet_hitbox.Intersects(player.rectangle))
                        {
                            b.isVisible = false;
                            player.Health -= 10;

                            if (player.Health >= 0)
                            {
                                if (sound)
                                    soundEffects[7].CreateInstance().Play(); //playerhit
                            }
                        }
                    }
                }



                if (level == 1)
                {
                    foreach (CollisionTiles tile in map.CollisionTiles)
                    {
                        player.Collision(tile.Rectangle, map.Width, map.Height);
                        camera.Update(player.Position, map.Width, map.Height);
                    }

                    _jump.Update(player);
                }

                else if (level == 2)
                {
                    foreach (CollisionTiles tile in map2.CollisionTiles)
                    {
                        player.Collision(tile.Rectangle, map2.Width, map2.Height);
                        camera.Update(player.Position, map2.Width, map2.Height);
                    }

                    if (nextlevel)
                    {
                        //enemies.Clear();
                        
                        

                        /*_enemy = new Enemy(7, e_bulletTexture1)
                        {
                            size = 144,
                            walkAnimation = new Animation(boss_texture_1, 144, 0.1f, true),
                            position = new Vector2(600, 300),
                            health = 300,
                            speed_of_bullet = 2,
                            timeBetweenShots = 500
                        };

                        enemies.Add(_enemy);*/


                        nextlevel = false;
                    }

                }

                else if (level == 3)
                {
                    foreach (CollisionTiles tile in map3.CollisionTiles)
                    {
                        player.Collision(tile.Rectangle, map3.Width, map3.Height);
                        camera.Update(player.Position, map3.Width, map3.Height);
                    }
                }

                else if (level == 4)
                {
                    foreach (CollisionTiles tile in map4.CollisionTiles)
                    {
                        player.Collision(tile.Rectangle, map4.Width, map4.Height);
                        camera.Update(player.Position, map4.Width, map4.Height);
                    }

                    _helth.Update(player);
                    _helth2.Update(player);
                }

                else if (level == 5)
                {
                    foreach (CollisionTiles tile in map2.CollisionTiles)
                    {
                        player.Collision(tile.Rectangle, map4.Width, map4.Height);
                        camera.Update(player.Position, map4.Width, map4.Height);
                    }
                }

                //Player Shoot

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    right = true;
                    left = false;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    right = false;
                    left = true;
                }

                //End of Player Shoot

                //Enemy Shoot

                //if (_enemy.ShootTimer(gameTime) && _enemy.ShootDirection())
                //{
                //    enemybulletsRight.Add(_enemy.Position);
                //}

                /*
                if (_enemy2.ShootTimer(gameTime))
                {
                    if (_enemy2.ShootDirection())
                    {
                        enemybulletsRight.Add(_enemy2.Position);
                    }
                    else if (!_enemy2.ShootDirection())
                    {
                        enemybulletsLeft.Add(_enemy2.Position);
                    }
                }

                if (_enemy3.ShootTimer(gameTime))
                {
                    if (_enemy3.ShootDirection())
                    {
                        enemybulletsRight.Add(_enemy3.Position);
                    }
                    else if (!_enemy3.ShootDirection())
                    {
                        enemybulletsLeft.Add(_enemy3.Position);
                    }
                }
                */

                // End of Enemy Shoot

                foreach (var component in _components)
                    component.Update(gameTime);


                player.Update(gameTime, level);

            }

            else if (pause)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    pause = false;
                }
            }

            Console.WriteLine("current agility: " + player.agility);
            Console.WriteLine("current vector: " + upgrade_vec3);
        }



    private void MainMenuButton_Click(object sender, EventArgs e)
    {
        _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
    }

    private void Gun_dmg_Click(object sender, EventArgs e)
    {
            upgrade_vec3.X += 10;
            upgrade_section = false;

            next = true;
            nextlevel = true;
            level = temp_level;
            Save();
            player.NextLvl();
            _game1.ChangeState(new GameState(_game1, _graphicsDevice, _content, level, sound, temp_level, 1, upgrade_vec3));
        }

    private void Agility_Click(object sender, EventArgs e)
    {
            upgrade_vec3.Y += 2;
            upgrade_section = false;

            next = true;
            nextlevel = true;
            level = temp_level;
            Save();
            player.NextLvl();
            _game1.ChangeState(new GameState(_game1, _graphicsDevice, _content, level, sound, temp_level, 2, upgrade_vec3));
        }

    private void Attack_speed_Click(object sender, EventArgs e)
    {
            upgrade_vec3.Z += 100;
            upgrade_section = false;

            next = true;
            nextlevel = true;
            level = temp_level;
            Save();
            player.NextLvl();
            _game1.ChangeState(new GameState(_game1, _graphicsDevice, _content, level, sound, temp_level, 3, upgrade_vec3));
        }

    }


}
