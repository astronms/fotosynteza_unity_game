using System;
using System.Collections.Generic;

[Serializable]
public class Save
{
    public List<Field> activeFields = new List<Field>();
    public int activePlayerId;
    public List<Player> activePlayers = new List<Player>();
    public int round;
    public int SunRotation;
}