using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


class Field
{
    public Vector3Int _vector { get; set; } // vektor numeracji planszy

    public int _fieldlevel { get; set; }

    public TreeObject _assignment { get; set; } //wskaźnik do umiejscowionego drzewa

    public bool _already_used { get; set; }
    public bool[] _is_active { get; set; }

    public Field()
    {
        _is_active = new bool[4];
    }


}
