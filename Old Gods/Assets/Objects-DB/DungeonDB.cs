using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DungeonDB : ScriptableObject
{
    public DungeonLevel[] dungeon;


    public int DungeonCount
    {
        get
        {
            return dungeon.Length;
        }
    }

    public DungeonLevel GetDungeonLevel(int index)
    {
        return dungeon[index];
    }

    public DungeonLevel GetDungeonLevelByLevel(int index)
    {
        for (int i = 0; i < DungeonCount; i++)
        {
            if (dungeon[i].level == index)
            {
                return dungeon[i];
            }
        }

        return null;

    }
}
