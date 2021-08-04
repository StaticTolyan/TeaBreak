using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Start();
        }
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private Random rand = new Random();
        private Form f2 = new Form2();

        private void Start()
        { 
            f2 = new Form2();
            f2.Show();
            f2.Focus();
            
            const int WM_COMMAND = 0x111;
            const int MIN_ALL = 419;
            //const int MIN_ALL_UNDO = 416;

            IntPtr lHwnd = FindWindow("Shell_TrayWnd", null);
            SendMessage(lHwnd, WM_COMMAND, (IntPtr)MIN_ALL, IntPtr.Zero);

            timer1.Enabled = true;
            timer3.Enabled = true;
            WpChange();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Hide();
            Cursor = new Cursor(Cursor.Current.Handle);
            timer1.Interval = rand.Next(20, 200);
            if (rand.Next() % 4 == 0)
            {

                Cursor.Position = new Point(Cursor.Position.X - rand.Next(-10, 11), Cursor.Position.Y - rand.Next(-10, 11));

                if (rand.Next() % 5 == 0 && (uint)Cursor.Position.Y<1030)
                {
                    uint X = (uint)Cursor.Position.X;
                    uint Y = (uint)Cursor.Position.Y;
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer3.Enabled = false;
            DirectoryInfo d = new DirectoryInfo("standart");
            FileInfo[] Files = d.GetFiles("*.png");
            Wallpaper.Set(Files[0].FullName, WallpaperStyle.Fill);
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            WpChange();
        }
        private void WpChange()
        {
            DirectoryInfo d = new DirectoryInfo("work");

            FileInfo[] Files = d.GetFiles("*.png");
            if (Files.Length != 0)
            {
                Wallpaper.Set(Files[rand.Next(0, Files.Length - 1)].FullName, WallpaperStyle.Fill);
            }
            else
            {
                timer3.Enabled = false;
            }
        }
    }
}
