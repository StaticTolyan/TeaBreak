using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.W)
            {
                DirectoryInfo d = new DirectoryInfo("standart");
                FileInfo[] Files = d.GetFiles("*.png");
                Wallpaper.Set(Files[0].FullName, WallpaperStyle.Fill);
                Application.Exit();
            }
        }
    }
}
