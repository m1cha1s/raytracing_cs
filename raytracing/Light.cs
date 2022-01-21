using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Drawing;

namespace raytracing
{
    class Light
    {
        public Vector3 Pos;
        public Color Col;

        public Light(Vector3 pos, Color col)
        {
            Pos = pos;
            Col = col;
        }
    }
}
