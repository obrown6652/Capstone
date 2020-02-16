using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCounter : ItemCounter
{
    private static GoldCounter instance;

    public static GoldCounter MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GoldCounter>();
            }
            return instance;
        }
    }
}
