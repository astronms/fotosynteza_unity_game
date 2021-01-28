using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;

[System.Serializable]
public class Save
{
    public List<Player> activePlayers = new List<Player>();
    public List<Field> activeFields = new List<Field>();
    public int SunRotation;
    public int round;
    public int activePlayerId;
}

