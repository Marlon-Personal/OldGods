using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class LevelDB : ScriptableObject
{
    public Level[] level;


    public int LevelCount
    {
        get
        {
            return level.Length;
        }
    }

    public Level GetLevel(int index)
    {
        return level[index];
    }

    public Level GetLevelByLevel(int index)
    {
        for (int i = 0; i < LevelCount; i++)
        {
            if (level[i].level == index)
            {
                return level[i];
            }
        }

        return null;

    }
}
