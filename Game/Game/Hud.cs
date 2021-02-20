using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Game {
    class Hud {


    }

    class HPBar {

        Entity Entity { get; set; }

        public ProgressBar progressBar { get; set; }

        Point offset { get; set; }


        public HPBar(Entity entity, Point offset, int width = -1, int height = 3) {
            progressBar = new ProgressBar();
            this.Entity = entity;


            progressBar.Width = width == -1 ? entity.EntityControl.Width : width;

            progressBar.Height = height;

            this.offset = offset;

            progressBar.Maximum = this.Entity.HPMax;
            progressBar.Value = this.Entity.HPCurrent; 
            Scene.GetScene().Controls.Add(progressBar);
        }

        public void TrackEntity() {
            Point point = Entity.EntityControl.Location;
            progressBar.Location = new Point(point.X + offset.X, point.Y + offset.Y);
        }

    }
}
