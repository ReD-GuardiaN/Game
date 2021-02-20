using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Game {
    class Enemy : Entity {

        public Enemy(Control control) {
            this.EntityControl = control;
            this.physics = new Physics(control);
            this.HPBar = new HPBar(this, new Point(0, -10));

        }

    }
}
