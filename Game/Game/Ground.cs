using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Landscape {
    class Ground {

        Control ground { get; set; }
    
        public Ground(Control control) {
            ground = control;
        }
    
    }

    enum Surfacess {
        Grass,
        Spike,
        Lava,
        Ice
    }
}
