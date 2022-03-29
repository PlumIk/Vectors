using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vectors.Interfaces
{
    internal interface IIntersect
    {
        bool HasIntersect(Segment3D one, Segment3D two);
    }
}
