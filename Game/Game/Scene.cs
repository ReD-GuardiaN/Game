using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Game {
    static class Scene {

        static Form MainScene { get; set; }

        public static void SetScene(Form form) {
            MainScene = form;
        }
        public static Form GetScene() {
            return MainScene;
        }
    }
}
