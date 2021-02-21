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
        public float gravity;
        float a;

        public float dx;

        Entity Entity { get; set; }

        public Physics(Entity entity) {
            this.Entity = entity;
            this.control = entity.EntityControl;

            gravity = 0;
            a = 0.5f;
            dx = 0;
        }


        public void ApplyPhysics() {
            CalculatePhysics();
        }


        public void CalculatePhysics() {
            gravity += a;

            Point point = control.Location;

            int vectorDirection = gravity > 0 ? 1 : -1;

            while (point.Y != control.Location.Y + (int)gravity) {
                point.Offset(0, vectorDirection);
                if (CheckColision(point, "ground")) {
                    Entity.IsOnGround = true;
                    point.Offset(0, -1);
                    gravity = 0;
                    break;
                }
                Entity.IsOnGround = false;
            }
            point.Offset((int)dx, 0);
            control.Location = point;
            dx = 0;

            //gravity += a;
            //int Y = control.Location.Y;

            //int vectorDirection = gravity > 0 ? 1 : -1;

            //scene.Text = gravity.ToString();

            //while (Y != control.Location.Y + (int)gravity) {
            //    if (CheckColision(control, "ground")) {
            //        this.Entity.IsOnGround = true;
            //        if (gravity > 0) {
            //            gravity = 0;
            //        }
            //        Y += vectorDirection - 1;
            //        break;
            //    }
            //    Y += vectorDirection;
            //}


            //control.Location = new Point((int)(control.Location.X + dx), Y);
            //dx = 0;
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
            gravity = -force;
        }
    }
}
