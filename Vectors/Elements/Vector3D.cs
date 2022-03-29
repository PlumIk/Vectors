using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vectors
{
    public class Vector3D
    {
        public double GetX {get { return X; }}
        public double GetY {get { return Y; }}  
        public double GetZ {get { return Z; }}

        double X;
        double Y;
        double Z;

        public Vector3D(double x,double y,double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
