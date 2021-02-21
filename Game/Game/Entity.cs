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

        private int _HPCurrent;
        public int HPCurrent {
            get { return _HPCurrent; }
            set {
                _HPCurrent = value < 0 ? 0 : value;
                HPBar?.SetValue(_HPCurrent);
            }
        }

        public int Damage { get; set; }

        public float ReloadTime { get; set; }

        public float MoveSpeed { get; set; }

        public float JumpHeight { get; set; }

        public int Weight { get; set; }

        public Control Entity_ { get; set; }

        public HPBar HPBar { get; set; }

        public Physics physics { get; set; }


        public bool IsOnGround = false;
    }



    class Player : Entity {

        public int JumpReloadTime = 400;//ms

        public bool IsJumpAvailable = true;

        public bool IsHitAvailable = true;

        public int HitReloadTime = 400;//ms

        public int JumpMaxCount = 1;

        public int CurrentJumps = 1;

        public event EventHandler OnPlayerDeath;


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
            if (!IsHitAvailable) return;
            this.HPCurrent -= damage;
            HPBar.SetValue(HPCurrent);

            if (HPCurrent <= 0) OnPlayerDeath?.Invoke(this, EventArgs.Empty);
            HitCoolDown();
        }


        async void ReloadJump() {
            IsJumpAvailable = false;
            await Task.Run(() => {
                Thread.Sleep(JumpReloadTime);
                IsJumpAvailable = true;
            });
        }

        async void HitCoolDown() {
            IsHitAvailable = false;
            await Task.Run(() => {
                Thread.Sleep(HitReloadTime);
                IsHitAvailable = true;
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
