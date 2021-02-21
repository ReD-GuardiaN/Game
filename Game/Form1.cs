using Game.Game;
using Game.Landscape;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game {
    public partial class Form1 : Form {

        Game.Game game;

        public Form1() {
            InitializeComponent();
            Scene.SetScene(this);

            //Player entity = new Player(panel1);

            //new Control().SuspendLayout();

            this.BackgroundImage = Properties.Resources.background;
            this.SuspendLayout();


            var enem = new List<Enemy>() {
                    new Enemy(pictureBox2),
                    new Enemy(pictureBox3),
                    new Enemy(pictureBox4),
                    new Enemy(pictureBox5),
            };



            enem.ForEach(x => x.EntityControl.Click += (s,e) => {
                enem.Remove(x);
            });

            game = new Game.Game(
                player: new Player(pictureBox1),
                enemies: enem,
                    //new Enemy(pictureBox3),
                    //new Enemy(pictureBox4),
                    //new Enemy(pictureBox5),
                grounds: null,
                controls: this.Controls
                );

            this.KeyPress += game.KeyPress;
        }






    }
}
