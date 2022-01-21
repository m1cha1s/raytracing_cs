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
        List<Light> lights = new List<Light>();
        Random rng = new Random();

        Bitmap bmp;

        public Form1()
        {
            InitializeComponent();

            //spheres.Add(new Sphere(new Vector3(500, 150, 5), 20, Color.Red));
            //spheres.Add(new Sphere(new Vector3(500, 200, 40), 50, Color.Blue));
            //spheres.Add(new Sphere(new Vector3(500, 250, 1), 30, Color.Green));

            for(int i = 0; i < 100; i++)
            {
                spheres.Add(new Sphere(new Vector3(rng.Next(pictureBox1.Width), rng.Next(pictureBox1.Height), rng.Next(100)), 
                            rng.Next(10, 50), 
                            Color.FromArgb(rng.Next(255), rng.Next(255), rng.Next(255))));
            }

            lights.Add(new Light(new Vector3(100, 100, -100), Color.Blue));
            lights.Add(new Light(new Vector3(500, 100, -100), Color.Pink));

            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawImage(bmp, 0, 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //spheres[1].CPos -= new Vector3(0, 0, 10/10);

            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                {
                    bool intersect = false;
                    float minz = 0;
                    Color color = Color.Black;
                    Vector3 intersection;
                    Vector3 to_light;
                    Vector3 sphere_normal;

                    foreach (Sphere sph in spheres)
                    {
                        (bool inter, float z) = sph.Intersects(new Vector2((float)x, (float)y));
                        if (inter)
                        {
                            if (intersect && z >= minz)
                                continue;

                            // Shading :)

                            Vector3 colorV = new Vector3((float)sph.Col.R / 255, (float)sph.Col.G / 255, (float)sph.Col.B / 255);
                            Vector3 point_color = new Vector3(0, 0, 0);
                            Vector3 viever = new Vector3(0, 0, -1);

                            intersection = new Vector3(x, y, z);
                            sphere_normal = sph.Normal(intersection);
                            

                            foreach (Light light in lights)
                            {
                                to_light = Vector3.Normalize(light.Pos - intersection);
                                Vector3 light_reflection = Vector3.Reflect(to_light, sphere_normal);

                                float cos_alpha = Vector3.Dot(sphere_normal, to_light);
                                if (cos_alpha < 0)
                                {
                                    cos_alpha = 0;
                                }

                                float cos_phi = MathF.Pow(Vector3.Dot(viever, light_reflection), 100);
                                if (cos_phi < 0)
                                {
                                    cos_phi = 0;
                                }
                                
                                point_color += colorV * new Vector3(light.Col.R/255, light.Col.G/255, light.Col.B/255) * (cos_alpha + cos_phi);
                            }

                            point_color = Vector3.Clamp(point_color, new Vector3(0, 0, 0), new Vector3(1, 1, 1));

                            intersect = inter;
                            minz = z;
                            color = Color.FromArgb(Convert.ToInt32(point_color.X*255), Convert.ToInt32(point_color.Y*255), Convert.ToInt32(point_color.Z*255));
                            
                        }
                    }

                    bmp.SetPixel(x, y, color);
                }

            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //spheres[0].CPos.X = e.X;
            //spheres[0].CPos.Y = e.Y;

            lights[0].Pos.X = e.X;
            lights[0].Pos.Y = e.Y;
        }
    }
}
