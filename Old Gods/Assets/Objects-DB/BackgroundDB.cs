using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BackgroundDB : ScriptableObject
{
    public Background[] background;


    public int BackgroundCount
    {
        get
        {
            return background.Length;
        }
    }

    public Background GetBackground(int index)
    {
        return background[index];
    }

    public Background GetBackgroundByLevel(int index)
    {
        for (int i = 0; i < BackgroundCount; i++)
        {
            if (background[i].level == index)
            {
                return background[i];
            }
        }

        return null;

    }
}
