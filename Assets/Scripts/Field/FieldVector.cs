using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Field
{
    public class FieldVector
    {
        public int x { get; }
        public int y { get; }
        public int z { get; }

        public FieldVector(int v1, int v2, int v3)
        {
            x = v1;
            y = v2;
            z = v3;
        }


    }
}
