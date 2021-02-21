using System;
using Game.Game;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Game.Landscape;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;

namespace Game.Game {
    abstract class Entity {


        public Control EntityControl { get; set; }

        public int HPMax { get; set; }

        public int HPCurrent { get; set; }

        public float Damage { get; set; }

        public float ReloadTime { get; set; }

        public float MoveSpeed { get; set; }

        public float JumpHeight { get; set; }

        public int Weight { get; set; }

        public Control Entity_ { get; set; }

        public HPBar HPBar { get; set; }

        public Physics physics { get; set; }


        public bool IsOnGround = false;

        //public async virtual void ForceOfGravity() {

        //    await Task.Run(() => {
        //        while (true) {
        //            foreach (Control item in Scene.SceneControls) {
        //                MessageBox.Show(item.Name);
        //                if (Entity_.Bounds.IntersectsWith(item.Bounds)) {
        //                    foreach (Surfaces surface in (Surfaces[])Enum.GetValues(typeof(Surfaces))) {
        //                        MessageBox.Show(surface.ToString());
        //                    }
        //                }
        //            }
        //        }
        //    });
        //}
    }



    class Player : Entity {

        public int JumpReloadTime = 400;//ms

        public bool IsJumpAvailable = true;

        public int JumpMaxCount = 1;

        public int CurrentJumps = 1;


        public Player(Control player) {
            this.EntityControl = player;


            this.HPMax = 100;

            this.HPCurrent = HPMax;

            this.HPBar = new HPBar(this, new Point(0, -10));
            //this.HP = 100;
            //this.HPBar.Value = this.HP;



            physics = new Physics(this);

            player.Tag += " player";

            //new Thread(() => {
            //    Thread.Sleep(2000);
            //    Hit(20);
            //}).Start();
        }


        public void Hit(int damage) {
            //if (HPBar.InvokeRequired) {
            //    HPBar.Invoke(new MethodInvoker(delegate {
            //        HPBar.Value -= damage;
            //    }));
            //}
            //else HPBar.Value -= damage;
        }


        async void ReloadJump() {
            IsJumpAvailable = false;
            await Task.Run(() => {
                Thread.Sleep(JumpReloadTime);
                IsJumpAvailable = true;
            });
        }

        //public void TrackPlayer(Control control, int x = 0, int y = 0) {
        //    control.Location = new Point(EntityControl.Location.X + x, EntityControl.Location.Y + y);
        //}


        public void Jump() {
            if (IsJumpAvailable && CurrentJumps > 0) {
                physics.AddForce(8);
                ReloadJump();
                CurrentJumps--;
            }
            if (IsOnGround) {
                CurrentJumps = JumpMaxCount;
            }
        }

        bool isLeft = true;
        bool isRight = true;

        Image PLeft = Properties.Resources.player6;
        Image PRight = Properties.Resources.player5;

        Dictionary<Skins, Dictionary<Sides, Image>> PlayerSkins = new Dictionary<Skins, Dictionary<Sides, Image>>() {
             {Skins.Default,new Dictionary<Sides, Image>() { { Sides.Left, Properties.Resources.player6 } } }
        };

        enum Skins {
            Default
        }

        enum Sides {
            Left,
            Right
        }

        public void Left() {

            if (IsOnGround) CurrentJumps = JumpMaxCount;
            physics.dx = -5;
            isRight = true;
            if (isLeft) {
                EntityControl.BackgroundImage = PlayerSkins[Skins.Default][Sides.Left];
                isLeft = false;
            }
        }

        public void Right() {
            if (IsOnGround) CurrentJumps = JumpMaxCount;
            physics.dx = 5;
            isLeft = true;
            if (isRight) {
                EntityControl.BackgroundImage = PRight;
                isRight = false;
            }
        }
    }

}
