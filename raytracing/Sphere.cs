using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Numerics;

namespace raytracing
{
    class Sphere
    {
        public Vector3 CPos; // position of sphere center
        public float R;      // sphere radius
        public Color Col;    // sphere color

        public Sphere(Vector3 cPos, float r, Color col)
        {
            CPos = cPos;
            R = r;
            Col = col;
        }

        public (bool, float) Intersects (Vector2 pos)
        {
            bool intersects = false;
            float intersectZ = 0;
           
            if (Vector2.DistanceSquared(pos, new Vector2(CPos.X, CPos.Y)) <= R * R)
            {
                intersects = true;
                intersectZ = CPos.Z - MathF.Sqrt(R*R-(pos.X-CPos.X)*(pos.X-CPos.X)-(pos.Y-CPos.Y)*(pos.Y-CPos.Y));
            }
            return (intersects, intersectZ);
        }
    }
}
