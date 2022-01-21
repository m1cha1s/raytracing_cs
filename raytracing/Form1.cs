using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace raytracing
{
    public partial class Form1 : Form
    {
        List<Sphere> spheres = new List<Sphere>();

        Bitmap bmp;

        public Form1()
        {
            InitializeComponent();

            spheres.Add(new Sphere(new Vector3(50, 30, 5), 20, Color.Red));
            spheres.Add(new Sphere(new Vector3(50, 70, 40), 50, Color.Blue));
            spheres.Add(new Sphere(new Vector3(50, 100, 1), 30, Color.Green));

            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawImage(bmp, 0, 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            spheres[1].CPos.Z -= 5 / 30;

            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                {
                    bool intersect = false;
                    float minz = 0;
                    Color color = Color.Black;

                    foreach (Sphere sph in spheres)
                    {
                        (bool inter, float z) = sph.Intersects(new Vector2((float)x, (float)y));
                        if (inter)
                        {
                            if (intersect && z >= minz)
                                continue;

                            intersect = inter;
                            minz = z;
                            color = sph.Col;
                        }
                    }

                    if (intersect)
                    {
                        bmp.SetPixel(x, y, color);
                        //pictureBox1.Refresh();

                    }
                }

            pictureBox1.Refresh();
        }
    }
}
