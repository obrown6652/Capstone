using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellbookCounter : ItemCounter
{

    private static SpellbookCounter instance;

    public static SpellbookCounter MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpellbookCounter>();
            }
            return instance;
        }
    }
}
