using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Game {
    class Physics {
        Control control;

        private float _Gravity;
        public float Gravity { get { return _Gravity; } set { _Gravity = value > 200 ? 200 : value; } }

        public bool IsOn = true;
        float a;

        public float dx;

        Entity Entity { get; set; }

        public Physics(Entity entity) {
            this.Entity = entity;
            this.control = entity.EntityControl;

            Gravity = 0;
            a = 0.5f;
            dx = 0;
        }


        public void ApplyPhysics() {
            CalculatePhysics();
        }


        public void CalculatePhysics() {
            Gravity += a;

            Point point = control.Location;

            int vectorDirection = Gravity > 0 ? 1 : -1;

            while (point.Y != control.Location.Y + (int)Gravity & IsOn) {
                point.Offset(0, vectorDirection);
                if (CheckColision(point, "ground")) {
                    Entity.IsOnGround = true;
                    point.Offset(0, -1);
                    Gravity = 0;
                    break;
                }
                Entity.IsOnGround = false;
            }
            point.Offset((int)dx, 0);
            control.Location = point;
            dx = 0;
        }

        Form scene = Scene.GetScene();

        public bool CheckColision(Point point, params string[] tags) {
            point.Offset(0, control.Height);
            var temp = scene.GetChildAtPoint(point);

            if (temp == null) {
                point.Offset(control.Width, 0);
                temp = scene.GetChildAtPoint(point);
            }

            if (temp == null) return false;

            if (tags.Contains(temp.Tag)) return true;

            return false;
        }


        public void AddForce(int force) {
            Gravity = -force;
        }
    }
}
