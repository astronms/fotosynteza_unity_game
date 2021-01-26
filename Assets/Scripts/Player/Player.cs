using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int Id { get; }
    public PlayerType PlayerType { get; }
    public string Nick { get;  }
    public int Points { get; private set; }
    public int PointOfLights { get; private set; }
    public int NumberOfSeeds { get; private set; }
    public int NumberOfSmallTrees { get; private set; }
    public int NumberOfMediumTrees { get; private set; }
    public int NumberOfLargeTrees { get; private set; }

    public Player(int id, string nick, PlayerType playerType)
    {
        Nick = nick;
        PlayerType = playerType;
        Points = 0;
        PointOfLights = 100;
        NumberOfSeeds = 2;
        NumberOfSmallTrees = 4;
        NumberOfMediumTrees = 1;
        NumberOfLargeTrees = 0;
    }

    public bool ChangePoints(int x)
    {
        if ((Points + x)>=0)
        {
            Points += x;
            return true;
        }
        else
        {
            Debug.Log("Not enough Points");
            return false;
        }
    }

    public bool ChangePointOfLights(int x)
    {
        if ((PointOfLights + x) >= 0)
        {
            PointOfLights += x;
            return true;
        }
        else
        {
            MessageBox.Show("Nie masz wystarczająco punktów światła");
            Debug.Log("Not enough PointOfLights");
            return false;
        }
    }

    public bool ChangeNumberOfSmallTrees(int x)
    {
        if ((NumberOfSmallTrees + x) >= 0)
        {
            NumberOfSmallTrees += x;
            return true;
        }
        else
        {
            MessageBox.Show("Nie masz wystarczająco małych drzew");
            Debug.Log("Not enough NumberOfSmallTrees");
            return false;
        }
    }

    public bool ChangeNumberOfSeeds(int x)
    {
        if ((NumberOfSeeds + x) >= 0)
        {
            NumberOfSeeds += x;
            return true;
        }
        else
        {
            MessageBox.Show("Nie masz wystarczająco sadzonek");
            Debug.Log("Not enough NumberOfSeeds");
            return false;
        }
    }

    public bool ChangeNumberOfMediumTrees(int x)
    {
        if ((NumberOfMediumTrees + x) >= 0)
        {
            NumberOfMediumTrees += x;
            return true;
        }
        else
        {
            MessageBox.Show("Nie masz wystarczająco średnich drzew");
            Debug.Log("Not enough NumberOfMediumTrees");
            return false;
        }
    }

    public bool ChangeNumberOfLargeTrees(int x)
    {
        if ((NumberOfLargeTrees + x) >= 0)
        {
            NumberOfLargeTrees += x;
            return true;
        }
        else
        {
            MessageBox.Show("Nie masz wystarczająco dużych drzew");
            Debug.Log("Not enough NumberOfLargeTrees");
            return false;
        }
    }
    public bool buy()
    {
        return true;
    }

    public bool plant()
    {
        return false;
    }

}

