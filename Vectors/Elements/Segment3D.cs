using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vectors
{
    public class Segment3D
    {
        public Vector3D Start { get { return start; } }
        public Vector3D End { get { return end; } } 
        Vector3D start { get; }
        Vector3D end { get; }

        public Segment3D( Vector3D start, Vector3D end)
        {
            this.start = start;
            this.end = end;
        }
    }
}
