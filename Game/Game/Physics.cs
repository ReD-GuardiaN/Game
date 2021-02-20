using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Game {
    class Physics {
        Control control;
        public float gravity;
        float a;

        public float dx;

        public Physics(Control control) {
            this.control = control;

            gravity = 0;
            a = 0.5f;
            dx = 0;
        }


        public void ApplyPhysics() {
            CalculatePhysics();
        }


        public void CalculatePhysics() {

            //if (dx != 0) {
            //    control.Location = new Point((int)(control.Location.X + dx),control.Location.Y);
            //    if (dx > 0) {
            //        dx -= 5;
            //    }
            //    if (dx < 0) {
            //        dx += 5;
            //    }
            //}
            //if (control.Location.Y < 700) {
            //    control.Location = new Point(control.Location.X, (int)(control.Location.Y + gravity));//+ gravity
            //    gravity += a;
            //}

            control.Location = new Point((int)(control.Location.X + dx), (int)(control.Location.Y + gravity));
            gravity += a;
            dx = 0;
        }

        public void AddForce(int force) {
            gravity = -force;
        }
    }
}
