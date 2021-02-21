using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game.Landscape;
using Timer = System.Windows.Forms.Timer;

namespace Game.Game {
    class Game {

        Game reload;

        void SaveGame() => reload = this;

        //void ReloadGame() => this = reload;


        Control.ControlCollection controls;
        Timer timer;

        public Player Player { get; set; }

        public List<Entity> entities = new List<Entity>();

        List<Enemy> enemies;


        //public Game(Game game) {
        //    this.controls = game.controls;
        //    this.Player = game.Player;
        //    this.enemies = game.enemies;
        //    this.entities.Add(this.Player);
        //    foreach (Entity entity in game.enemies) {
        //        this.entities.Add(entity);
        //    }
        //    timer = new Timer();

        //    timer.Interval = 1;
        //    timer.Tick += Update;

        //    //timer.Start();
        //    //Player.OnPlayerDeath += (s, e) => {
        //    //    timer.Stop();
        //    //};
        //}


        public Game(Player player, List<Enemy> enemies, List<Ground> grounds, Control.ControlCollection controls) {
            this.controls = controls;
            Player = player;

            this.enemies = enemies;

            entities.Add(player);
            foreach (Entity entity in enemies) {
                entities.Add(entity);
            }

            timer = new Timer();

            timer.Interval = 10;
            timer.Tick += Update;
            timer.Start();


            //Player.OnPlayerDeath += (s, e) => {
            //    timer.Stop();
            //};
        }


        enum Interact {
            Left,
            Right,
            Top,
            Bottom
        }

        private async void Shake() {
            await Task.Run(() => {
                var original = Scene.GetScene().Location;
                var rnd = new Random(1337);
                const int shake_amplitude = 10;
                for (int i = 0; i < 10; i++) {

                    if (Scene.GetScene().InvokeRequired) {
                        Scene.GetScene().Invoke(new MethodInvoker(delegate {
                            Scene.GetScene().Location = new Point(original.X + rnd.Next(-shake_amplitude, shake_amplitude), original.Y + rnd.Next(-shake_amplitude, shake_amplitude));
                            //System.Threading.Thread.Sleep(20);
                        }));
                    }
                    else {
                        Scene.GetScene().Location = new Point(original.X + rnd.Next(-shake_amplitude, shake_amplitude), original.Y + rnd.Next(-shake_amplitude, shake_amplitude));
                    }
                    Thread.Sleep(20);
                }
                if (Scene.GetScene().InvokeRequired) {
                    Scene.GetScene().Invoke(new MethodInvoker(delegate {
                        Scene.GetScene().Location = original;
                    }));
                }
                else Scene.GetScene().Location = original;

            });
        }


        private void Update(object sender, EventArgs e) {




            foreach (Enemy enemy in enemies) {
                enemy.physics.ApplyPhysics();
                if (enemy.IsOnGround) {
                    enemy.physics.AddForce(8);
                }
                enemy.HPBar.TrackEntity();
                if (enemy.EntityControl.Bounds.IntersectsWith(Player.EntityControl.Bounds)) {
                    Player.Hit(enemy.Damage);
                }
            }

            Player.physics.ApplyPhysics();
            Player.HPBar.TrackEntity();


            byte[] keys = new byte[256];

            GetKeyboardState(keys);

            if ((keys[(int)Keys.A] & 128) == 128) {
                    Player.Left();
            }

            if ((keys[(int)Keys.Space] & 128) == 128) {
                Player.Jump();

                //Shake();
            }

            if ((keys[(int)Keys.D] & 128) == 128) {
                    Player.Right();
            }
            Scene.GetScene().Text = cod;
        }


        [DllImport("user32.dll")]
        public static extern int GetKeyboardState(byte[] keystate);

        string cod = new String(' ', 15);

        public void KeyPress(object sender, KeyPressEventArgs e) {
            cod += e.KeyChar;
            cod = cod.Substring(1);

            if (cod.EndsWith("healing_")) {
                Player.HPCurrent = Player.HPMax;
            }


        }

    }
}
/*
  if (controls[1].InvokeRequired) {
                    controls[1].Invoke(new MethodInvoker(delegate {
                        //access picturebox here

                        controls.RemoveAt(0);
                    }));
                }
 
 */