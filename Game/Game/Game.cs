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

        Control.ControlCollection controls;
        Timer timer;

        public Player Player { get; set; }

        public List<Entity> entities = new List<Entity>();

        List<Enemy> enemies;

        public Game(Player player, List<Enemy> enemies, List<Ground> grounds, Control.ControlCollection controls) {
            this.controls = controls;
            Player = player;

            this.enemies = enemies;

            entities.Add(player);
            foreach (Entity entity in enemies) {
                entities.Add(entity);
            }

            timer = new Timer();

            timer.Interval = 15;
            timer.Tick += Update;

            timer.Start();


        }

        enum Interact {
            Left,
            Right,
            Top,
            Bottom
        }


        public bool CheckColision(Control control, params string[] tags) {
            var temp = Scene.GetScene().GetChildAtPoint(new Point(control.Location.X, control.Location.Y + control.Height));

            if (temp == null) return false;

            if (tags.Contains(temp.Tag)) return true;

            return false;
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
            //f.Text = Player.HPBar.Value.ToString();
            //Player.physics.ApplyPhysics();

            foreach (Entity entity in entities) {
                entity.physics.ApplyPhysics();
                if (CheckColision(entity.EntityControl, "ground", "wall", "wallR")) {
                    entity.physics.gravity = 0;
                    entity.IsOnGround = true;
                }
                else entity.IsOnGround = false;
                entity.HPBar.TrackEntity();
            }
            if (CheckColision(entities[1].EntityControl, "player")) {
                entities[1].physics.AddForce(-8);
            }
            //if (CheckColision(Player.EntityControl, "death")) {
            //    Player.EntityControl.Location = new System.Drawing.Point(945, 229);
            //}


            byte[] keys = new byte[256];

            GetKeyboardState(keys);

            if ((keys[(int)Keys.A] & 128) == 128) {
                //if (!CheckColision(Player.EntityControl, "wall")) {
                    Player.Left();
                //}
            }

            if ((keys[(int)Keys.Space] & 128) == 128) {
                Player.Jump();

                //Shake();
            }

            if ((keys[(int)Keys.D] & 128) == 128) {
                //if (!CheckColision(Player.EntityControl, "wallR")) {
                    Player.Right();
                //}
            }

            //entities[1].HPBar.TrackEntity();


        }


        [DllImport("user32.dll")]
        public static extern int GetKeyboardState(byte[] keystate);

        //public void KeyPress(object sender, KeyPressEventArgs e) {
        //    if ((Keys)e.KeyChar == Keys.Space) {
        //        Player.Jump();
        //    }


        //}

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