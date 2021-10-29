using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterDB : ScriptableObject
{
    public Character[] character;


    public int UnitCount
    {
        get
        {
            return character.Length;
        }
    }

    public Character GetCharacter(int index)
    {
        return character[index];
    }

    public Character GetCharacterByID(int index)
    {

        for (int i = 0; i < UnitCount; i++)
        {
            if (character[i].spriteID == index)
            {
                return character[i];
            }
        }

        return null;

    }
}