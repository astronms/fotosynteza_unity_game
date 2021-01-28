using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
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
    public override string ToString()
    {
        return string.Format("[{0}, {1}, {2}]", x, y, z);
    }

    public override bool Equals(object obj)
    {
        return this.Equals(obj as FieldVector);
    }

    public bool Equals(FieldVector f)
    {
        // If parameter is null, return false.
        if (Object.ReferenceEquals(f, null))
        {
            return false;
        }

        // Optimization for a common success case.
        if (Object.ReferenceEquals(this, f))
        {
            return true;
        }

        // If run-time types are not exactly the same, return false.
        if (this.GetType() != f.GetType())
        {
            return false;
        }

        // Return true if the fields match.
        // Note that the base class is not invoked because it is
        // System.Object, which defines Equals as reference equality.
        return (x == f.x) && (y == f.y) && (z == f.z);
    }
}

