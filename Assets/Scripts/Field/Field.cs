using UnityEngine;

[System.Serializable]
public class Field
{
    public Field()
    {
        _is_active = new bool[4];
    }
    public FieldVector _vector { get; set; } // vektor numeracji planszy
    public int _fieldlevel { get; set; }
    public TreeObject _assignment { get; set; } //wskaźnik do umiejscowionego drzewa
    public bool _already_used { get; set; }
    public bool[] _is_active { get; set; }
    public static FieldVector GetCoordinates(string[] tmp)
    {
        FieldVector fieldCoordinates = new FieldVector(int.Parse(tmp[0]), int.Parse(tmp[1]), int.Parse(tmp[2]));
        return fieldCoordinates;
    }
}